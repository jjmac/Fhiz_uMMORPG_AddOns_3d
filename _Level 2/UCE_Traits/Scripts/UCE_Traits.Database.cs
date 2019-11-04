// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// * Instructions.......................: https://indie-mmo.net/knowledge-base/
// =======================================================================================
#if _MYSQL
using MySql.Data;								// From MySql.Data.dll in Plugins folder
using MySql.Data.MySqlClient;                   // From MySql.Data.dll in Plugins folder
#elif _SQLITE

using SQLite; 						// copied from Unity/Mono/lib/mono/2.0 to Plugins

#endif

// DATABASE (SQLite / mySQL Hybrid)

public partial class Database
{
    // -----------------------------------------------------------------------------------
    // Connect_UCE_Traits
    // -----------------------------------------------------------------------------------
    [DevExtMethods("Connect")]
    private void Connect_UCE_Traits()
    {
#if _MYSQL
		ExecuteNonQueryMySql(@"CREATE TABLE IF NOT EXISTS character_UCE_traits (`character` VARCHAR(32) NOT NULL, name VARCHAR(32) NOT NULL)");
#elif _SQLITE
        connection.CreateTable<character_UCE_traits>();
#endif
    }

    // -----------------------------------------------------------------------------------
    // CharacterLoad_UCE_Traits
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterLoad")]
    private void CharacterLoad_UCE_Traits(Player player)
    {
#if _MYSQL
		var table = ExecuteReaderMySql("SELECT name FROM character_UCE_traits WHERE `character`=@character", new MySqlParameter("@character", player.name));
#elif _SQLITE
        var table = connection.Query<character_UCE_traits>("SELECT name FROM character_UCE_traits WHERE character=?", player.name);
#endif
        foreach (var row in table)
        {
#if _MYSQL
            UCE_TraitTemplate tmpl = UCE_TraitTemplate.dict[((string)row[0]).GetStableHashCode()];
#elif _SQLITE
            UCE_TraitTemplate tmpl = UCE_TraitTemplate.dict[row.name.GetStableHashCode()];
#endif
            player.UCE_Traits.Add(new UCE_Trait(tmpl));
        }
    }

    // -----------------------------------------------------------------------------------
    // CharacterSave_UCE_Traits
    // -----------------------------------------------------------------------------------
    [DevExtMethods("CharacterSave")]
    private void CharacterSave_UCE_Traits(Player player)
    {
#if _MYSQL
		ExecuteNonQueryMySql("DELETE FROM character_UCE_traits WHERE `character`=@character", new MySqlParameter("@character", player.name));
        for (int i = 0; i < player.UCE_Traits.Count; ++i)
        {
            UCE_Trait trait = player.UCE_Traits[i];
            ExecuteNonQueryMySql("INSERT INTO character_UCE_traits VALUES (@character, @name)",
                    new MySqlParameter("@character", player.name),
                    new MySqlParameter("@name", trait.name)
                    );
        }
#elif _SQLITE
        connection.Execute("DELETE FROM character_UCE_traits WHERE character=?", player.name);
        for (int i = 0; i < player.UCE_Traits.Count; ++i)
        {
            UCE_Trait trait = player.UCE_Traits[i];
            connection.Insert(new character_UCE_traits
            {
                character = player.name,
                name = trait.name
            });
        }
#endif
    }

    // -----------------------------------------------------------------------------------
}
