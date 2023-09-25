using UnityEngine;
using System.Collections;

public class AIDummy : MonoBehaviour {

	public Vector3 direction;

	public bool sprint;

	void Start() {
//		StartCoroutine(changeDirectionRandomlyForRoaming());
//		StartCoroutine(sprintRandomly());

	}

	IEnumerator sprintRandomly() {
		float waitTime = 3;
		while (true)  {
			int random = Random.Range(0, 3 + 1);
			if (random == 0) {
				sprint = true;
			} else
			{
				sprint = false;
			}
			yield return new WaitForSeconds(waitTime);

		}
	}

	IEnumerator changeDirectionRandomlyForRoaming() {
		float waitTime = 0.5f;
		Vector3 startingPoint = transform.position;

		while (true) {
			Vector3 circle = Random.insideUnitSphere * 50;
			circle.y = startingPoint.y;
			direction = circle - transform.position;
			yield return new WaitForSeconds(waitTime);
		}


	}


	void OnTriggerStay(Collider obj) {

		if (obj.tag == "Snake" && obj.transform.root != transform.root)
		{
			Escape(obj);
		}


		if (obj.tag == "Obstacle")
		{
			//			Debug.Log ("-------------- stay change Direction main Player");
			changeDirection(obj);
		}


	}

	void OnTriggerEnter(Collider obj)
	{
		if (obj.tag == "Food")
		{
			Chase(obj);
		}
		if (obj.tag == "snakeHead")
		{
			//			Debug.Log ("-------------- chase main Player"+obj.GetComponent<Snake>().);
			sprint = true;
			ChasePlayer(obj);
		}
		if (obj.tag == "Obstacle")
		{
			//						Debug.Log ("-------------- change Direction main Player");
			changeDirection(obj);
		}

	}


	void Escape(Collider obj) {

		direction = transform.position - obj.transform.position;
		direction.y = transform.position.y;
		int random = Random.Range(0, 6 + 1);
		if (random == 0)
		{
			sprint = true;
		}
		else
		{
			sprint = false;
		}
	}



	void Chase(Collider obj)
	{
		direction =  obj.transform.position-transform.position;
		direction.y = transform.position.y;

	}
	void ChasePlayer(Collider obj)
	{
		direction =  obj.transform.position+transform.position;
		//		direction.y = transform.position.y;

	}
	public void changeDirection(Collider obj)
	{
		//		Debug.Log ("--------- change direction");
		//		direction = transform.position - obj.transform.position;
		//		direction.y = transform.position.y;
	}


}





