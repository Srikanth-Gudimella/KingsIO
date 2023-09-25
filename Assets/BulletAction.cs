using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour {
	float timeValue;
	public ParticleSystem[] playerEffects;
	// Use this for initialization
	void Start () {
//		timeValue = 0;
//		Destroy (this.gameObject, 3f);		
//		PoolingSystem.DestroyAPS (gameObject);
	}
	void OnEnable()
	{
		timeValue = 0;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position += transform.up * 100 * Time.deltaTime*-1;
		timeValue += Time.deltaTime;
		if (timeValue > 3) {
			PoolingSystem.DestroyAPS (gameObject);
		}
	}
//	void OnCollisionEnter(Collision collision)
//	{
//		if (collision.gameObject.CompareTag ("Snake")) {
//			Snake snakeParam = collision.transform.root.GetComponent<Snake>();
//			snakeParam.reduceStrength (20,collision.gameObject);
//			PoolingSystem.DestroyAPS (gameObject);
//		}
//	}
	void OnTriggerEnter(Collider obj)
	{
		if (obj.CompareTag ("Snake")) {
			Snake snakeParam = obj.transform.root.GetComponent<Snake>();
			snakeParam.reduceStrength (20,gameObject);
			PoolingSystem.DestroyAPS (gameObject);
//			if(gameObject.layer.com==GameManagerSlither.instance.playerSnake.layer)
		}
	}
	float bulletScaleValue;

	public void setBulletScaleValue(float referenceScale)
	{
		bulletScaleValue=referenceScale-0.2f;
		transform.localScale = Vector3.one * (bulletScaleValue);

		for (int i = 0; i < playerEffects.Length; i++) {
			playerEffects[i].startSize=2*referenceScale;
		}
	}
}
