using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeHeadRotateAnim : MonoBehaviour {
	private bool rotate1,rotate2;
	float x;
	// Use this for initialization
	Vector3 snakeHeadAngle;
	void Start () {
		x = transform.localEulerAngles.x;
//		Debug.Log ("angle x=" + x);
//		snakeRotate1 ();
	}
//	void snakeRotate1()
//	{
//		snakeHeadAngle = new Vector3 (x, 160, 0);
//		Invoke ("snakeRotate2",1);
//	}
//	void snakeRotate2()
//	{
//		snakeHeadAngle = new Vector3 (x,200, 0);
//		Invoke ("snakeRotate1",1);
//
//	}
	// Update is called once per frame
//	void Update () {
//		Quaternion targetLook = Quaternion.EulerAngles(snakeHeadAngle);
//
//		transform.localRotation = Quaternion.Slerp(transform.localRotation, targetLook, 2 * Time.deltaTime);
//	}
}
