using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ResultPage : MonoBehaviour {
	public Text sizeCount,enemiesCollectedCount,coinsTxt;
	public static ResultPage mee;
	public GameObject popUp,nextBtn,rateBtn,shareBtn,retryBtn;
	public GameObject moreGamesImg,title;
//	public GameObject inviteBtn;
	void Awake()
	{
		mee = this;
		gameObject.SetActive (false);
	}

	public void Open()
	{
//		AudioClipManager.Instance.Play (InGameSounds.ResultPage);
		gameObject.SetActive (true);
        //btm2018
        /*
		if (!GameManagerSlither.isOfflineMode) {
			PhotonNetwork.LeaveRoom ();
			PhotonNetwork.Disconnect ();
		}*/
        //		if (GirlGameConfigs.mee != null && GirlGameConfigs.moreIcon != null) {
        //			moreGamesImg.SetActive (true);
        //			moreGamesImg.GetComponent<Image> ().sprite = GirlGameConfigs.moreIcon;
        ////			title.transform.localPosition = new Vector3 (0, 174, 0);
        //		} else {
        //			moreGamesImg.SetActive (false);
        ////			title.transform.localPosition = new Vector3 (0, 125, 0);
        //		}
        gameObject.transform.localPosition = Vector3.zero;

		sizeCount.text = (GameManagerSlither.instance.myScore-200)+"";
		enemiesCollectedCount.text = GameManagerSlither.instance.enemiesKilledCount+"";
		Debug.Log ("GameManagerSlither.instance.enemiesKilledCount="+(GameManagerSlither.instance.enemiesKilledCount));
//		coinsTxt.text = GameManagerSlither.instance.coinsCollectedCount+"";
//		coinsTxt.text =GameManagerSlither.instance.enemiesKilledCount+"*10";
		coinsTxt.text =(GameManagerSlither.instance.enemiesKilledCount*10)+"";

		StoreManager.AddCoins (GameManagerSlither.instance.enemiesKilledCount*10);

		iTween.MoveFrom(popUp,iTween.Hash ("y", 1000, "time", 0.5f, "delay", 0,  "islocal", true,"easetype", iTween.EaseType.spring));

		iTween.Stop (retryBtn);
		retryBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (retryBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (nextBtn);
		nextBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (nextBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.4f, "islocal", true, "easetype", iTween.EaseType.spring));


			iTween.Stop (rateBtn);
			rateBtn.transform.localScale = Vector3.one;
			iTween.ScaleFrom (rateBtn, iTween.Hash ("x", 0, "y", 0, "time", 0.4f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.spring));
		if (PlayerPrefs.GetString ("IsRated") == "true") {
			rateBtn.GetComponent<Button> ().interactable = false;
		} 
		iTween.Stop (shareBtn);
		shareBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (shareBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.spring));

        //		iTween.Stop (inviteBtn);
        //		inviteBtn.transform.localScale = new Vector3(-1,1,1);
        //		iTween.ScaleFrom (inviteBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.6f, "islocal", true, "easetype", iTween.EaseType.spring));
        //btm2018
        /*
                Debug.Log ("------ result page levelNumber="+(GameManagerSlither.levelNumber));
                if (GirlGameConfigs.mee) {
        //			GirlGameConfigs.mee.PushDisplayname (PlayerPrefs.GetString ("myName",""));

                    GirlGameConfigs.mee.PushLeaderStats ("size",PlayerPrefs.GetInt (StoreManager.Best_Score,GameManagerSlither.myPlayerMass), true);
                    GirlGameConfigs.mee.AnalyticsLevelUp (GameManagerSlither.levelNumber, GameManagerSlither.selectedSnakeIndex, GameManagerSlither.myPlayerMass);
                    GirlGameConfigs.mee.showRotationAds (GameManagerSlither.levelNumber, AdsPageType.lc);
                }*/
		#if Adsetup_ON
		if (AdManager.instance) {
			//			GirlGameConfigs.mee.PushDisplayname (PlayerPrefs.GetString ("myName",""));
			//srikanth playfab commented
			//			PlayfabFBManager.mee.PushLeaderStats ("size",PlayerPrefs.GetInt (StoreManager.Best_Score,GameManager.myPlayerMass), true);
			//			GameConfigs2018.mee.AnalyticsLevelUp (GameManager.levelNumber, GameManager.selectedFidgetIndex, GameManager.myPlayerMass);// old adsetup
			if(GameManagerSlither.ShowVideoInResultPage)
			{
				Debug.Log ("--------------------- ResultPageOpen 111111111111");
				Invoke("OpenRewardVideo",AdManager.instance.LcAdDelay);
			}
			else
			{
				Debug.Log ("--------------------- ResultPageOpen 222222222");
				//				AdManager.instance.RunActions (AdManager.PageType.LC,GameManager.levelNumber);
			}
			AdManager.instance.RunActions (AdManager.PageType.LC,GameManagerSlither.levelNumber);
		}
		#endif

	}
	void OpenRewardVideo()
	{
		#if Adsetup_ON
		AdManager.instance.ShowRewardVideoWithCallback ((result)=>{
			if (result)
			{
				Debug.Log("Watched video after resume");
			}

		}
		);
		#endif
	}
    public void playClick()
	{
		AudioClipManager.Instance.Play (InGameSounds.Button);

		System.GC.Collect ();
		int playedCountForUpgrade = PlayerPrefs.GetInt("playedCountForUpgrade", 0);
		Debug.Log ("played count forupgrade="+playedCountForUpgrade+":::lvlnumber="+GameManagerSlither.levelNumber);
		if (GameManagerSlither.levelNumber == 2 && playedCountForUpgrade == 2) {
			PlayerPrefs.SetInt ("playedCountForUpgrade", 0);
			//PlayerPrefs.SetInt ("UpgradePageNavigationCount", AdManager.instance.UnlockPopIn_UP_LS[0]-1);
			Application.LoadLevel("UpgradePage");
		}
		else if (playedCountForUpgrade == 6) {
			PlayerPrefs.SetInt ("playedCountForUpgrade", 0);
			//PlayerPrefs.SetInt ("UpgradePageNavigationCount", AdManager.instance.UnlockPopIn_UP_LS[0]-1);
			Application.LoadLevel("UpgradePage");
		} else {
			Application.LoadLevel ("Menu");
		}
	}
	public void ReStartClick()
    {
		GameManagerSlither.instance.restartPlayersCount++;
		NakamaManager.Instance.SendMatchState(
			   OpCodes.NewRound,
			   MatchDataJson.Input(true)
		   );

		GameManagerSlither.instance.RestartGame();
    }
	public void retryClick()
	{
		AudioClipManager.Instance.Play (InGameSounds.Button);
		System.GC.Collect ();
		Application.LoadLevel ("GameScene");
	}
	public void RateClick()
	{
		AudioClipManager.Instance.Play (InGameSounds.Button);
		//AdManager.instance.ShowRatePopUp ();
    }
    public void shareClick()
	{
		AudioClipManager.Instance.Play (InGameSounds.Button);
		//AdManager.instance.FacebookShare ();
    }
    public void moreGamesIconClick()
	{
		AudioClipManager.Instance.Play (InGameSounds.Button);
        //btm2018
        //Debug.Log ("more games icon link="+GirlGameConfigs.moreiconlink);
        //Application.OpenURL (GirlGameConfigs.moreiconlink);
    }

//    public void inviteClick()
//	{
//
//		//Debug.Log("FB inviteclick isloggedin="+FB.IsLoggedIn);
//#if btmfacebook
//        FB.Mobile.AppInvite(new Uri("https://fb.me/1514943851930474"), callback: this.HandleResult);
//#endif
//
//	}
//
//    #if btmfacebook
//	protected void HandleResult(IResult result)
//	{
//		Debug.Log ("-------------------- HandleRequest");
//		if (result == null)
//		{
//			//			this.LastResponse = "Null Response\n";
//			//			LogView.AddLog(this.LastResponse);
//			Debug.Log ("Null Response");
//			return;
//		}
//
//		//		this.LastResponseTexture = null;
//
//		// Some platforms return the empty string instead of null.
//		if (!string.IsNullOrEmpty(result.Error))
//		{
//			//			this.Status = "Error - Check log for details";
//			//			this.LastResponse = "Error Response:\n" + result.Error;
//
//			Debug.Log ("Error Response:" + result.Error);
//			GirlGameConfigs.jarToast ("Invite Failed");
//
//		}
//		else if (result.Cancelled)
//		{
//			//			this.Status = "Cancelled - Check log for details";
//			//			this.LastResponse = "Cancelled Response:\n" + result.RawResult;
//			Debug.Log ("Cancelled Response:\n" + result.RawResult);
//			GirlGameConfigs.jarToast ("Invite Cancelled");
//		}
//		else if (!string.IsNullOrEmpty(result.RawResult))
//		{
//			//			this.Status = "Success - Check log for details";
//			//			this.LastResponse = "Success Response:\n" + result.RawResult;
//			Debug.Log ("Success Response:\n" + result.RawResult);
//
//			GirlGameConfigs.jarToast ("successfully Invited");
//
//#if firebasee
//            FirebaseAnalytics.LogEvent(
//				FirebaseAnalytics.EventShare,
//				new Parameter(FirebaseAnalytics.ParameterLevel, GameManagerSlither.levelNumber),
//				new Parameter(FirebaseAnalytics.ParameterCharacter, "InGame"+GameManagerSlither.selectedSnakeIndex));
//#endif
//		}
//		else
//		{
//			//			this.LastResponse = "Empty Response\n";
//			Debug.Log ("Empty Response");
//		}
//
//		//		LogView.AddLog(result.ToString());
//	}
//#endif
}
