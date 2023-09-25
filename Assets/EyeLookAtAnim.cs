using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class EyeLookAtAnim : MonoBehaviour {
	public Transform targetObjTrans;
	public Transform playerHead;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = targetObjTrans.position;
//		transform.position = playerHead.position-new Vector3(3,0,4);

		transform.localScale = targetObjTrans.lossyScale;
//		Vector3 direction = transform.position - snakeHeadTrans.position;
//
//		transform.LookAt (snakeHeadTrans.forward);
//
//		////                        snakePieces[v].rotation = Quaternion.Lerp(snakePieces[v].rotation, myPointsRot[tempCounter], myHeadSpeedForOneFrame * speedf);
//		//
//		//
//		Vector3 direction2 = gameObject.transform.position - snakeHeadTrans.position;
//		            if (direction2 != Vector3.zero)
//		            {
//						gameObject.transform.rotation = Quaternion.LookRotation(direction2);
//		            }



//		transform.LookAt (snakeHeadTrans.up*-1);




	}
}
