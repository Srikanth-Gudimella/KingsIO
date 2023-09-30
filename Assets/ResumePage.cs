using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResumePage : MonoBehaviour {
	public static ResumePage instance;
	public GameObject popUp,BG,Continue;
	public Image timerRing;
	public Text timeCount;
	public GameObject Heart;
	void Awake()
	{
		instance = this;
		gameObject.SetActive (false);
	}
		
	public void Open()
	{
		//		AudioClipManager.Instance.Play (InGameSounds.ResultPage);
		BGSoundManager.Instance.PauseSound();
		gameObject.SetActive (true);
		timeCount.text = ""+5;
		timerRing.GetComponent<Image>().fillAmount = 0;

		IsWatchToContinueClicked = false;
		BG.GetComponent<Image> ().CrossFadeAlpha (0, 0.1f, true);
		BG.GetComponent<Image> ().CrossFadeAlpha (1, 0.4f, false);
		popUp.transform.localPosition = Vector3.zero+new Vector3(0,50,0);
//		iTween.MoveFrom(popUp,iTween.Hash ("x", -1000, "time", 0.5f, "delay", 0.4f,  "islocal", true,"easetype", iTween.EaseType.spring));
		iTween.MoveFrom(popUp,iTween.Hash ("y", -1000, "time", 0.5f, "delay", 0.4f,  "islocal", true,"easetype", iTween.EaseType.spring));

		iTween.Stop (Continue);
		Continue.transform.localScale = Vector3.one;
		iTween.ScaleFrom (Continue,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.8f, "islocal", true, "easetype", iTween.EaseType.spring,"oncomplete", "animateWatchBtn", "oncompletetarget" , this.gameObject));

		iTween.Stop(gameObject);
		iTween.ValueTo (gameObject, iTween.Hash ("from", 0, "to", 1, "time", 5f,"delay", 1f, "easetype", iTween.EaseType.linear, "onupdate", "FillBar"));

		iTween.Stop (Heart);
		iTween.ScaleFrom (Heart, iTween.Hash ("x", 0.9f,"y",0.9f, "time", 0.5f,"delay",0f, "islocal",true,"easetype", iTween.EaseType.linear,"looptype",iTween.LoopType.pingPong));

//		if (AdManager.instance) {
//			AdManager.instance.RunActions (AdManager.PageType.PreLF);
//		}
	}
	void FillBar(float value)
	{
		timerRing.GetComponent<Image>().fillAmount = value;
		timeCount.text = "" + (5 - (int)(value*5));
		if (value >= 1) {
			Debug.LogError ("---Open GameOverpage");
			if (!IsWatchToContinueClicked) {
				Invoke ("OpenGameOverPage", 1);
			} else {
				Invoke ("OpenGameOverPage", 2);
			}
		}
	}
	void OpenGameOverPage()
	{
		//ResultPage.mee.Open ();
		Close ();
	}
	void animateWatchBtn()
	{
		Debug.LogError ("--- animateWatchBtn");
//		iTween.ScaleFrom (Continue, iTween.Hash ("x", 0.9f,"y",0.9f, "time", 0.5f,"delay",0f, "islocal",true,"easetype", iTween.EaseType.linear,"looptype",iTween.LoopType.pingPong));

	}
	public void Close()
	{
		gameObject.SetActive (false);
	}
	bool IsWatchToContinueClicked;
	public void ContinueClick()
	{
		Debug.Log ("ContinueClick");
		IsWatchToContinueClicked=true;
//		if (AdManager.instance) {
//			AdManager.instance.ShowRewardVideoWithCallback ((result) => {
//				if (result) {
//					iTween.Stop (gameObject);
//					CancelInvoke ("OpenGameOverPage");
//					//reset
//					BGSoundManager.Instance.ResumeSound ();
//					gameObject.SetActive (false);
//				}
//			});
//		} else {
//			iTween.Stop (gameObject);
//			CancelInvoke ("OpenGameOverPage");
//			//reset
//			BGSoundManager.Instance.ResumeSound ();
//			gameObject.SetActive (false);
//		}

		iTween.Stop (gameObject);
		CancelInvoke ("OpenGameOverPage");
		//reset
		GameManagerSlither.instance.ResetGame();
//		BGSoundManager.Instance.ResumeSound ();
		gameObject.SetActive (false);
		
	}
	public void NoThanksClick()
	{
		Debug.Log ("NoThanksClick");
		iTween.Stop (gameObject);
		//ResultPage.mee.Open ();
	}
}
