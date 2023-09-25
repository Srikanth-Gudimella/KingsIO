using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class BackButton : MonoBehaviour
{

	public static GameObject CurrentPopup;
	public static List<GameObject> CurrentlyOpenedPopups = new List<GameObject> ();

	public delegate void CloseCurrentPopup ();
	public static event CloseCurrentPopup CloseCurrentPopupEvent;


	#region "Getter"
	private static BackButton _instance;
	public static BackButton Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<BackButton> ();
			}
			return _instance;
		}
	}

	private bool canClickEscape;

	#endregion
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}
	//btm2018
	/*void Update ()
	{
		if (!canClickEscape) {
			return;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Application.loadedLevelName != "Menu") {
				if (CloseCurrentPopupEvent != null) {
					CloseCurrentPopupEvent ();
				} else {
					using (AndroidJavaClass javaClass = new AndroidJavaClass ("com.timuz.moregames.webViewClass")) {
						if (TimuzMoreGames.mee.IsExitShowing) {
							Debug.Log ("Is Showing already.. hiding now");
							javaClass.CallStatic ("hideWebView");
							TimuzMoreGames.mee.IsExitShowing = false;
						} else {
							if (TimuzMoreGames.ExitPageURL != "") {
								Debug.Log ("Is hide already.. showing now");
								javaClass.CallStatic ("showWebView", TimuzMoreGames.ExitPageURL, GirlGameConfigs.packageID);
								TimuzMoreGames.mee.IsExitShowing = true;
								GirlGameConfigs.mee.setCounterExit ();
							} else {
								if (Application.loadedLevelName.Contains ("Level") == true && Application.loadedLevelName != "Levelcomplete") {
									if (GameObject.Find ("UIcontrols(Clone)") != null) {
										GameObject.Find ("UIcontrols(Clone)").SendMessage ("Quitpagefunc");
									} else {
										Application.Quit ();
									}
									TimuzMoreGames.mee.IsExitShowing = false;
								} else {
									Application.Quit ();
								}
							}
						}
					}
				}
//				else if (Application.loadedLevelName == "LevelSelection") {
//					SettingPanel.Instance.GoToScene ("Menu"); // home button click
//				} else if ((Application.loadedLevelName == "InGame") && !WinningConditions.obj.isGameOver) {
//					QuitBoard.Instance.ShowQuitBoard ();
//				}
			} else {
				if (CloseCurrentPopupEvent != null) {
					CloseCurrentPopupEvent ();
				} 
				else {
//					gameConfigs.mee.forRateCheck ();
					print ("---pluginTest backSPace pressed");
					using (AndroidJavaClass javaClass = new AndroidJavaClass ("com.timuz.moregames.webViewClass")) {
						if (TimuzMoreGames.mee.IsExitShowing) {
							Debug.Log ("Is Showing already.. hiding now");
							javaClass.CallStatic ("hideWebView");
							TimuzMoreGames.mee.IsExitShowing = false;
						} else {
							if (TimuzMoreGames.ExitPageURL != "") {
								Debug.Log ("Is hide already.. showing now");
								javaClass.CallStatic ("showWebView", TimuzMoreGames.ExitPageURL, GirlGameConfigs.packageID);
								TimuzMoreGames.mee.IsExitShowing = true;
								GirlGameConfigs.mee.setCounterExit ();
							} else {
								if (Application.loadedLevelName.Contains ("Level") == true && Application.loadedLevelName != "Levelcomplete") {
									if (GameObject.Find ("UIcontrols(Clone)") != null) {
										GameObject.Find ("UIcontrols(Clone)").SendMessage ("Quitpagefunc");
									} else {
										Application.Quit ();
									}
									TimuzMoreGames.mee.IsExitShowing = false;
								} else {
									Application.Quit ();
								}
							}
						}
					}
				}
			}

		}
	}*/

	public bool SetBackClick {
		get {
			return canClickEscape;
		}
		set {
			canClickEscape = value;
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

		canClickEscape = true;
		CloseCurrentPopupEvent = null;
		CurrentPopup = null;
		CurrentlyOpenedPopups = new List<GameObject> ();
		//		BalaLevelParserXML.CanClick = true;
		//		gameConfigs.ismoreGamesShowing = false;
	}

	public void Add (GameObject currentObj)
	{
		canClickEscape = false;
		CancelInvoke ("makeWorkBack");
		Invoke ("makeWorkBack", 0.5f);
		BackButton.CurrentPopup = currentObj;
		BackButton.CurrentlyOpenedPopups.Add (BackButton.CurrentPopup);
	}

	private void makeWorkBack ()
	{
		canClickEscape = true;
	}

	public void Remove ()
	{
		BackButton.CurrentlyOpenedPopups.Remove (BackButton.CurrentPopup);
		BackButton.CurrentPopup = null;
		if (BackButton.CurrentlyOpenedPopups.Count > 0) {
			BackButton.CurrentPopup = BackButton.CurrentlyOpenedPopups [BackButton.CurrentlyOpenedPopups.Count - 1];
		}
	}
}
