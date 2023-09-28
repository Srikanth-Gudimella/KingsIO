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

using UnityEngine;

/// <summary>
/// Syncs the local player's state across the network by sending frequent network packets containing relevent information such as velocity, position and inputs.
/// </summary>
public class PlayerNetworkLocalSync : MonoBehaviour
{
    /// <summary>
    /// How often to send the player's velocity and position across the network, in seconds.
    /// </summary>
    public float StateFrequency = 0.1f;

    private NakamaManager gameManager;
    //private PlayerHealthController playerHealthController;//Srikanth
    //private PlayerInputController playerInputController;//Srikanth
    //private Rigidbody2D playerRigidbody;
    //private Transform playerTransform;
    private float stateSyncTimer;
    public Snake ThisSnake;
    public SnakeHead ThisSnakeHead;

    /// <summary>
    /// Called by Unity when this GameObject starts.
    /// </summary>
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("NakamaManager").GetComponent<NakamaManager>();
        //playerInputController = GetComponent<PlayerInputController>();//Srikanth
        // playerRigidbody = GetComponentInChildren<Rigidbody2D>();
        ThisSnake = GetComponent<Snake>();
        ThisSnakeHead = ThisSnake.SnakeHead;

        //playerTransform = GetComponent<Snake>().SnakeHead.transform;
    }

    /// <summary>
    /// Called by Unity every frame after all Update calls have been made.
    /// </summary>
    private void LateUpdate()
    {
        // Send the players current velocity and position every StateFrequency seconds.
        //if (stateSyncTimer <= 0)
        //{
            // Send a network packet containing the player's velocity and position.
            gameManager.SendMatchState(
                OpCodes.VelocityAndPosition,
                MatchDataJson.VelocityAndPosition(ThisSnakeHead.transform.position, ThisSnakeHead.transform.rotation));

            stateSyncTimer = StateFrequency;
        //}

        stateSyncTimer -= Time.deltaTime;

        // If the players input hasn't changed, return early.
        //if (!playerInputController.InputChanged)Srikanth
        //{
        //    return;
        //}

        // Send network packet with the player's current input.
        //Srikanth
        //gameManager.SendMatchState(
        //    OpCodes.Input, 
        //    MatchDataJson.Input(playerInputController.HorizontalInput, playerInputController.Jump, playerInputController.JumpHeld, playerInputController.Attack)
        //);
    }
}
