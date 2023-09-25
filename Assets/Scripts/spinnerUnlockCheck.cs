using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class spinnerUnlockCheck : MonoBehaviour {
	public int index;
	public GameObject BuyBtn,SelectBtn,shareBtn;
	public static spinnerUnlockCheck instance;
	void Awake()
	{
		instance = this;
	}
//	public int[] 
	// Use this for initialization
	void OnEnable () {
		//		Debug.Log ("SpinnerUnlock OnEnable index="+index);
		upgradePage.BuyUnlockAllCheck += checkAllSpinnersBuy;
		setItemBtns (index-1);
	}
	void OnDisable()
	{
		upgradePage.BuyUnlockAllCheck -= checkAllSpinnersBuy;
	}
	public void checkAllSpinnersBuy()
	{
		//		Debug.Log ("SpinnerUnlock checkAllSpinnersBuy index="+index);

		if (index == 1) {
			BuyBtn.SetActive (false);
			SelectBtn.SetActive (true);
			shareBtn.SetActive (false);
		} else {
			string isUnlocked = PlayerPrefs.GetString (("s" + (index-1)), "false");
			//			Debug.Log ("SpinnerUnlock checkAllSpinnersBuy index="+index+":::isUnlocked="+isUnlocked);

			if (isUnlocked == "true") {
				BuyBtn.SetActive (false);
				SelectBtn.SetActive (true);
				shareBtn.SetActive (false);

			} else {
				BuyBtn.SetActive (true);
				SelectBtn.SetActive (false);
				shareBtn.SetActive (false);

				if (index == 4 || index == 8 || index == 11 || index == 18 ||index == 25||index == 35||index == 50||index == 60||index == 65) {
					BuyBtn.SetActive (false);
					SelectBtn.SetActive (false);
					shareBtn.SetActive (true);
				}
				if (upgradePage.mee) {
					BuyBtn.GetComponentInChildren<Text> ().text = "" + upgradePage.mee.spinnerPrices [index - 1];
				}
			}
		}
	}
	public void setItemBtns (int indexValue) {
		index = indexValue+1;

			if (index == 1) {
				BuyBtn.SetActive (false);
				SelectBtn.SetActive (true);
				shareBtn.SetActive (false);
			} else {
				string isUnlocked = PlayerPrefs.GetString (("s" + index), "false");
				if (isUnlocked == "true") {
					BuyBtn.SetActive (false);
					SelectBtn.SetActive (true);
					shareBtn.SetActive (false);

				} else {
					BuyBtn.SetActive (true);
					SelectBtn.SetActive (false);
					shareBtn.SetActive (false);

				if (index == 5 || index == 18) {
					BuyBtn.SetActive (false);
					SelectBtn.SetActive (false);
					shareBtn.SetActive (true);
				}
				if (upgradePage.mee) {
					BuyBtn.GetComponentInChildren<Text> ().text = "" + upgradePage.mee.spinnerPrices [index - 1];
				}
				}
			}		
	}
	public void shareClick()
	{
			AudioClipManager.Instance.Play (MenuSounds.Button);
        //btm2018
		//AdManager.instance.FacebookShare();

        PlayerPrefs.SetString (("s" + index), "true");
		Invoke ("SetButtons",2);
#if firebasee
        FirebaseAnalytics.LogEvent(
			FirebaseAnalytics.EventShare,
			new Parameter(FirebaseAnalytics.ParameterLevel, GameManagerSlither.levelNumber),
			new Parameter(FirebaseAnalytics.ParameterCharacter, "fidget"+GameManagerSlither.selectedSnakeIndex));
#endif
//		inviteClick ();
	}
	void SetButtons()
	{
		BuyBtn.SetActive (false);
		SelectBtn.SetActive (true);
		shareBtn.SetActive (false);
	}
//	public void inviteClick()
//	{
//        //		FB.
//        //		if (this.Button("Android Invite"))
//        //		{
//        //
//        //			this.Status = "Logged FB.AppEvent";
//#if btmfacebook
//        FB.Mobile.AppInvite(new Uri("https://fb.me/1514943851930474"), callback: this.HandleResult);
//#endif
//		//		}
//		//		else
//		//		{
//		//			PlayfabFBManager.mee.FBLogin ();
//		//		}
//		//		}
//		//
//		//		if (this.Button("Android Invite With Custom Image"))
//		//		{
//		//			this.Status = "Logged FB.AppEvent";
//		//			FB.Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), new Uri("http://i.imgur.com/zkYlB.jpg"), this.HandleResult);
//		//		}
//	}
//
//#if btmfacebook
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
//			PlayerPrefs.SetString (("s" + index), "true");
//			BuyBtn.SetActive (false);
//			SelectBtn.SetActive (true);
//			shareBtn.SetActive (false);
//
//
//			FirebaseAnalytics.LogEvent(
//				FirebaseAnalytics.EventShare,
//				new Parameter(FirebaseAnalytics.ParameterLevel, GameManagerSlither.levelNumber),
//				new Parameter(FirebaseAnalytics.ParameterCharacter, "fidget"+GameManagerSlither.selectedSnakeIndex));
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
	public void buySpinner()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);

//		AudioClipManager.Instance.Play (InGameSounds.CoinDeduct);

		int currectCoins = StoreManager.GetCoins ();
		if (currectCoins >= upgradePage.mee.spinnerPrices[index-1]) {
			StoreManager.ReduceCoins(upgradePage.mee.spinnerPrices[index-1]);
			PlayerPrefs.SetString (("s" + index), "true");
			BuyBtn.SetActive (false);
			SelectBtn.SetActive (true);
			shareBtn.SetActive (false);
			upgradePage.mee.displayCoins ();

		} else {
            ////btm2018
            //GirlGameConfigs.jarToast ("Not enough coins");

			//AdManager.instance.BuyItem (1, true);
        }
        upgradePage.mee.coinsTxt.text = "" + StoreManager.GetCoins ();
	}
	public void selectSpinner()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);

//		AudioClipManager.Instance.Play (InGameSounds.RestBtn);

//		spinCheck.Instance.selectedSpinner = index-1;
//		spinCheck.Instance.selectSpinner ();
		GameManagerSlither.selectedSnakeIndex=index-1;
		Debug.Log ("selectedSnakeIndex="+(GameManagerSlither.selectedSnakeIndex)+":::index="+index);
		upgradePage.mee.close ();
		PlayerPrefs.SetInt (StoreManager.SelectedSpinner, index-1);
	}

}
