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

/// <summary>
/// Defines the various network operations that can be sent/received.
/// </summary>
public class OpCodes
{
    public const long VelocityAndPosition = 1;
    public const long Input = 2;
    public const long Died = 3;
    public const long Respawned = 4;
    public const long NewRound = 5;
    public const long shoot = 6;
    public const long damage = 7;
    public const long AnnounceWinner = 8;
    public const long Restart = 9;
    public const long PlayerHeadIndex = 10;

}
