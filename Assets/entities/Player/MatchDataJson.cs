﻿/*
Copyright 2021 Heroic Labs

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.Collections.Generic;
using Nakama.TinyJson;
using UnityEngine;

/// <summary>
/// A static class that creates JSON string network messages.
/// </summary>
public static class MatchDataJson
{   
    /// <summary>
    /// Creates a network message containing velocity and position.
    /// </summary>
    /// <param name="velocity">The velocity to send.</param>
    /// <param name="position">The position to send.</param>
    /// <returns>A JSONified string containing velocity and position data.</returns>
    public static string VelocityAndPosition(Vector3 position,Quaternion rotation)
    {
        var values = new Dictionary<string, string>
        {
            { "position.x", position.x.ToString() },
            { "position.z", position.z.ToString() },
            { "rotation.x", rotation.eulerAngles.x.ToString() },
            { "rotation.y", rotation.eulerAngles.y.ToString() },
            { "rotation.z", rotation.eulerAngles.z.ToString() }
        };

        return values.ToJson();
    }
    
    /// <summary>
    /// Creates a network message containing player input.
    /// </summary>
    /// <param name="horizontalInput">The current horizontal input.</param>
    /// <param name="jump">The jump input.</param>
    /// <param name="jumpHeld">The jump held input.</param>
    /// <param name="attack">The attack input.</param>
    /// <returns>A JSONified string containing player input.</returns>
    public static string Input(float horizontalInput, bool jump, bool jumpHeld, bool attack)
    {
        var values = new Dictionary<string, string>
        {
            { "horizontalInput", horizontalInput.ToString() },
            { "jump", jump.ToString() },
            { "jumpHeld", jumpHeld.ToString() },
            { "attack", attack.ToString() }
        };

        return values.ToJson();
    }
    public static string Input(bool IsShoot)
    {
        var values = new Dictionary<string, string>
        {
            { "isshoot", IsShoot.ToString() }
        };

        return values.ToJson();
    }
    public static string damage(int damageValue)
    {
        var values = new Dictionary<string, string>
        {
            { "damage", damageValue.ToString() }
        };

        return values.ToJson();
    }
    public static string Getkeyvalue(string keystring,int value)
    {
        var values = new Dictionary<string, string>
        {
            { "PlayerHeadIndexs", value.ToString() }
        };

        return values.ToJson();
    }

    /// <summary>
    /// Creates a network message specifying that the player died and the position when they died.
    /// </summary>
    /// <param name="position">The position on death.</param>
    /// <returns>A JSONified string containing the player's position on death.</returns>
    public static string Died(Vector3 position)
    {
        var values = new Dictionary<string, string>
        {
            { "position.x", position.x.ToString() },
            { "position.y", position.y.ToString() }
        };

        return values.ToJson();
    }

    /// <summary>
    /// Creates a network message specifying that the player respawned and at what spawn point.
    /// </summary>
    /// <param name="spawnIndex">The spawn point.</param>
    /// <returns>A JSONified string containing the player's respawn point.</returns>
    public static string Respawned(Vector3 position)
    {
        var values = new Dictionary<string, string>
        {
            { "position.x", position.x.ToString() },
            { "position.y", position.y.ToString() }
        };

        return values.ToJson();
    }

    /// <summary>
    /// Creates a network message indicating a new round should begin and who won the previous round.
    /// </summary>
    /// <param name="winnerPlayerName">The winning player's name.</param>
    /// <returns>A JSONified string containing the winning players name.</returns>
    public static string StartNewRound(string winnerPlayerName)
    {
        var values = new Dictionary<string, string>
        {
            { "winningPlayerName", winnerPlayerName }
        };
        
        return values.ToJson();
    }
    public static string AnnounceWiner(string winnerID)
    {
        var values = new Dictionary<string, string>
        {
            { "winnerID", winnerID }
        };

        return values.ToJson();
    }
}
