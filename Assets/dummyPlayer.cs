using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyPlayer : MonoBehaviour {
	public bool isMoveToPlayer=false;
	public GameObject targetPlayer=null;
	// Use this for initialization
	void Start () {
		
	}
	void FixedUpdate()
	{
		if(isMoveToPlayer && targetPlayer!=null)
		{
			//			Debug.Log ("----- food picked up fixed update");
			transform.position = Vector3.Lerp (transform.position, targetPlayer.transform.position+new Vector3(0,0,0.1f), Time.deltaTime*15);//*8
			//			scaleValue=scaleValue*0.01f;
			//			transform.localScale = scaleValue;
		}
	}
	public void deActive()
	{
		gameObject.SetActive (false);
	}

}
