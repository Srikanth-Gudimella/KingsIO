using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeColliderPosSet : MonoBehaviour {
	public GameObject snakeHead;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x,transform.position.y,snakeHead.transform.position.z);
		
	}
}
