using UnityEngine;
using System.Collections;

public class FoodDestroyable : MonoBehaviour
{

	float foodValue;
	bool beingEat;
	float timerToBeActive = 2;
	float timerToDestroy = 10;
	bool destruction;
	public SpriteRenderer spriteRenderer;

	public bool isDestroyable;
	public Vector3 targetPos;
	public void SetColor(Color col) {
		//        spriteRenderer.color = col;
		//		GlowspriteRenderer.color = col;

		//        StartCoroutine(fadeStartEffect());//srikanth
	}
	public void setFoodType(Sprite foodType)
	{

		spriteRenderer.sprite = foodType;

	}
	public void SetSize(float value)
	{
//		Debug.Log ("Food Destroyable setsize");
		foodValue = value;
		//		if (!isDestroyable) {
		transform.localScale = new Vector3 (6 + value, 8 + value, 6 + value);
		//		} else {
		//			transform.localScale = new Vector3 (12 + value, 12 + value, 12 + value);
		//		}

		StartCoroutine(moveToTargetPos(Random.Range(0,0.2f)));
	}
	IEnumerator moveToTargetPos(float waitTime)
	{
//		Debug.Log ("moveToTargetPos");
		yield return new WaitForSeconds (waitTime);
//		gameObject.GetComponent<SphereCollider> ().enabled = true;

		float lerper = 0;
		float lerperTime = 0.5f;
		Vector3 currentPosition = transform.position;
		while (lerper <= 1)
		{
			lerper += Time.deltaTime / lerperTime;
			try
			{
				transform.position = Vector3.Lerp(currentPosition, targetPos, lerper);
			}
			catch
			{
				lerper = 1;
			}
			yield return waitForEnd;
		}
//		Debug.Log ("MoveToTargetPos End");
		gameObject.GetComponent<SphereCollider> ().enabled = true;


	}

	void OnTriggerEnter(Collider obj)
	{
		//        if (timerToBeActive >= 0) return;
		if (beingEat) return;

		if (obj.CompareTag("Snake") && !GameManagerSlither.instance.isPlayerBlasted)
		{
			Snake snakeParam = obj.transform.root.GetComponent<Snake>();
			//            if ((snakeParam.thisSnakeIsPhotonAI && Snake.all_AIs_Mine) || snakeParam.isPlayer)
			//            {
			snakeParam.addPointsOnFoodForPlayer(Mathf.RoundToInt(foodValue * 2));//btmphoton for ai also


			if (snakeParam.isPlayer) {
				AudioClipManager.Instance.Play (InGameSounds.EatFood);
				GameManagerSlither.instance.myScore = snakeParam.points;
			}

			if (snakeParam.isPlayer && GameManagerSlither.challengeModeType ==2 && !GameManagerSlither.instance.isTargetReached) {
				Debug.Log ("points="+(snakeParam.points-snakeParam.startingPoints)+":::"+snakeParam.points);
				GameManagerSlither.instance.checkTargetSize (snakeParam.points-snakeParam.startingPoints);
			}
			//            }
			snakeParam.ShowScore();
			GetComponent<SphereCollider>().enabled = false;
			//			StartCoroutine(moveAndDisappear(obj.transform));
			//			Debug.Log("snake="+(obj.GetComponent<Snake>()));
			StartCoroutine(moveAndDisappear(obj.GetComponent<SnakeHead>().mouthObj.transform));

		}
	}
	WaitForEndOfFrame waitForEnd = new WaitForEndOfFrame();
	IEnumerator moveAndDisappear(Transform targetTransform)
	{
//		Debug.Log ("Food destroyable move and disappear");
//		yield break;
		beingEat = true;

		float lerper = 0;
		float lerperTime = 0.5f;
		Vector3 currentPosition = transform.position;
		while (lerper <= 1)
		{
			lerper += Time.deltaTime / lerperTime;
			try
			{
				transform.position = Vector3.Lerp(currentPosition, targetTransform.position+new Vector3(0,2,0) - Vector3.up, lerper);
			}
			catch
			{
				lerper = 1;
			}
			yield return waitForEnd;
		}
		// FoodManager.instance.SpawnFood(Random.Range(1, 2.5f), FoodManager.instance.foodColorRandomList[Random.Range(0, FoodManager.instance.foodColorRandomList.Length)], Vector3.zero);
		beingEat = false;

//		if (!isDestroyable)
//		{
//			FoodManager.instance.reSpawnFood(this.gameObject);
//			GetComponent<SphereCollider>().enabled = true;
//		}
//		else
//		{
			//			FoodManager.instance.foodCount--;
//			Destroy(this.gameObject);
			PoolingSystem.DestroyAPS (this.gameObject);
//		}
	}


}
