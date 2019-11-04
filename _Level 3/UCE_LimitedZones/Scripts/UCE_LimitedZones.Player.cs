// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using System.Collections.Generic;

// =======================================================================================
// PLAYER
// =======================================================================================
[System.Serializable]
public partial class Player
{

    UCE_LimitedZonesManager sharedInstanceManager;

    // -----------------------------------------------------------------------------------
    // Cmd_UCE_teleportPlayerToInstance
    // @Client -> @Server
    // -----------------------------------------------------------------------------------
    public void Cmd_UCE_teleportPlayerToInstance(int index, int instanceCategory, int instanceIndex)
    {

        if (!sharedInstanceManager)
            sharedInstanceManager = FindObjectOfType<UCE_LimitedZonesManager>();

        List<UCE_LimitedZonesEntry> instancesAvailable = sharedInstanceManager.getAvailableSharedInstances(this, instanceCategory);

        instancesAvailable[instanceIndex].payEntranceCost(this);

        UCE_PlayerGroupLocations locations = instancesAvailable[instanceIndex].targetArea.playerGroupLocation[index];

        if (locations.teleportPosition.Length == 0) return;

        index = UnityEngine.Random.Range(0, locations.teleportPosition.Length - 1);

        agent.Warp(locations.teleportPosition[index].position);

    }




}

// =======================================================================================