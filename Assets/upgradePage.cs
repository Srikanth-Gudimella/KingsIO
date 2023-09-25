using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradePage : MonoBehaviour {
	public static upgradePage mee;
	public GameObject scroller;
	private int scrollCount;
	public GameObject prevBtn, nextBtn;
	public GameObject backBtn,watchVideoBtn,title,totalCoins;
	public Vector3 backBtnPos,watchVideoBtnPos,totalCoinsPos;
	public GameObject[] upgradeFidgets;
	public Sprite[] mainFidgets;
	public int[] spinnerPrices;
	public Text coinsTxt,videoEarnCoinsTxt;
	public GameObject unlockAllBtn;
	public iTween.EaseType itweenType;
	public float timeValue=0.1f;
	public static int totalSnakes=15;


	void Awake()
	{
		mee = this;

		backBtnPos = backBtn.transform.localPosition;
		watchVideoBtnPos = watchVideoBtn.transform.localPosition;
		totalCoinsPos = totalCoins.transform.localPosition;

//		gameObject.SetActive (false);
		Debug.Log ("---------------- Upgrade page awake");
	}
	// Use this for initialization
	void Start () {
		for (int i = 0; i < upgradeFidgets.Length; i++) {
			upgradeFidgets [i].transform.GetChild(1).gameObject.GetComponent<Image> ().sprite = mainFidgets [i];
		}
		GameManagerSlither.cameFromPlayArea=true;
		open();
	}
	public void displayCoins()
	{
		coinsTxt.text = "" + StoreManager.GetCoins ();
	}

	public void prevClick()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);

		if (scrollCount > 0) {
			scrollCount -= 1;
		}
		if (scrollCount == 0) {
			prevBtn.GetComponent<Button> ().interactable = false;
			prevBtn.GetComponent<Image> ().color = new Color32 (255, 255, 255, 122);
		} 
		if (scrollCount != 4) {
			nextBtn.GetComponent<Button> ().interactable = true;
			nextBtn.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		} 
		iTween.MoveTo (scroller, iTween.Hash ("x", scrollCount * -1200, "time", timeValue,"islocal",true, "easetype", itweenType));


	}
	public void nextClick()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);

		if (scrollCount < 7) {
			scrollCount += 1;
		} 
		if (scrollCount == 7) {
			nextBtn.GetComponent<Button> ().interactable = false;
			nextBtn.GetComponent<Image> ().color = new Color32 (255, 255, 255, 122);
		}
		if (scrollCount != 0) {
			prevBtn.GetComponent<Button> ().interactable = true;
			prevBtn.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		}
		iTween.MoveTo (scroller, iTween.Hash ("x", scrollCount*-1200, "time", timeValue,"islocal",true, "easetype", itweenType));


//		iTween.MoveTo (scroller, iTween.Hash ("x", scrollCount*-1200, "time", 0.5f,"islocal",true, "easetype", iTween.EaseType.linear));
	}

	private bool isUnlockedAllSpinners()
	{
		bool unlockedAll = true;
		for (int i = 0; i < totalSnakes; i++) {
			if (PlayerPrefs.GetString (("s" + i), "false")=="false") {
				unlockedAll = false;
				return unlockedAll;
				break;
			}
		}
		return unlockedAll;
	}
	public void open()
	{
		Debug.Log ("updateFidger open");
		gameObject.SetActive (true);
		#if Adsetup_ON
		if (AdManager.instance) {
			AdManager.instance.RunActions (AdManager.PageType.Upgrade,1);
		}
		#endif
		float videoCoins = PlayerPrefs.GetFloat (StoreManager.Video_Coins, 1000);
		//		videoEarnCoinsTxt.text = "" + videoCoins/1000+".0k";

		if(videoCoins==1000)
		{
			videoEarnCoinsTxt.text = "" + videoCoins/1000+".0 k";
		}
		else
		{
			videoEarnCoinsTxt.text = "" + videoCoins/1000+" k";

		}
		coinsTxt.text = "" + StoreManager.GetCoins ();
		scrollCount = 0;
		BuyUnlockAllCheck ();
//		changeSnakeTexture (scrollCount);
		prevBtn.GetComponent<Button> ().interactable = false;
		prevBtn.GetComponent<Image> ().color = new Color32 (255, 255, 255, 122);
		nextBtn.GetComponent<Button> ().interactable = true;


//		spinnerUnlockCheck.instance.setItemBtns (scrollCount);
		iTween.Stop (scroller);
		scroller.transform.localPosition = Vector3.zero;
		iTween.MoveFrom (scroller,iTween.Hash ("x",1500, "time", timeValue, "delay", 0f, "islocal", true, "easetype", itweenType));


		iTween.Stop (title);
		title.transform.localScale = Vector3.one;
		iTween.ScaleFrom (title,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (backBtn);
		backBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (backBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (totalCoins);
		totalCoins.transform.localScale = Vector3.one;
		iTween.ScaleFrom (totalCoins,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (watchVideoBtn);
		watchVideoBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (watchVideoBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.4f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (prevBtn);
		prevBtn.transform.localScale = new Vector3(-1,1,1);
		iTween.ScaleFrom (prevBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (nextBtn);
		nextBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (nextBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		if (!isUnlockedAllSpinners ()) {
			iTween.Stop (unlockAllBtn);
			unlockAllBtn.transform.localScale = Vector3.one;
			iTween.ScaleFrom (unlockAllBtn, iTween.Hash ("x", 0, "y", 0, "time", 0.4f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.spring));
			Invoke ("animateUnlockAllBtn",1f);
		} else {
			unlockAllBtn.SetActive (false);
		}

		if (BackButton.Instance!=null && BackButton.CurrentPopup != gameObject) {
			BackButton.Instance.Add (gameObject);
			BackButton.CloseCurrentPopupEvent += close;
		}
	}
	void animateUnlockAllBtn()
	{
//		iTween.PunchScale(unlockAllBtn)
		iTween.ScaleFrom (unlockAllBtn, iTween.Hash ("x", 0.9f,"y",0.9f, "time", 0.5f,"delay",0f, "islocal",true,"easetype", iTween.EaseType.linear,"looptype",iTween.LoopType.pingPong));

	}

	public void close()
	{
		if (BackButton.Instance != null && BackButton.CurrentPopup != gameObject)
		{
			return;
		}
		AudioClipManager.Instance.Play (MenuSounds.Button);

		Debug.Log ("upgrade close");
		gameObject.SetActive (false);

//		menuPageHandler.mee.open ();
		Application.LoadLevel("Menu");

		if (BackButton.Instance != null) {
			BackButton.Instance.Remove ();
			BackButton.CloseCurrentPopupEvent -= close;
		}
	}
	public void watchVideoBtnClick()
	{
		Debug.Log ("watchVideoBtnClick");
		AudioClipManager.Instance.Play (MenuSounds.Button);
		#if Adsetup_ON
		AdManager.instance.ShowRewardVideoWithCallback((result)=>{
			if (result)
			{
				Debug.Log("watched Video reward here..." + 1000);
				float videoCoins = PlayerPrefs.GetFloat (StoreManager.Video_Coins, 1000);

				StoreManager.AddCoins((int)videoCoins);
				displayCoins();
				coinsTxt.text = "" + StoreManager.GetCoins ();
				videoCoins+=100;
				Debug.Log("videoCOins="+videoCoins);
				videoCoins=Mathf.Clamp(videoCoins,1000,1500);
				PlayerPrefs.SetFloat (StoreManager.Video_Coins, videoCoins);
				videoCoins = PlayerPrefs.GetFloat (StoreManager.Video_Coins, 1000);
				if(videoCoins==1000)
				{
					videoEarnCoinsTxt.text = "" + videoCoins/1000+".0 k";
				}
				else
				{
					videoEarnCoinsTxt.text = "" + videoCoins/1000+" k";

				}
				AdManager.instance.ShowToast ("Coins added");

			}
			else
			{
				Debug.Log("Not Watched Video No Reward....." + 1000);
			}
		}
		);
		#endif
    }
    public void openStorePage()
	{
		Debug.Log ("openStorePage");
		AudioClipManager.Instance.Play (MenuSounds.Button);

		gameObject.SetActive (false);
		storepage.mee.open ();
	}
	public void unlockAllClick()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);
    }
    
	public delegate void BuyCheck ();
	public static event BuyCheck BuyUnlockAllCheck;
	public void buyAllSpinners()
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);
		if (BuyUnlockAllCheck != null) {
			BuyUnlockAllCheck ();
		} 
	}
}
