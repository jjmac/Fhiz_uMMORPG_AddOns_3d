// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// * Instructions.......................: https://indie-mmo.net/knowledge-base/
// =======================================================================================
using UnityEngine;
using Mirror;
using System.Collections;
using System;

// PLAYER

public partial class Player
{
    [Header("-=-=-=- UCE LEVEL UP NOTICE -=-=-=-")]
    public UCE_PopupClass levelUpNotice;

    // -----------------------------------------------------------------------------------
    // OnLevelUp_UCE_LevelUpNotice
    // -----------------------------------------------------------------------------------
    [Server]
    [DevExtMethods("OnLevelUp")]
    private void OnLevelUp_UCE_LevelUpNotice()
    {
        Target_UCE_ShowPopup(connectionToClient, levelUpNotice.message + level.ToString(), levelUpNotice.iconId, levelUpNotice.soundId);
    }
}
