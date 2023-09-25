using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPage : MonoBehaviour {
	public static startPage mee;
	public GameObject eatObj,bigObj,smallObj,conquerBtn;
	void Awake()
	{
		mee = this;

	}
	// Use this for initialization 
	void Start () {
		if (PlayerPrefs.HasKey ("HelpFirstTime")) {
			gameObject.SetActive (false);



			int playedCountForUpgrade=PlayerPrefs.GetInt ("playedCountForUpgrade",0);
			GameManagerSlither.levelNumber = PlayerPrefs.GetInt ("playedCount",0);

//			Debug.Log ("lvlNUm="+GameManager.levelNumber+":::playedCount="+playedCountForUpgrade);
			if (GameManagerSlither.levelNumber==2 && playedCountForUpgrade == 2) {
				upgradePage.mee.open ();
				PlayerPrefs.SetInt ("playedCountForUpgrade",0);

			} 
			else if (playedCountForUpgrade == 5) {
				upgradePage.mee.open ();
				PlayerPrefs.SetInt ("playedCountForUpgrade",0);

			} else {
				menuPageHandler.mee.open ();
			}
		} else {
			menuPageHandler.mee.close ();
			PlayerPrefs.SetString ("HelpFirstTime","true");

			iTween.MoveFrom (eatObj,iTween.Hash("x",-1000,"time",0.5f,"delay",0f,"islocal",true,"easetype",iTween.EaseType.spring));
			iTween.MoveFrom (bigObj,iTween.Hash("x",1000,"time",0.5f,"delay",0.2f,"islocal",true,"easetype",iTween.EaseType.spring));
			iTween.MoveFrom (smallObj,iTween.Hash("x",-1000,"time",0.5f,"delay",0.4f,"islocal",true,"easetype",iTween.EaseType.spring));

			iTween.ScaleFrom (conquerBtn, iTween.Hash ("x", 0, "y", 0, "time", 0.5f, "delay", 0.7f, "islocal", true, "easetype", iTween.EaseType.spring));

		}
	}
	
	public void conquerClick()
	{
		gameObject.SetActive (false);
		menuPageHandler.mee.open ();
	}
}
