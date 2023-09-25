using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelUpPop : MonoBehaviour {

	public GameObject claimBtn,homeBtn;
	public Text RewardValueTxt;
	public GameObject popUp;
	public GameObject rewardPatch;
//	public GameObject inviteBtn;

	public static LevelUpPop mee;
	void Awake()
	{
		mee = this;
		gameObject.SetActive (false);
	}

	public void Open()
	{
		AudioClipManager.Instance.Play (InGameSounds.ChallengeComplete);
		gameObject.SetActive (true);
        //btm2018


		gameObject.transform.localPosition = Vector3.zero;

		RewardValueTxt.text =100+"";

		iTween.MoveFrom(popUp,iTween.Hash ("y", -1000, "time", 0.5f, "delay", 0,  "islocal", true,"easetype", iTween.EaseType.spring));

		iTween.Stop (rewardPatch);
		rewardPatch.transform.localScale = Vector3.one;
		iTween.ScaleFrom (rewardPatch,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.spring));


		iTween.Stop (claimBtn);
		claimBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (claimBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

//		iTween.Stop (homeBtn);
//		homeBtn.transform.localScale = Vector3.one;
//		iTween.ScaleFrom (homeBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.2f, "islocal", true, "easetype", iTween.EaseType.spring));


//		iTween.Stop (inviteBtn);
//		inviteBtn.transform.localScale = new Vector3(-1,1,1);
//		iTween.ScaleFrom (inviteBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.4f, "islocal", true, "easetype", iTween.EaseType.spring));



		Debug.Log ("------ result page levelNumber="+(GameManagerSlither.levelNumber));
        //btm2018
        /*
		GirlGameConfigs.mee.AnalyticsLevelUp (GameManagerSlither.levelNumber,GameManagerSlither.selectedSnakeIndex,GameManagerSlither.myPlayerMass);
		GirlGameConfigs.mee.showRotationAds (GameManagerSlither.levelNumber, AdsPageType.lc);*/

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
    public void claimClick()
	{
		StoreManager.AddCoins (100);
		int playedCountForUpgrade = PlayerPrefs.GetInt("playedCountForUpgrade", 0);
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

	public void homeBtnClick()
	{

		Debug.Log("homeBtnClick");

		Application.LoadLevel ("Menu");

	}

}
