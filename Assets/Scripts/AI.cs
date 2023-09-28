using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{

    public Vector3 direction;

    public bool sprint;
    public bool isAttackingPlayer;
	public SnakeHead SnakeHead;
	public GameObject AttackingObj;
	float bulletScaleValue;

    void Start()
    {
		if (!SnakeHead.IsPlayer) {
			StartCoroutine (changeDirectionRandomlyForRoaming ());
			Invoke ("EnableAttacking", 1);
		}
    }
	void EnableAttacking()
	{
//		StartCoroutine(sprintRandomly());//SRIKANTH
		if (isAttackingPlayer) {
			tempWt = waitForSecDirection2;
			AIAttctCounter = attackingPercentage;
//			attackingPercentage = Random.Range (2, 4);
		}
	}

    WaitForSeconds waitForSec = new WaitForSeconds(3);
    IEnumerator sprintRandomly()
    {
        //		Debug.Log ("--------------- sprint Randomly");
        while (true)
        {
            int random = Random.Range(0, 3 + 1);
            if (random == 0)
            {
                sprint = true;
            }
            else
            {
                sprint = false;
            }
            yield return waitForSec;

        }
    }
    //	void OnDrawGizmos() {
    //				Gizmos.color = new Color(1, 0, 0, 0.5F);
    //				//		Gizmos.DrawCube(transform.position, new Vector3(foodRegionMultiplier*enemyRegion, foodRegionMultiplier*enemyRegion, foodRegionMultiplier*enemyRegion));
    //				Gizmos.DrawCube(transform.position, new Vector3(100,50,100));
    //		
    //				//		Gizmos.color = new Color(1, 0, 0, 0.1F);
    //				//		Gizmos.DrawCube(transform.position, new Vector3(5*enemyRegion/4, 5*enemyRegion/4, 5*enemyRegion/4));
    //			}
    public LayerMask onlyPlayerMask;

    WaitForSeconds waitForSecDirection1 = new WaitForSeconds(10);
    WaitForSeconds waitForSecDirection2 = new WaitForSeconds(1f);
    private Collider[] Collider = new Collider[5];
    float tempDist;
	int nearColliderID = 0;
	WaitForSeconds tempWt;// = waitForSecDirection1;
	int AIAttctCounter;
	public int attackingPercentage=3;
    IEnumerator changeDirectionRandomlyForRoaming()
    {
        yield break;
//		Debug.Log ("change Direction RandomlyForRoaming");
        tempWt = waitForSecDirection1;
        //float waitTime = 10;//5
//		if (isAttackingPlayer)
//        {
//            //waitTime = 0.5f;
//            tempWt = waitForSecDirection2;
//        }
        Vector3 startingPoint = transform.position;

        while (true)
        {
			if (isAttackingPlayer)
            {
                //				Debug.Log (gameObject.transform.parent.parent.gameObject.name+"------>Pos="+(gameObject.transform.position));
				if (AIAttctCounter == 0) {
					Physics.OverlapSphereNonAlloc (gameObject.transform.position, 250, Collider, onlyPlayerMask);
//				}
//                				Debug.Log (gameObject.transform.parent.parent.gameObject.name+"------>ColliderLength="+Collider.Length);
//				if (Collider.Length != 0) {
					//					Vector3 circle = Random.insideUnitSphere * 500;
					//					circle.y = startingPoint.y;

					//					Vector3 circle = new Vector3 ( Random.Range(-Population.instance.RangeX , Population.instance.RangeX ) ,0 , Random.Range(Population.instance.RangeZ , -Population.instance.RangeZ)  )  ;
					//					direction = circle - transform.position;
					for (int i = 0; i < 5; i++) {
						if (Collider [i] != null && AttackingObj.GetInstanceID() != Collider [i].gameObject.GetInstanceID()) {
							tempDist = Collider [i].gameObject.transform.position.DistanceBtmSuperFast (transform.position);
							break;
						}
					}

						nearColliderID = 0;
						for (int i = 0; i < 5; i++) {
							if (Collider [i] != null) {
								if (Collider [i].gameObject.transform.position.DistanceBtmSuperFast (transform.position) < tempDist) {
									tempDist = Collider [i].gameObject.transform.position.DistanceBtmSuperFast (transform.position);
									nearColliderID = i;
								}
							}
						}
					
				}

				if (Collider [nearColliderID] != null) {
					//						Debug.Log ("set direction");
					direction = Collider [nearColliderID].gameObject.transform.position - transform.position;
				
					AIAttctCounter++;

					if (AIAttctCounter%attackingPercentage==0) {						
						SnakeHead.mouthAnim.Play ();
						yield return new WaitForSeconds (0.25f);
						GameObject bullet = PoolingSystem.Instance.InstantiateAPS ("playerBullet"+(SnakeHead.bulletType), SnakeHead.mouthObj.transform.position, SnakeHead.mouthObj.transform.rotation);
						bullet.SetLayer (gameObject.layer, false);
						bullet.GetComponent<BulletAction> ().setBulletScaleValue (SnakeHead.snakeParameters.referenceScale);
//						bulletScaleValue=SnakeHead.snakeParameters.referenceScale -0.2f;
//						bullet.transform.localScale = Vector3.one * (bulletScaleValue);
					}
//                    for(int v = 0; v<5; v++)
//                    {
					if (AIAttctCounter == 10) {
						for (int v = 0; v < 5; v++)
						{
							Collider [v] = null;
						}
						AIAttctCounter = 0;
						SnakeHead.mouthAnim.Play ();
						yield return new WaitForSeconds (0.25f);
						GameObject bullet = PoolingSystem.Instance.InstantiateAPS ("playerBullet"+(SnakeHead.bulletType), SnakeHead.mouthObj.transform.position, SnakeHead.mouthObj.transform.rotation);
						bullet.SetLayer (gameObject.layer, false);
						bullet.GetComponent<BulletAction> ().setBulletScaleValue (SnakeHead.snakeParameters.referenceScale);

//						bulletScaleValue=SnakeHead.snakeParameters.referenceScale-0.2f;
//						bullet.transform.localScale = Vector3.one * (bulletScaleValue);
					}
				}
//                    }
//                }
//                	else {
//                					//attack
//                //					Debug.LogError("Attack");
//                
//					if (Population.instance != null) {
//						//					Debug.Log ("---- change RandomDirection");
//						Vector3 circle = new Vector3 (Random.Range (-Population.instance.RangeX, Population.instance.RangeX), 0, Random.Range (Population.instance.RangeZ, -Population.instance.RangeZ));
//						direction = circle - transform.position;
////						tempWt = waitForSecDirection1;
//					}
//                }

            }
            else
            {
                //				Vector3 circle = Random.insideUnitSphere * 250;
                //				circle.y = startingPoint.y;
				if (Population.instance != null) {
//					Debug.Log ("---- change RandomDirection");
					Vector3 circle = new Vector3 (Random.Range (-Population.instance.RangeX, Population.instance.RangeX), 0, Random.Range (Population.instance.RangeZ, -Population.instance.RangeZ));
					direction = circle - transform.position;
				}
            }
            yield return tempWt;
        }


    }


    void OnTriggerStay(Collider obj)
    {

        if (obj.CompareTag("Snake") && obj.transform.root != transform.root)
        {
            Escape(obj);
        }


//        if (obj.CompareTag("Obstacle"))
//        {
//            			Debug.Log ("-------------- obstacle collider stay");
//            changeDirection(obj);
//        }


    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Food"))
        {
            Chase(obj);
        }
        if (obj.CompareTag("snakeHead"))
        {
            //			Debug.Log ("-------------- chase main Player"+obj.GetComponent<Snake>().);
            sprint = true;
            ChasePlayer(obj);
        }
		if (obj.CompareTag("EndPath") && !SnakeHead.snakeParameters.isPlayer)
		{
			SnakeHead.EndPathHitEnter ();
		}
//        if (obj.CompareTag("Obstacle"))
//        {
//            						Debug.Log ("-------------- obstacle collider enter");
//			SnakeHead.ObstacleHitEnter (obj);
////            changeDirection(obj);
//        }

    }
	void OnTriggerExit(Collider obj)
	{
//		if (obj.CompareTag("Obstacle"))
//		{
//			Debug.Log ("-------------- obstacle collider exit");
////			changeDirection(obj);
//			SnakeHead.ObstacleHitExit ();
//		}
		if (obj.CompareTag("EndPath") && !SnakeHead.snakeParameters.isPlayer)
		{
			//            snakeParameters.isEnableMovement = true;
			//			snakeParameters.isCollideWithObstacle = false;
			SnakeHead.EndPathHitExit ();
		}
	}


    void Escape(Collider obj)
    {
        //		Debug.Log ("-----------Escape called");
        direction = transform.position - obj.transform.position;
        direction.y = transform.position.y;
        int random = Random.Range(0, 6 + 1);
        if (random == 2)
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
        direction = obj.transform.position - transform.position;
        direction.y = transform.position.y;

    }
    void ChasePlayer(Collider obj)
    {
        direction = obj.transform.position + transform.position;
        //		direction.y = transform.position.y;

    }
    public void changeDirection(Collider obj)
    {
        //		Debug.Log ("--------- change direction");
        //		direction = transform.position - obj.transform.position;
        //		direction.y = transform.position.y;
    }


}





