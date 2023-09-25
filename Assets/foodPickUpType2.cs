using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodPickUpType2 : MonoBehaviour {
	Vector3 targetPos;
	private float minX=-27.5f,maxX=27.5f,minY=-28,maxY=28f;
	// Use this for initialization
	void Start () {
		
	}
	
	public void setPosition(GameObject Obj)
	{
		CancelInvoke ("enableCollision");
		gameObject.transform.localScale = Vector3.one * (Random.Range (0.5f, 0.9f));
		gameObject.GetComponent<CircleCollider2D> ().enabled = false;
		Vector2 randPos = Random.insideUnitSphere * 5;
		transform.position = Obj.transform.position;
		Vector3 newPos=transform.position + new Vector3 (randPos.x, randPos.y, 0.1f);
		if (newPos.x > maxX) {
			newPos.x = maxX;
		}
		if (newPos.x < minX) {
			newPos.x = minX;
		}
		if (newPos.y > maxY) {
			newPos.y = maxY;
		}
		if (newPos.y < minY) {
			newPos.y = minY;
		}
		targetPos=newPos;

//		transform.position = targetPos;
		Invoke("enableCollistion",0.2f);
	}
//	void OnDisable()
//	{
////		Debug.Log ("foodType2 ondisable");
//		CancelInvoke ("enableCollision");
//	}
	private void enableCollistion()
	{
		gameObject.GetComponent<CircleCollider2D> ().enabled = true;

	}
	public void FixedUpdate()
	{
		transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime*2);//*8

	}
}
