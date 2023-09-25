using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlePositionSetup : MonoBehaviour {
	public GameObject snakeHead;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localPosition = new Vector3 (160, 0, (-snakeHead.transform.localPosition.z)+60);
		
	}
}
