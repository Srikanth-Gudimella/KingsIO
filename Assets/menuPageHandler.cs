using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
public class menuPageHandler : MonoBehaviour {

	public static menuPageHandler mee;
	public GameObject title, LBBtn,changeFidget, settings, rate, share, playOnlineBtn, playAIBtn,playChallengeBtn, details;
	public Vector3 titlePos,LBBtnPos,changeFidgetPos,settingsPos,ratePos,sharePos,inviteBtnPos,detailsPos;
	public static string playerName;
	public InputField nameEntryObj;
	public Text finalScore,bestScore;
	public Text PlayerLevelTxt;
	public GameObject leaderBoardContent;
	public GameObject playerDetail;
	public GameObject leaderBoardObj;
	public GameObject leaderBoardLoading;
	public GameObject myLeaderBoardObj;
	public GameObject titleParticleEffect;


	public Text dummyPlayfabID,dummyCountryID;

	public GameObject loadingImg;
	public GameObject[] players;
	void Awake()
	{
//		PlayerPrefs.SetInt (StoreManager.KEY_COINS,10000);
		mee = this;
		SetToggles ();
		titlePos = title.transform.localPosition;
		LBBtnPos = LBBtn.transform.localPosition;
		changeFidgetPos = changeFidget.transform.localPosition;
		settingsPos = settings.transform.localPosition;
		ratePos = rate.transform.localPosition;
		sharePos = share.transform.localPosition;
//		inviteBtnPos = inviteBtn.transform.localPosition;
		detailsPos = details.transform.localPosition;
//		gameObject.SetActive (false);
		leaderBoardObj.SetActive (false);
		loadingImg.SetActive (false);
//		if ((PlayerPrefs.GetFloat ("version", 0) != GameManagerSlither.version)) {
//			Debug.LogError ("------------- Menu version test");
//			PlayerPrefs.SetFloat("version",GameManagerSlither.version);
//		}


	}
	void Start()
	{
//		testAnim.Play ();
//		Open ();
//		for (int i = 0; i < 30; i++) {
//			PlayerPrefs.SetString (("s" + i), "true");
//		}
		Debug.Log ("Menu start 111111");

		AudioClipManager.Instance.Play (MenuSounds.BG);
		BGSoundManager.Instance.SetVolume (100);
		nameEntryObj.text = PlayerPrefs.GetString ("myName","");
		GameManagerSlither.levelNumber = PlayerPrefs.GetInt ("playedCount",0);

		finalScore.text="Your final size was: "+GameManagerSlither.myPlayerMass;
		bestScore.text ="Your best size ever was: "+ PlayerPrefs.GetInt (StoreManager.Best_Score,GameManagerSlither.myPlayerMass);
		GameManagerSlither.myPlayerMass = 0;
		GameManagerSlither.selectedSnakeIndex = PlayerPrefs.GetInt (StoreManager.SelectedSpinner, 0);
		GameManagerSlither.joyStickType = PlayerPrefs.GetInt (StoreManager.joyStickType, 2);// defalut is floating joystick

		GameManagerSlither.startWithBigSize = false;
        //		if (GirlGameConfigs.mee) {
        //			GirlGameConfigs.mee.showRotationAds (1, AdsPageType.ingame);
        //			NewUI.Instance.ShowHeader ();
        //		}
		#if Adsetup_ON
		if (AdManager.instance) {
			Debug.Log ("is cam from play area="+(GameManagerSlither.cameFromPlayArea));
			AdManager.instance.RunActions (AdManager.PageType.Menu,GameManagerSlither.levelNumber);
		}
		#endif
        foreach (Transform child in leaderBoardContent.transform) {
			child.gameObject.SetActive (false);
			//			GameObject.Destroy(child.gameObject);
		}
		myLeaderBoardObj.SetActive (false);
		leaderBoardLoading.SetActive (true);


//		Sprite flagImg = Resources.Load<Sprite>("IN");
//		Flag.GetComponent<Image> ().sprite = flagImg;


//		dummyPlayfabID.text = GirlGameConfigs.mee.myPlayFabID;
//		dummyCountryID.text=(PlayerPrefs.GetString ("playerCountry", ""));

//		playerController.isStartMoving=false;

		Debug.Log ("Menu start 22222222");
		//start page logic added here
		int playedCountForUpgrade=PlayerPrefs.GetInt ("playedCountForUpgrade",0);
		GameManagerSlither.levelNumber = PlayerPrefs.GetInt ("playedCount",0);

		//			Debug.Log ("lvlNUm="+GameManager.levelNumber+":::playedCount="+playedCountForUpgrade);
//		if (GameManagerSlither.levelNumber==2 && playedCountForUpgrade == 2) {
////			upgradePage.mee.open ();
//			Application.LoadLevel("UpgradePage");
//			PlayerPrefs.SetInt ("playedCountForUpgrade",0);
//
//		} 
//		else if (playedCountForUpgrade == 5) {
////			upgradePage.mee.open ();
//			Application.LoadLevel("UpgradePage");
//			PlayerPrefs.SetInt ("playedCountForUpgrade",0);
//
//		} else {
			playAIBtn.SetActive(false);
			playOnlineBtn.SetActive(false);
			playChallengeBtn.SetActive(false);
			LBBtn.SetActive(false);
			changeFidget.SetActive(false);
			settings.SetActive(false);
			rate.SetActive(false);
			share.SetActive(false);
			details.SetActive(false);


			titleParticleEffect.SetActive (false);
			Debug.Log ("----- MenuPage start camfromPlay="+(GameManagerSlither.cameFromPlayArea));
			if (!GameManagerSlither.cameFromPlayArea) {
				Invoke ("showTitleParticle", 0.5f);
				iTween.Stop (title);
				title.transform.localPosition = titlePos;
				iTween.MoveFrom (title, iTween.Hash ("y", 1000f, "time", 0.8f, "delay", 0 + dummyDelay, "islocal", true, "easetype", iTween.EaseType.easeOutBack));
				Invoke ("open", 0.8f);
			} else {
				open ();
			}
//		}
//		for (int i = 0; i < snakes.Length; i++) {
//			changeSnakeTexture (Random.Range(0,snakeMaterial1.Length));
//		}
	}

	void SetToggles ()
	{
		SETTINGUTILS.GetSaved_SoundStatus ();
		SETTINGUTILS.GetSaved_MusicStatus ();
	}
	public void setPlayerName(string nameOfPlayer)
	{
        //btm2018
        /*if ((!PlayerPrefs.HasKey ("myName") && GirlGameConfigs.mee!=null)|| (PlayerPrefs.GetFloat("version",0)!=GameManagerSlither.version) ||(PlayerPrefs.GetString ("myName","")!=nameOfPlayer)) {
            if(GirlGameConfigs.mee)
			GirlGameConfigs.mee.PushDisplayname (nameOfPlayer);	
		}*/
    }

	public void InputFieldEndEntry()
	{
		Debug.Log ("Input FieldEnd Entry");
		switch(PlayType)
		{
		case 1:
			playOnline ();
			break;
		case 2:
			playAgainstAI ();
			break;
		case 3:
			playChallengeMode ();
			break;
		}
	}
	int PlayType=0;
    public void playOnline()
	{
		Debug.Log ("--- PlayOnline click");
		AudioClipManager.Instance.Play (MenuSounds.Button);

		playerName = nameEntryObj.text;


		if (nameEntryObj.text.Length < 3) {
			PlayType = 1;
            //			Debug.Log ("Name should contains atleat 3 characters");
            //btm2018
            //GirlGameConfigs.jarToast ("Please Enter Nickname");
			nameEntryObj.ActivateInputField();
            return;
		}
		setPlayerName (playerName);
		PlayerPrefs.SetString ("myName",playerName);
        //btm2018
        /*if (!GirlGameConfigs.mee.isWifi_OR_Data_Availble ()) {
			GirlGameConfigs.jarToast ("Please check your internet connection..");
			return;
		}*/

        //		GameManagerSlither.isOfflineMode = false;//original
        GameManagerSlither.isOfflineMode = true;
		GameManagerSlither.isForceOfflineMode = true;


		GameManagerSlither.challengeModeType = 0;
		int playedCountForWatchVideo=PlayerPrefs.GetInt ("playedCountForVideo",0);
		Debug.Log("playedCountForWatchVideo="+(playedCountForWatchVideo));
		if (playedCountForWatchVideo >= 3) {
			watchVideoPop2.mee.open ();
			PlayerPrefs.SetInt ("playedCountForVideo",0);
		} else {
			Application.LoadLevel ("GameScene");
			loadingImg.SetActive (true);
		}

	}
	public void playAgainstAI()
	{
		Debug.Log ("--- PlayAgainstAI click");
		AudioClipManager.Instance.Play (MenuSounds.Button);

		playerName = nameEntryObj.text;
		if (nameEntryObj.text.Length < 3) {
			PlayType = 2;

			Debug.Log ("Name should contains atleat 3 characters");
            //btm2018
            //GirlGameConfigs.jarToast ("Please Enter Nickname");
            return;
		}
		setPlayerName (playerName);

		PlayerPrefs.SetString ("myName",playerName);
//		Debug.Log ("play with AI myname="+(PlayerPrefs.GetString ("myName","Fidget")));

		GameManagerSlither.isOfflineMode = true;
		GameManagerSlither.isForceOfflineMode = false;

		GameManagerSlither.challengeModeType = 0;
		int playedCountForWatchVideo=PlayerPrefs.GetInt ("playedCountForVideo",0);
		Debug.Log("playedCountForWatchVideo="+(playedCountForWatchVideo));
		if (playedCountForWatchVideo >= 3) {  
			watchVideoPop2.mee.open ();
			PlayerPrefs.SetInt ("playedCountForVideo",0);
		} else {
			Application.LoadLevel ("GameScene");
			loadingImg.SetActive (true);
		}

	}
	public void playChallengeMode()
	{
		Debug.Log ("--- PlayChallengeMode click");
		AudioClipManager.Instance.Play (MenuSounds.Button);

		playerName = nameEntryObj.text;
		if (nameEntryObj.text.Length < 3) {
			PlayType = 3;

            Debug.Log ("Name should contains atleat 3 characters");
            ////btm2018
            //GirlGameConfigs.jarToast ("Please Enter Nickname");
            return;
		}
		setPlayerName (playerName);

		PlayerPrefs.SetString ("myName",playerName);
		//		Debug.Log ("play with AI myname="+(PlayerPrefs.GetString ("myName","Fidget")));

		GameManagerSlither.isOfflineMode = true;
		GameManagerSlither.isForceOfflineMode = false;

		GameManagerSlither.challengeModeType = PlayerPrefs.GetInt ("challengeType",1);
		int playedCountForWatchVideo=PlayerPrefs.GetInt ("playedCountForVideo",0);
		Debug.Log("playedCountForWatchVideo="+(playedCountForWatchVideo));
		if (playedCountForWatchVideo >= 3) {
			watchVideoPop2.mee.open ();
			PlayerPrefs.SetInt ("playedCountForVideo",0);
		} else {
			Application.LoadLevel ("GameScene");
			loadingImg.SetActive (true);
		}
		Debug.Log ("------------ menupage challenge mode="+GameManagerSlither.challengeModeType);

	}
	public void changeFidgetClick()
	{
		Debug.Log ("chagneFidget Click");
//		close ();
//		upgradePage.mee.open ();
		AudioClipManager.Instance.Play (MenuSounds.Button);
		Application.LoadLevel("UpgradePage");
        //btm2018
        /*if (GirlGameConfigs.mee != null) {
			GirlGameConfigs.mee.AnalyticsLevelUp (GameManagerSlither.levelNumber, GameManagerSlither.selectedSnakeIndex, GameManagerSlither.myPlayerMass);
		}*/
    }
    public void rateClick()
	{
		Debug.Log ("rate Click");
		AudioClipManager.Instance.Play (MenuSounds.Button);
		//AdManager.instance.ShowRatePopUp ();
        //		if (PlayerPrefs.GetString ("rated", "false") == "false" && GirlGameConfigs.ratingCoins != 0) {
        //btm2018
//        RatePopup.Instance.Open (GirlGameConfigs.mee.gameName, GirlGameConfigs.rateMessage);
        //		} else {
        //			GirlGameConfigs.mee.rateNow();
        //		}
    }
    public void shareClick()
	{
		Debug.Log ("share Click");
//		SharePopup.Instance.Open (GirlGameConfigs.mee.gameName, GirlGameConfigs.shareMessage);
		AudioClipManager.Instance.Play (MenuSounds.Button);
		//AdManager.instance.FacebookShare ();
        //GirlGameConfigs.mee.shareFB();//btm2018
    }
    public void settingsClick()
	{
		Debug.Log ("settings Click");
		AudioClipManager.Instance.Play (MenuSounds.Button);
		settingsPage.mee.open ();
		close ();
	}
	public bool isOpenLB = false;
	public void leaderBoardClick()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);
		leaderBoardObj.SetActive (true);
		isOpenLB = true;
        //btm2018
        //GirlGameConfigs.mee.getPlayersForLBWorld("size");
        //GirlGameConfigs.mee.getPlayersForLBWorld2("size");
    }
    public void hideLeaderBoard()
	{
		isOpenLB = false;
		leaderBoardObj.SetActive (false);
	}

	void showTitleParticle()
	{
		titleParticleEffect.SetActive (true);
	}
	float dummyDelay=0.6f;
	public void open()
	{
		Debug.Log ("menuPageHandlerOpen="+(GameManagerSlither.selectedSnakeIndex));
		gameObject.SetActive (true);
		for (int i = 0; i < players.Length; i++) {
			players [i].SetActive (false);
		}
		players [GameManagerSlither.selectedSnakeIndex].SetActive (true);
		PlayerLevelTxt.text="Level "+PlayerPrefs.GetInt ("PlayerLevel", 1);

//		SetToggles ();

//		iTween.RotateFrom (title, iTween.Hash ("z", 150,"time", 0.7f, "delay", 0f, "islocal", true, "easetype", iTween.EaseType.linear));
//		return;
		playAIBtn.SetActive(true);
		playOnlineBtn.SetActive(true);
		playChallengeBtn.SetActive(true);
//		LBBtn.SetActive(true);
		changeFidget.SetActive(true);
		settings.SetActive(true);
		rate.SetActive(true);
		share.SetActive(true);
		details.SetActive(true);

		iTween.Stop (playOnlineBtn);
		playOnlineBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (playOnlineBtn, iTween.Hash ("x",0,"y",0,"time", 0.5f, "delay", 0.1f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (playAIBtn);
		playAIBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (playAIBtn, iTween.Hash ("x",0,"y",0,"time", 0.5f, "delay", 0.2f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));




//		iTween.Stop (playOnlineBtn);
//		playOnlineBtn.transform.localScale = Vector3.one;
//		iTween.ScaleFrom (playOnlineBtn, iTween.Hash ("x",0,"y",0,"time", 0.5f, "delay", 0.6f, "islocal", true, "easetype", iTween.EaseType.spring));
//
		iTween.Stop (playChallengeBtn);
		playChallengeBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (playChallengeBtn, iTween.Hash ("x",0,"y",0,"time", 0.5f, "delay", 0.25f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));

//		iTween.Stop (LBBtn);
//
//		iTween.MoveFrom (LBBtn, iTween.Hash ("y", 500f,  "time", 0.4f, "delay", 0f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));


		iTween.Stop (settings);
		//		settings.transform.localPosition = settingsPos;
		iTween.MoveFrom (settings, iTween.Hash ("y", 500f,  "time", 0.4f, "delay", 0.1f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (changeFidget);
		changeFidget.transform.localScale = Vector3.one;
//		changeFidget.transform.localPosition = changeFidgetPos;
//		changeFidget.transform.localPosition = new Vector3(changeFidget.transform.parent.position.x-145,changeFidget.transform.parent.position.y-25,0);

//		iTween.MoveFrom (changeFidget, iTween.Hash ("x", -1000f,  "time", 0.4f, "delay", 0.2f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));
		iTween.ScaleFrom (changeFidget, iTween.Hash ("x", 0,"y",0,  "time", 0.4f, "delay", 0.2f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));


		if (PlayerPrefs.GetString ("IsRated") != "true") {
			iTween.Stop (rate);
//		rate.transform.localPosition = ratePos;
			iTween.MoveFrom (rate, iTween.Hash ("y", -500f, "time", 0.4f, "delay", 0.2f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));
		} else {
			rate.SetActive (false);
		}
		iTween.Stop (share);
//		share.transform.localPosition = sharePos;
		iTween.MoveFrom (share, iTween.Hash ("y", -500f,  "time", 0.4f, "delay", 0.1f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));

//		iTween.Stop (inviteBtn);
		//		share.transform.localPosition = sharePos;
//		iTween.MoveFrom (inviteBtn, iTween.Hash ("y", -500f,  "time", 0.4f, "delay", 0.8f, "islocal", true, "easetype", iTween.EaseType.spring));


		iTween.Stop (details);
//		details.transform.localPosition = detailsPos;
		details.transform.localScale=Vector3.one;
		iTween.MoveFrom (details, iTween.Hash ("y", 0f,  "time", 0.4f, "delay", 0f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));
		iTween.ScaleFrom (details, iTween.Hash ("x",0f,"y", 0f,  "time", 0.4f, "delay", 0f+dummyDelay, "islocal", true, "easetype", iTween.EaseType.spring));

        //		setLeaderBoard ();

        //		upgradePage.mee.open ();


        //		foreach (Transform child in leaderBoardContent.transform) {
        //			child.gameObject.SetActive (false);
        //		}
        //		leaderBoardLoading.SetActive (true);

    }
    //	private bool gotLBData;
#if btmfacebook
    public void setLeaderBoardSelf(GetLeaderboardAroundPlayerResult playersList)
	{
//		leaderBoardLoading.SetActive(false);
//		for (int i = 0; i < playersList.Leaderboard.Count; i++) {
		GameObject player=myLeaderBoardObj;
			player.SetActive (true);

			if(!string.IsNullOrEmpty(playersList.Leaderboard [0].DisplayName))
			{
				string[] playerName=playersList.Leaderboard [0].DisplayName.Split('%');
				string countryID = string.Empty;
				if (playerName.Length >= 2) {
					player.GetComponent<playerDetails> ().playerName.text = playerName [0];

					countryID = playerName [1];
					Sprite flagImg = null;
					flagImg = Resources.Load<Sprite>("Flags/"+countryID);
					if (flagImg != null) {
						player.GetComponent<playerDetails> ().flagImg.GetComponent<Image> ().sprite = flagImg;
					}

				} else if (playerName.Length == 1) {
					//				player.GetComponent<playerDetails> ().playerName.text = playersList.Leaderboard [i].DisplayName;
					player.GetComponent<playerDetails> ().playerName.text = playerName [0];

				} else {
					player.GetComponent<playerDetails> ().playerName.text = playersList.Leaderboard [0].DisplayName;
				}
			}
			else
			{
				player.GetComponent<playerDetails> ().playerName.text = playersList.Leaderboard [0].PlayFabId;

			}
			player.GetComponent<playerDetails> ().playerSize.text = playersList.Leaderboard [0].StatValue+"";
		player.GetComponent<playerDetails> ().Index.text = "" + (playersList.Leaderboard [0].Position+ 1);

//		}

	}
	public void setLeaderBoard(GetLeaderboardResult playersList)
	{
//		foreach (Transform child in leaderBoardContent.transform) {
//			child.gameObject.SetActive (false);
////			GameObject.Destroy(child.gameObject);
//		}
		leaderBoardLoading.SetActive(false);
		for (int i = 0; i < playersList.Leaderboard.Count; i++) {
			GameObject player=leaderBoardContent.transform.GetChild(i).gameObject;
			player.SetActive (true);

			if(!string.IsNullOrEmpty(playersList.Leaderboard [i].DisplayName))
			{
				string[] playerName=playersList.Leaderboard [i].DisplayName.Split('%');
				string countryID = string.Empty;
				if (playerName.Length >= 2) {
					player.GetComponent<playerDetails> ().playerName.text = playerName [0];

					countryID = playerName [1];
					Sprite flagImg = null;
					flagImg = Resources.Load<Sprite>("Flags/"+countryID);
//					Debug.Log ("index n flag="+i+":::"+countryID);
					if (flagImg != null) {
						player.GetComponent<playerDetails> ().flagImg.GetComponent<Image> ().sprite = flagImg;
					}

				} else if (playerName.Length == 1) {
	//				player.GetComponent<playerDetails> ().playerName.text = playersList.Leaderboard [i].DisplayName;
					player.GetComponent<playerDetails> ().playerName.text = playerName [0];

				} else {
					player.GetComponent<playerDetails> ().playerName.text = playersList.Leaderboard [i].DisplayName;
				}
			}
			else
			{
				player.GetComponent<playerDetails> ().playerName.text = playersList.Leaderboard [i].PlayFabId;

			}
			player.GetComponent<playerDetails> ().playerSize.text = playersList.Leaderboard [i].StatValue+"";
			player.GetComponent<playerDetails> ().Index.text = "" + (i + 1);

		}
	}
#endif
    public void close()
	{
		gameObject.SetActive (false);
	}

//	public void inviteClick()
//	{
//
//		//Debug.Log("FB inviteclick isloggedin="+FB.IsLoggedIn);
//#if btmfacebook
//			FB.Mobile.AppInvite(new Uri("https://fb.me/1514943851930474"), callback: this.HandleResult);
//#endif
//
//	}
//#if btmfacebook
//	protected void HandleResult(IResult result)
//	{
//		Debug.Log ("-------------------- HandleRequest");
//		if (result == null)
//		{
////			this.LastResponse = "Null Response\n";
////			LogView.AddLog(this.LastResponse);
//			Debug.Log ("Null Response");
//			return;
//		}
//
////		this.LastResponseTexture = null;
//
//		// Some platforms return the empty string instead of null.
//		if (!string.IsNullOrEmpty(result.Error))
//		{
////			this.Status = "Error - Check log for details";
////			this.LastResponse = "Error Response:\n" + result.Error;
//
//			Debug.Log ("Error Response:" + result.Error);
//			GirlGameConfigs.jarToast ("Invite Failed");
//
//		}
//		else if (result.Cancelled)
//		{
////			this.Status = "Cancelled - Check log for details";
////			this.LastResponse = "Cancelled Response:\n" + result.RawResult;
//			Debug.Log ("Cancelled Response:\n" + result.RawResult);
//			GirlGameConfigs.jarToast ("Invite Cancelled");
//		}
//		else if (!string.IsNullOrEmpty(result.RawResult))
//		{
////			this.Status = "Success - Check log for details";
////			this.LastResponse = "Success Response:\n" + result.RawResult;
//			Debug.Log ("Success Response:\n" + result.RawResult);
//
//			GirlGameConfigs.jarToast ("successfully Invited");
//
//
//			FirebaseAnalytics.LogEvent(
//				FirebaseAnalytics.EventShare,
//				new Parameter(FirebaseAnalytics.ParameterLevel, GameManagerSlither.levelNumber),
//				new Parameter(FirebaseAnalytics.ParameterCharacter, "menu"+GameManagerSlither.selectedSnakeIndex));
//		}
//		else
//		{
////			this.LastResponse = "Empty Response\n";
//			Debug.Log ("Empty Response");
//		}
//
////		LogView.AddLog(result.ToString());
//	}
//#endif
}
