using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TargetCompletePop : MonoBehaviour {
	public GameObject nextBtn,homeBtn;
	public Text challengeResultTxt;
	public GameObject popUp;
//	public GameObject inviteBtn;

	public static TargetCompletePop mee;
	void Awake()
	{
		mee = this;
		gameObject.SetActive (false);
	}

	public void Open()
	{
		AudioClipManager.Instance.Play (InGameSounds.ChallengeComplete);
		gameObject.SetActive (true);


		gameObject.transform.localPosition = Vector3.zero;
		Debug.Log ("---- Target Complete challengeModeType="+(GameManagerSlither.challengeModeType));
		switch (GameManagerSlither.challengeModeType) {
		case 1:
			string minutes = Mathf.Floor (GameManagerSlither.instance.currentPlayTime / 60).ToString ("00");
			string seconds = Mathf.Floor (GameManagerSlither.instance.currentPlayTime % 60).ToString ("00");
//			//				timeBar.fillAmount = levelTime / defaultTime;
			challengeResultTxt.text = "Time : "+(minutes + ":" + seconds);

			break;
		case 2:
			challengeResultTxt.text = "Size : "+GameManagerSlither.myPlayerMass+"";

			break;
		case 3:
			challengeResultTxt.text = "Kills : "+GameManagerSlither.instance.enemiesKilledCount+"";
			break;
		}

		iTween.MoveFrom(popUp,iTween.Hash ("y", -1000, "time", 0.5f, "delay", 0,  "islocal", true,"easetype", iTween.EaseType.spring));

		iTween.Stop (nextBtn);
		nextBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (nextBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.35f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (homeBtn);
		homeBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (homeBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.25f, "islocal", true, "easetype", iTween.EaseType.spring));

//		iTween.Stop (inviteBtn);
//		inviteBtn.transform.localScale = new Vector3(-1,1,1);
//		iTween.ScaleFrom (inviteBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.4f, "islocal", true, "easetype", iTween.EaseType.spring));

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
	public void nextClick()
	{
//		Application.LoadLevel ("Menu");
		playChallengeMode ();
	}
	public void playChallengeMode()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);



		GameManagerSlither.isOfflineMode = true;
		GameManagerSlither.isForceOfflineMode = false;

		GameManagerSlither.challengeModeType = PlayerPrefs.GetInt ("challengeType",1);
//		playerController.isStartMoving=false;
		Application.LoadLevel ("GameScene");

		Debug.Log ("------------ InGame challenge mode="+GameManagerSlither.challengeModeType);
	}
	public void inviteClick()
	{

//		Debug.Log("FB inviteclick isloggedin="+FB.IsLoggedIn);

//		FB.Mobile.AppInvite(new Uri("https://fb.me/1514943851930474"), callback: this.HandleResult);

	}
	public void homeBtnClick()
	{

		Debug.Log("homeBtnClick");

		Application.LoadLevel ("Menu");

	}
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
//
//			FirebaseAnalytics.LogEvent(
//				FirebaseAnalytics.EventShare,
//				new Parameter(FirebaseAnalytics.ParameterLevel, GameManagerSlither.levelNumber),
//				new Parameter(FirebaseAnalytics.ParameterCharacter, "InGame"+GameManagerSlither.selectedFidgetIndex));
//		}
//		else
//		{
//			//			this.LastResponse = "Empty Response\n";
//			Debug.Log ("Empty Response");
//		}
//
//		//		LogView.AddLog(result.ToString());
//	}
}
