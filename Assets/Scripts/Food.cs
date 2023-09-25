using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{

    float foodValue;
    bool beingEat;
    float timerToBeActive = 2;
    float timerToDestroy = 10;
    bool destruction;
    public SpriteRenderer spriteRenderer;
	public SpriteRenderer GlowspriteRenderer;

	public bool isDestroyable;
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
        foodValue = value;
//		if (!isDestroyable) {
			transform.localScale = new Vector3 (10 + value, 10 + value, 10 + value);
//		} else {
//			transform.localScale = new Vector3 (12 + value, 12 + value, 12 + value);
//		}


    }
    //    IEnumerator fadeStartEffect() {
    //        Color col = spriteRenderer.color;
    //        Color origin = col;
    //        origin.a = 0;
    //        float lerper = 0;
    //        float lerperTime = 1f;
    //        while (lerper <= 1) {
    //            lerper += Time.deltaTime / lerperTime;
    //            spriteRenderer.color = Color.Lerp(origin, col, lerper);
    //            yield return new WaitForEndOfFrame();
    //        }
    //
    //
    //    }

    //    void Update() {
    //
    //		if (isDestroyable) {
    ////			if (timerToBeActive >= 0) {
    ////				timerToBeActive -= Time.deltaTime;
    ////			}
    //			if (timerToDestroy >= 0) {
    //				timerToDestroy -= Time.deltaTime;
    //				if (timerToDestroy <= 0) {
    //					destruction = true;
    //				}
    //
    //
    //			}
    //
    //			if (destruction) {            
    //				if (Vector3.Distance (Camera.main.transform.position, this.transform.position) > 150) {
    ////					Debug.LogError ("---------- force destroying");
    //					FoodManager.instance.foodCount--;
    //					Destroy (this.gameObject);
    //
    //				}            
    //            
    //			}
    //		}
    //
    //    }
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
//				obj.transform.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100,obj.transform.position,10);
//				Debug.Log("---- apply force");
//				obj.transform.gameObject.GetComponent<SnakeHead> ().faceObj.GetComponent<Animation> ().Play ();
//				obj.transform.gameObject.GetComponent<Rigidbody>().AddForce(obj.transform.forward * -500,ForceMode.Force);
//				obj.transform.gameObject.GetComponent<Rigidbody>().AddForce(obj.transform.forward * -0);

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

        if (!isDestroyable)
        {
            FoodManager.instance.reSpawnFood(this.gameObject);
            GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            //			FoodManager.instance.foodCount--;
            Destroy(this.gameObject);
        }
        //        Destroy(this.gameObject);
    }


}
