using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsPage : MonoBehaviour {
	public static settingsPage mee;
	public GameObject leftHandTickMark,rightHandTickMark,floatingHandTickMark;
	public GameObject title,popUp,CloseBtn,soundOn,soundOff,promoBtn;
	private Vector3 titlePos;
	void Awake()
	{
		mee = this;
		titlePos = title.transform.localPosition;
		gameObject.SetActive (false);
	}

	void SetToggles ()
	{
		SETTINGUTILS.GetSaved_SoundStatus ();
		SETTINGUTILS.GetSaved_MusicStatus ();
		if (SETTINGUTILS.sound) {
			soundOn.transform.localScale = Vector3.one;
			soundOff.transform.localScale = Vector3.zero;
		} else {
			soundOn.transform.localScale = Vector3.zero;
			soundOff.transform.localScale = Vector3.one;
		}
	}
	private float timeStamp = 0.15f;
	public void SoundOnOff ()
	{
		SETTINGUTILS.sound = !SETTINGUTILS.sound;
		SETTINGUTILS.saveSoundKey ();

		SETTINGUTILS.music = !SETTINGUTILS.music;
		SETTINGUTILS.saveMusicKey ();
		Debug.Log ("sounds="+SETTINGUTILS.sound);
		if (SETTINGUTILS.sound) {
			iTween.Stop (soundOn);
			iTween.Stop (soundOff);
			soundOn.transform.localScale = Vector3.one;

			iTween.ScaleTo (soundOff, iTween.Hash ("x", 0, "y", 0, "time", timeStamp, "islocal", true, "easetyp", iTween.EaseType.easeInBack));
			iTween.ScaleFrom (soundOn, iTween.Hash ("x", 0, "y", 0, "time", timeStamp, "delay", timeStamp, "islocal", true, "easetyp", iTween.EaseType.spring));

		} else {
			iTween.Stop (soundOn);
			iTween.Stop (soundOff);
			soundOff.transform.localScale = Vector3.one;

			iTween.ScaleTo (soundOn, iTween.Hash ("x", 0, "y", 0, "time", timeStamp, "islocal", true, "easetyp", iTween.EaseType.easeInBack));
			iTween.ScaleFrom (soundOff, iTween.Hash ("x", 0, "y", 0, "time", timeStamp, "delay", timeStamp, "islocal", true, "easetyp", iTween.EaseType.spring));
		}
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
		AudioClipManager.Instance.Play (2);

		gameObject.SetActive (true);
		SetToggles ();
		int joyStickType = PlayerPrefs.GetInt (StoreManager.joyStickType, 2);
		setJoyStickType (joyStickType);	

		iTween.Stop (popUp);
		popUp.transform.localPosition = Vector3.zero+new Vector3(0,10,0);
		iTween.MoveFrom(popUp,iTween.Hash ("y", 1000, "time", 0.5f, "delay", 0,  "islocal", true,"easetype", iTween.EaseType.spring));

		iTween.Stop (title);
		title.transform.localScale = Vector3.one;
		iTween.ScaleFrom (title,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (promoBtn);
		promoBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom(promoBtn,iTween.Hash  ("x",0,"y", 0, "time", 0.3f, "delay", 0.4f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (CloseBtn);
		CloseBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom(CloseBtn,iTween.Hash  ("x",0,"y", 0, "time", 0.3f, "delay", 0.6f, "islocal", true, "easetype", iTween.EaseType.spring));

		if (BackButton.CurrentPopup != gameObject && BackButton.Instance!=null) {
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
	public void promoCodeClick()
	{
		Debug.Log ("promoCodeClick");
        //GirlGameConfigs.mee.usePromoCode ("Please Enter Promo Code");//btm2018
    }
	public void ControlsBtnClick()
	{
		gameObject.SetActive (false);
		ControlsPage.mee.open ();
	}
	public void privacyPolicyClick()
	{
		Application.OpenURL("http://timuz.com/mobilegames/privacypolicy.html");
	}


}
