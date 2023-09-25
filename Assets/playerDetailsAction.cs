using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetailsAction : MonoBehaviour {
	public Transform playerHeadTransform;
	public GameObject playerName,healthBackStripBar;
	public Transform playerDetailsDummy;
	public Transform playerNameDummyTrans,healthBackDummyTrans;
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = playerHeadTransform.position;
		playerDetailsDummy.position = playerHeadTransform.position;
		playerDetailsDummy.localScale = playerHeadTransform.localScale*0.9f;

		playerName.transform.position = playerNameDummyTrans.position;
		healthBackStripBar.transform.position = healthBackDummyTrans.position;
		
	}
}
