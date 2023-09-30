using Nakama;
using Nakama.TinyJson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class NakamaManager : MonoBehaviour
{
    public NakamaConnection NakamaConnection;
    //private IDictionary<string, GameObject> players;
    //private IUserPresence localUser;
    //private GameObject localPlayer;
    public IMatch currentMatch;
    private string localDisplayName;
    public static NakamaManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private async void Start()
    {
        Debug.LogError("------ Nakama Manager Start");
        // Create an empty dictionary to hold references to the currently connected players.
        //players = new Dictionary<string, GameObject>();

        // Get a reference to the UnityMainThreadDispatcher.
        // We use this to queue event handler callbacks on the main thread.
        // If we did not do this, we would not be able to instantiate objects or manipulate things like UI.
        var mainThread = UnityMainThreadDispatcher.Instance();

        // Connect to the Nakama server.
        await NakamaConnection.Connect();

        // Enable the Find A Match button on the main menu.
        //MainMenu.GetComponent<MainMenu>().EnableFindMatchButton();

        // Setup network event handlers.
        NakamaConnection.Socket.ReceivedMatchmakerMatched += m => mainThread.Enqueue(() => OnReceivedMatchmakerMatched(m));
        NakamaConnection.Socket.ReceivedMatchPresence += m => mainThread.Enqueue(() => OnReceivedMatchPresence(m));
        NakamaConnection.Socket.ReceivedMatchState += m => mainThread.Enqueue(async () => await OnReceivedMatchState(m));
        //FindMatch();
        // Setup in-game menu event handler.
        //InGameMenu.GetComponent<InGameMenu>().OnRequestQuitMatch.AddListener(async () => await QuitMatch());
    }
    public async void FindMatch()
    {
        ConnectingPopHandler.Instance.Open();
        PlayerPrefs.SetString("Name", "Sri");
        SetDisplayName("Sri");
        await NakamaConnection.FindMatch(2);
    }
    /// <summary>
    /// Called when a MatchmakerMatched event is received from the Nakama server.
    /// </summary>
    /// <param name="matched">The MatchmakerMatched data.</param>
    private async void OnReceivedMatchmakerMatched(IMatchmakerMatched matched)
    {
        Debug.LogError("------ OnReceivedMatchmakerMatched");
        // Cache a reference to the local user.
        GameManagerSlither.instance.localUser = matched.Self.Presence;

        // Join the match.
        var match = await NakamaConnection.Socket.JoinMatchAsync(matched);

        // Disable the main menu UI and enable the in-game UI.
        // In a larger game we would probably transition to a totally different scene.
        //MainMenu.GetComponent<MainMenu>().DeactivateMenu();//Srikanth
        //InGameMenu.SetActive(true);//Srikanth

        // Play the match music.
        //AudioManager.PlayMatchTheme();//Srikanth
        currentMatch = match;
        Debug.LogError("currentMatch 1111=" + currentMatch+"::count="+ match.Size);
        GameManagerSlither.instance.OnPlayButton(match);
        ConnectingPopHandler.Instance.Close();

        foreach (var user in match.Presences)
        {
            Debug.LogError("---- GeneratePlayerRandomHeadIndex 111111111");
            //GameManagerSlither.instance.GeneratePlayerRandomHeadIndex(user);//Srikanth
            StartCoroutine(GameManagerSlither.instance.GeneratePlayerRandomHeadIndex(1,user));//Srikanth
        }
        // Spawn a player instance for each connected user.
        foreach (var user in match.Presences)
        {
            Debug.LogError("---- can spawn player here");
            StartCoroutine(GameManagerSlither.instance.SpawnPlayer(2,match.Id, user));//Srikanth
        }

        // Cache a reference to the current match.
    }

    /// <summary>
    /// Called when a player/s joins or leaves the match.
    /// </summary>
    /// <param name="matchPresenceEvent">The MatchPresenceEvent data.</param>
    private void OnReceivedMatchPresence(IMatchPresenceEvent matchPresenceEvent)
    {
        Debug.LogError("--------- OnReceivedMatchPresence");
        // For each new user that joins, spawn a player for them.
        foreach (var user in matchPresenceEvent.Joins)
        {
            Debug.LogError("currentMatch 222 =" + currentMatch);

            Debug.LogError("---- GeneratePlayerRandomHeadIndex 222222222");
            StartCoroutine(GameManagerSlither.instance.GeneratePlayerRandomHeadIndex(1,user));//Srikanth
        }
        foreach (var user in matchPresenceEvent.Joins)
        {
            //GameManagerSlither.instance.SpawnPlayer(matchPresenceEvent.MatchId, user);//Srikanth
            StartCoroutine(GameManagerSlither.instance.SpawnPlayer(2, matchPresenceEvent.MatchId, user));//Srikanth
        }

        // For each player that leaves, despawn their player.
        foreach (var user in matchPresenceEvent.Leaves)
        {
            if (GameManagerSlither.instance.players.ContainsKey(user.SessionId))
            {
                Destroy(GameManagerSlither.instance.players[user.SessionId]);
                GameManagerSlither.instance.players.Remove(user.SessionId);
            }
        }
    }

    /// <summary>
    /// Called when new match state is received.
    /// </summary>
    /// <param name="matchState">The MatchState data.</param>
    private async Task OnReceivedMatchState(IMatchState matchState)
    {
        // Get the local user's session ID.
        var userSessionId = matchState.UserPresence.SessionId;

        // If the matchState object has any state length, decode it as a Dictionary.
        var state = matchState.State.Length > 0 ? System.Text.Encoding.UTF8.GetString(matchState.State).FromJson<Dictionary<string, string>>() : null;
       // Debug.LogError("---------- NakamaManager OnReceived Match State opcode=" + matchState.OpCode);

        // Decide what to do based on the Operation Code as defined in OpCodes.
        switch (matchState.OpCode)
        {
            case OpCodes.Died:
                Debug.Log("----- NakamaManager died event");

                // Get a reference to the player who died and destroy their GameObject after 0.5 seconds and remove them from our players array.
                var playerToDestroy = GameManagerSlither.instance.players[userSessionId];
                //Destroy(playerToDestroy, 0.5f);
                GameManagerSlither.instance.players.Remove(userSessionId);
                Debug.Log("----- NakamaManager died event players count="+(GameManagerSlither.instance.players.Count));
                Debug.Log("----- NakamaManager died event first key=" + (GameManagerSlither.instance.players.First().Key)+"::sessionID="+ GameManagerSlither.instance.localUser.SessionId);

                // If there is only one player left and that us, announce the winner and start a new round.
                if (GameManagerSlither.instance.players.Count == 1 && GameManagerSlither.instance.players.First().Key == GameManagerSlither.instance.localUser.SessionId)
                {
                    GameManagerSlither.instance.Invoke("AnnounceWinner", 2);//Srikanth

                    //GameManagerSlither.instance.Invoke("openResultPage", 2);
                }
                break;
            case OpCodes.AnnounceWinner:
                Debug.Log("----- NakamaManager AnnounceWinner event");

                // Display the winning player's name and begin a new round.
                GameManagerSlither.instance.openResultPage(state["winnerID"]);//Srikanth
                break;
            case OpCodes.Respawned:
                // Spawn the player at the chosen spawn index.
                //GameManagerSlither.instance.SpawnPlayer(currentMatch.Id, matchState.UserPresence, int.Parse(state["spawnIndex"]));//Srikanth
                //GameManagerSlither.instance.SpawnPlayer(currentMatch.Id, matchState.UserPresence);//Srikanth
                StartCoroutine(GameManagerSlither.instance.SpawnPlayer(2, currentMatch.Id, matchState.UserPresence));//Srikanth
                break;
            case OpCodes.NewRound:
                Debug.Log("----- NakamaManager NewRound event");

                // Display the winning player's name and begin a new round.
                //await AnnounceWinnerAndRespawn(state["winningPlayerName"]);//Srikanth
                GameManagerSlither.instance.restartPlayersCount++;
               
                GameManagerSlither.instance.RestartGame();
                break;
            case OpCodes.PlayerHeadIndex:
                // Display the winning player's name and begin a new round.
                //await AnnounceWinnerAndRespawn(state["winningPlayerName"]);//Srikanth

                GameManagerSlither.instance.PlayerHeadIndexSet(userSessionId, int.Parse(state["PlayerHeadIndexs"]));
                break;

            default:
                break;
        }
    }
    /// <summary>
    /// Sends a match state message across the network.
    /// </summary>
    /// <param name="opCode">The operation code.</param>
    /// <param name="state">The stringified JSON state data.</param>
    public async Task SendMatchStateAsync(long opCode, string state)
    {
        await NakamaConnection.Socket.SendMatchStateAsync(currentMatch.Id, opCode, state);
    }

    /// <summary>
    /// Sends a match state message across the network.
    /// </summary>
    /// <param name="opCode">The operation code.</param>
    /// <param name="state">The stringified JSON state data.</param>
    public void SendMatchState(long opCode, string state)
    {
        //Debug.Log("SendMatchState socket=" + NakamaConnection.Socket + "::currentMatch=" + currentMatch);
        NakamaConnection.Socket.SendMatchStateAsync(currentMatch.Id, opCode, state);
    }
    public void SetDisplayName(string displayName)
    {
        // We could set this on our Nakama Client using the below code:
        // await NakamaConnection.Client.UpdateAccountAsync(NakamaConnection.Session, null, displayName);
        // However, since we're using Device Id authentication, when running 2 or more clients locally they would both display the same name when testing/debugging.
        // So in this instance we will just set a local variable instead.
        localDisplayName = displayName;
    }
}
