using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loopRotate : MonoBehaviour {

	public float speed;
	public int directionY = 0;
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(speed * new Vector3(0,directionY,-1) * Time.deltaTime);

	}
}
