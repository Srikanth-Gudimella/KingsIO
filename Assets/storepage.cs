using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class storepage : MonoBehaviour {

	public static storepage mee;
	public Text[] PriceTexts;
	public GameObject[] storeItems;
	public iTween.EaseType itweenType;
	public float timeValue=0.1f;
	public GameObject title,backBtn;

	void Awake()
	{
		mee = this;
		gameObject.SetActive (false);
	}
	public void open()
	{
		AudioClipManager.Instance.Play (2);
		gameObject.SetActive (true);

		iTween.Stop (title);
		title.transform.localScale = Vector3.one;
		iTween.ScaleFrom (title,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.3f, "islocal", true, "easetype", iTween.EaseType.spring));

		iTween.Stop (backBtn);
		backBtn.transform.localScale = Vector3.one;
		iTween.ScaleFrom (backBtn,iTween.Hash ("x",0,"y", 0, "time", 0.4f, "delay", 0.5f, "islocal", true, "easetype", iTween.EaseType.spring));

		for (int i = 0; i < storeItems.Length; i++) {
			iTween.Stop (storeItems [i]);
			storeItems [i].transform.localPosition = new Vector3 ((-435 + i * 290), 10, 0);
			iTween.MoveFrom (storeItems [i],iTween.Hash ("x",1500, "time", 0.5f, "delay", i*timeValue, "islocal", true, "easetype", itweenType));

		}
        //btm2018
//		#if Adsetup_ON
//
//		if (GameConfigs2018.mee) {
//			for (int i = 0; i < PriceTexts.Length; i++) {
//				PriceTexts [i].text = PlayerPrefs.GetString (GameConfigs2018.mee.inappIds_All [i], "Buy");
//			}
//		}
//		#endif
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
		upgradePage.mee.open ();
		if (BackButton.Instance != null) {
			BackButton.Instance.Remove ();
			BackButton.CloseCurrentPopupEvent -= close;
		}
	}
	public void buyClick(int index)
	{
		AudioClipManager.Instance.Play (MenuSounds.Button);
    }
}
