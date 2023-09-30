using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPageHandler : MonoBehaviour
{
    public static StartPageHandler Instance;
    public InputField PlayerNameField;
    public InputField PlayersCount;

    private void Awake()
    {
        Instance = this;
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void StartClick()
    {
        if(string.IsNullOrEmpty(PlayerNameField.text))
        {
            iTween.PunchScale(PlayerNameField.gameObject, iTween.Hash("x", 0.3f, "y", 0.2f, "time", 0.5f));
            return;
        }
        Debug.LogError("Player Name="+PlayerNameField.text);
        Debug.LogError("Players Count=" + PlayersCount.text);
        GameManagerSlither.instance.LocalPlayerName = PlayerNameField.text;
        if(int.Parse(PlayersCount.text)>8)
        {
            PlayersCount.text = 8+"";
        }
        GameManagerSlither.instance.TotalPlayersCount = int.Parse(PlayersCount.text);
        NakamaManager.Instance.FindMatch();
        Close();

    }
}
