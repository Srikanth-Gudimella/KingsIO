using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ControlsPage : MonoBehaviour {
	public static ControlsPage mee;
	public GameObject leftHandTickMark,rightHandTickMark,floatingHandTickMark;
	public GameObject title,popUp,OkBtn;
	private Vector3 titlePos;
	void Awake()
	{
		mee = this;
		titlePos = title.transform.localPosition;
		gameObject.SetActive (false);
	}


	private void setJoyStickType(int type)
	{
		switch (type) {
		case 0:
			leftHandTickMark.SetActive (true);
			rightHandTickMark.SetActive (false);
			floatingHandTickMark.SetActive(false);
			break;
		case 1:
			leftHandTickMark.SetActive (false);
			rightHandTickMark.SetActive (true);
			floatingHandTickMark.SetActive(false);
			break;
		case 2:
			leftHandTickMark.SetActive (false);
			rightHandTickMark.SetActive (false);
			floatingHandTickMark.SetActive(true);
			break;
		}	
		GameManagerSlither.joyStickType = type;
		PlayerPrefs.SetInt (StoreManager.joyStickType, type);
		//		Debug.Log ("setJoyStickType ="+type);
	}
	public void leftHandToggleClick()
	{
		setJoyStickType (0);		
	}
	public void rightHandToggleClick()
	{
		setJoyStickType (1);		
	}
	public void floatHandToggleClick()
	{
		setJoyStickType (2);		
	}

	public void open()
	{
		Debug.Log ("---- controlsPage open");
		AudioClipManager.Instance.Play (2);

		gameObject.SetActive (true);
		int joyStickType = PlayerPrefs.GetInt (StoreManager.joyStickType, 2);
		setJoyStickType (joyStickType);	

		iTween.Stop (popUp);
		popUp.transform.localPosition = Vector3.zero+new Vector3(0,10,0);
		iTween.MoveFrom(popUp,iTween.Hash ("y", 1000, "time", 0.5f, "delay", 0,  "islocal", true,"easetype", iTween.EaseType.spring));

		iTween.Stop (title);
		title.transform.localScale = Vector3.one;
		iTween.ScaleFrom (title,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (OkBtn);
		OkBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom(OkBtn,iTween.Hash  ("x",0,"y", 0, "time", 0.3f, "delay", 0.4f, "islocal", true, "easetype", iTween.EaseType.spring));


		if (BackButton.CurrentPopup != gameObject && BackButton.Instance!=null) {
			BackButton.Instance.Add (gameObject);
			BackButton.CloseCurrentPopupEvent += close;
		}
	}
	public void close()
	{
//		if (BackButton.Instance != null && BackButton.CurrentPopup != gameObject)
//		{
//			return;
//		}
		AudioClipManager.Instance.Play (InGameSounds.Button);

		if (SceneManager.GetActiveScene ().name == "GameScene") {
			PlayerPrefs.SetString ("HelpFirstTime","true");
			Virtualjoystick.mee.SetJoyStickType ();
			//GameManagerSlither.instance.OnPlayButton ();//Srikanth
		} else {
			menuPageHandler.mee.open ();
		}
		gameObject.SetActive (false);



//		if (BackButton.Instance != null) {
//			BackButton.Instance.Remove ();
//			BackButton.CloseCurrentPopupEvent -= close;
//		}
	}


}
