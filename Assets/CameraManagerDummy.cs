using UnityEngine;
using System.Collections;

public class CameraManagerDummy : MonoBehaviour {
	public snakeDummy playerSnake;
	public GameObject snakeHead;
	Camera cam;
	float startOrtographic;
	float initialPosY;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		//        startOrtographic = cam.orthographicSize;
		startOrtographic = cam.fieldOfView;
		initialPosY = transform.position.y;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (playerSnake != null)
		{
			Follow();
			Zoom();
		}
	}

	void Zoom() {
		float scale = playerSnake.referenceScale;
		//		cam.fieldOfView = Mathf.Lerp(cam.orthographicSize,startOrtographic + scale*20,1);


	}

	void Follow() {
		Vector3 playerPosition = playerSnake.SnakeHead.transform.position+new Vector3(0,0,-50);
		//        playerPosition.y = transform.position.y;
		float scale = playerSnake.referenceScale;
		playerPosition.y = initialPosY + (scale-1) * 50;
		transform.position = Vector3.Lerp(transform.position, playerPosition,  10*Time.deltaTime);
//		playerSnake.rotatingSpeed = 6 - (snakeHead.transform.localScale.x*1.1f);
	}
}
