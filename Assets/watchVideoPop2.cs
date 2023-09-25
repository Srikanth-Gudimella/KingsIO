using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watchVideoPop2 : MonoBehaviour {
	public GameObject popUp,yesBtn,noBtn;
	public static watchVideoPop2 mee;
	void Awake()
	{
		mee = this;
		gameObject.SetActive (false);
	}
	public void open()
	{
		AudioClipManager.Instance.Play (2);
		gameObject.SetActive (true);
		//		int joyStickType = PlayerPrefs.GetInt (StoreManager.joyStickType, 0);
		//		setJoyStickType (joyStickType);	

		iTween.Stop (popUp);
//		popUp.transform.localPosition = Vector3.zero;
		popUp.transform.localPosition = Vector3.zero+new Vector3(0,10,0);

		iTween.MoveFrom(popUp,iTween.Hash ("y", -1000, "time", 0.5f, "delay", 0,  "islocal", true,"easetype", iTween.EaseType.spring));

		iTween.Stop (yesBtn);
		yesBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (yesBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (noBtn);
		noBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom(noBtn,iTween.Hash  ("x",0,"y", 0, "time", 0.3f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.spring));
		if (BackButton.CurrentPopup != gameObject && BackButton.Instance) {
			BackButton.Instance.Add (gameObject);
			BackButton.CloseCurrentPopupEvent += close;
		}
	}
	public void close()
	{
		if (BackButton.Instance != null && BackButton.CurrentPopup != gameObject)
		{
			return;
		}
		AudioClipManager.Instance.Play (MenuSounds.Button);

		gameObject.SetActive (false);
		menuPageHandler.mee.open ();
		if (BackButton.Instance != null) {
			BackButton.Instance.Remove ();
			BackButton.CloseCurrentPopupEvent -= close;
		}
	}
	public void yesClick()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);
		//btm2018//btm2018
#if UNITY_EDITOR
		GameManagerSlither.startWithBigSize = true;
		Application.LoadLevel("GameScene");
#endif
		//AdManager.instance.ShowRewardVideoWithCallback((result)=>{
  //      if (result)
		//	{
		//		// go to ingame with big size
		//		Debug.LogError("------- start game with big size");
		//		GameManagerSlither.startWithBigSize=true;
		//		Application.LoadLevel("GameScene");
		//	}
		//	else
		//	{
		//		Debug.Log("Not Watched Video.....");
		//	}
		//}
		//);
//		GameManagerSlither.startWithBigSize=true;
//		Application.LoadLevel("GameScene");
	}
	public void noClick()
	{
		Application.LoadLevel("GameScene");
	}
}
