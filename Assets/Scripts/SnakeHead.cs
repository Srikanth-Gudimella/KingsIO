using UnityEngine;
using System.Collections;
using CnControls;
public class SnakeHead : MonoBehaviour
{
    // Controls the snake movement
    public Snake snakeParameters;
    public bool IsPlayer;
    public SpriteRenderer spriteRenderer;
    public AI aimodule;
    public SpriteRenderer glow;
    public GameObject snakeHeadObj;
	public GameObject faceObj,mouthObj;
	public Animation mouthAnim;
	public GameObject rightEyeBal;
	public GameObject leftEyeBal;
	public playerDetailsAction playerDetailsObj;
	private Vector3 center;
	public Animation BulletHitAnim,BulletHitPlayerAnim;
	public string bulletType;

    void Start()
    {
        //		Debug.Log ("snakeHeadStart");
		playerDetailsObj.playerHeadTransform=this.gameObject.transform;
		snakeParameters.isPlayer=this.IsPlayer;
        SetColorBasedOnTemplate();
		Invoke ("startMovement",0.35f);
		center = transform.position;
		BulletHitAnim = faceObj.GetComponent<Animation> ();
    }
	void startMovement()
	{
		isStartMovement = true;
	}
    public void SetColorBasedOnTemplate()
    {
        //		if (!IsPlayer) {
        ////			spriteRenderer.color = snakeParameters.colorTemplate.colors [0];
        //			snakeHeadObj.GetComponent<MeshRenderer>().material=GameManager.instance.snakeMaterials[snakeParameters.MaterialIndex];
        //		} else {
        //			snakeHeadObj.GetComponent<MeshRenderer>().material=GameManagerSlither.instance.snakeMaterials[snakeParameters.MaterialIndex];
        //		}

//        if (snakeParameters.isPlayer)
//        {
//            gameObject.name = "playerHead";
//            snakeHeadObj.GetComponent<MeshRenderer>().material = GameManagerSlither.instance.snakeMaterials2[GameManagerSlither.selectedSnakeIndex];
//        }
//        else
//        {
//            snakeHeadObj.GetComponent<MeshRenderer>().material = GameManagerSlither.instance.snakeMaterials2[snakeParameters.MaterialIndex];
//        }
        //		if (aimodule.isAttackingPlayer) {
        //			snakeHeadObj.GetComponent<MeshRenderer>().material = GameManagerSlither.instance.snakeBotMaterial;
        //
        //		}


//		if (snakeParameters.isPlayer)
//		{
//			gameObject.name = "playerHead";
//			faceObj.GetComponent<SpriteRenderer>().sprite = GameManagerSlither.instance.faceImgs[GameManagerSlither.selectedSnakeIndex];
//			mouthObj.GetComponent<SpriteRenderer>().sprite = GameManagerSlither.instance.mouthImgs[GameManagerSlither.selectedSnakeIndex];
//		}
//		else
//		{
//			faceObj.GetComponent<SpriteRenderer>().sprite = GameManagerSlither.instance.faceImgs[snakeParameters.FaceIndex];
//			mouthObj.GetComponent<SpriteRenderer>().sprite = GameManagerSlither.instance.mouthImgs[snakeParameters.FaceIndex];
//		}

    }


    void OnTriggerEnter(Collider obj)
    {
        /*if (!snakeParameters.isPlayer)
            return;
        if (obj.transform.root.name == "Food_Manager")
            return;

        Debug.Log("Hit with something="+obj.transform.root.name);*/
        if (snakeParameters.dieing) return;
        //		if(snakeParameters.isPlayer)
        //		Debug.Log("Hit with something="+obj.gameObject.name);

//		if (obj.CompareTag("Snake"))
//        {
//			Debug.LogError ("----- players triggered with each other challgentype="+GameManagerSlither.challengeModeType);
////			return;
//			if (GameManagerSlither.challengeModeType != 0 && GameManagerSlither.instance.isTargetReached) {
//				return;
//			}
//            //			if (snakeParameters.isPlayer) {
//            //				Debug.Log ("Hit with something=" + obj.gameObject.name);
//            //			}
//            //return;  && obj.transform.root != transform.root
//            SnakeHead snakeHead = obj.transform.GetComponent<SnakeHead>();
////			faceObj.GetComponent<Animation> ().Play ();
//
//		//	srikanth
//            if (snakeHead != null)
//            {
//                // Collided with an other head
//
//                if (snakeHead.snakeParameters.getPoints() > snakeParameters.getPoints())
//                { // Little is stronger
//                  //Bigger is stroner now
//                    Die(obj);
//
//                }
//				snakeParameters.reduceStrength (10,gameObject);
//				snakeHead.snakeParameters.reduceStrength (10, obj.gameObject);
//            }
//            
//
//        }

//		if (obj.CompareTag("EndPath") && !snakeParameters.isPlayer)
//        {
////            snakeParameters.isEnableMovement = false;
////			snakeParameters.isCollideWithObstacle = false;
////			aimodule.direction = Vector3.zero;
//
//			EndPathHitEnter ();
//        }
		if (obj.CompareTag("Obstacle"))
        {

			snakeParameters.isEnableMovement = false;
			snakeParameters.isCollideWithObstacle = true;
			dir = (transform.position - obj.transform.position);
        }
		else if (obj.CompareTag("EndPath"))
		{

			snakeParameters.isEnableMovement = false;
			dir = (transform.position - obj.transform.position);
		}
	}
    Vector3 dir;
    private GameObject directionObj;
    void OnTriggerExit(Collider obj)
    {
//		if (obj.CompareTag("EndPath") && !snakeParameters.isPlayer)
//        {
////            snakeParameters.isEnableMovement = true;
////			snakeParameters.isCollideWithObstacle = false;
//			EndPathHitExit ();
//        }
		if (obj.CompareTag("Obstacle"))
        {
			directionObj = null;
			snakeParameters.isEnableMovement = true;
			snakeParameters.isCollideWithObstacle = false;
        }
		else if (obj.CompareTag("EndPath"))
		{
			directionObj = null;
			snakeParameters.isEnableMovement = true;
		}

	}
	public void EndPathHitEnter()
	{
		snakeParameters.isEnableMovement = false;
		snakeParameters.isCollideWithObstacle = false;

		aimodule.direction = Vector3.zero;
	}
	public void EndPathHitExit()
	{
		snakeParameters.isEnableMovement = true;
		snakeParameters.isCollideWithObstacle = false;
	}
	void Die(Collider obj)
    {
        snakeParameters.DeathRoutine();
//		faceObj.GetComponent<Animation> ().Play ();
//		if (!snakeParameters.isPlayer && GameManagerSlither.challengeModeType==3 && obj.gameObject.layer == LayerMask.NameToLayer ("snake1")) {
		if (!snakeParameters.isPlayer && obj.gameObject.layer == LayerMask.NameToLayer ("snake1")) {
			GameManagerSlither.instance.enemiesKilledCount++;
			if (GameManagerSlither.challengeModeType == 3) {
				GameManagerSlither.instance.checkTargetKills ();
			}
		}
    }
	bool isInSideArea;
	public float RangeX, RangeZ;
	bool isStartMovement=false;
	Vector3 playerPosition;
    void LateUpdate()
    {
		if (!IsPlayer)
			return;
		if (!isStartMovement) {
			return;
		}
        if (snakeParameters.dieing) return;

//		if ((IsPlayer || (snakeParameters.thisSnakeIsPhotonAI &&  Snake.all_AIs_Mine == true)) && GameManagerSlither.instance.isStartGamePlay)
//        {
		if (GameManagerSlither.instance.isStartGamePlay)
		{
//            snakeParameters.ControlGlow(glow);

            //		Vector3 step = transform.forward * 1;
            //
            //		this.gameObject.transform.position += step;
            //		if(snakeParameters.isPlayer)
            //		{
            //			Debug.Log("speedMultipPlayer="+snakeParameters.speedMultiplier);
            //		}
			if (!GameManagerSlither.instance.isPlayerBlasted) {
				transform.position += transform.forward * snakeParameters.speed * Time.deltaTime * 1 * snakeParameters.speedMultiplier;
				playerPosition = transform.position;
			}

			if (snakeParameters.isPlayer) {
				if (playerPosition.x > RangeX) {
					playerPosition.x = RangeX;
					transform.position = playerPosition;
				}
				if (playerPosition.x < -RangeX) {
					playerPosition.x = -RangeX;
					transform.position = playerPosition;
				}
				if (playerPosition.z > RangeZ) {
					playerPosition.z = RangeZ;
					transform.position = playerPosition;
				}
				if (playerPosition.z < -RangeZ) {
					playerPosition.z = -RangeZ;
					transform.position = playerPosition;
				}
			}


//			dummyHead.transform.position = gameObject.transform.position;

//            transform.localScale = new Vector3(snakeParameters.referenceScale, snakeParameters.referenceScale, snakeParameters.referenceScale);


            //        if (Vector3.Distance(transform.position, Vector3.zero) <= Population.instance.spawnCircleLenght)
            //        {
//			Debug.Log("isEnableMovement="+(snakeParameters.isEnableMovement)+":::isCollidewith="+(snakeParameters.isCollideWithObstacle));
            if (snakeParameters.isEnableMovement)
            {

                if (IsPlayer)
                {
                    Rotate();
                }
                else
                {
                    IaRotate();

                }
            }
            else
            {
                if (snakeParameters.isCollideWithObstacle)
                {
                    RotateTowardsTail();
                }
                else
                {
                    RotateTowardsCenter();
                }
            }
			/*if (transform.position.x > -RangeX && transform.position.x < RangeX &&
			    transform.position.z > -RangeZ && transform.position.z < RangeZ) {
				isInSideArea = true;
			} else {
				isInSideArea = false;
			}
			if (isInSideArea)
			{

				if (IsPlayer)
				{
					Rotate();
				}
				else
				{
					IaRotate();

				}
			}
			else
			{
				if (snakeParameters.isCollideWithObstacle)
				{
					RotateTowardsTail();
				}
				else
				{
					RotateTowardsCenter();
				}
			}*/
        }
        else
        {
            //This can be AI player that you cannot controll, or this is not Player..!
        }
    }

    void RotateTowardsCenter()
    {
        Quaternion targetLook = Quaternion.LookRotation(Vector3.zero - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetLook, snakeParameters.rotatingSpeed * Time.deltaTime);
    }
    void RotateTowardsTail()
    {
//		Debug.Log ("RotateTowardsTail");
        //		Vector3 newPos = (transform.position * dir) * -10;
        Quaternion targetLook = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetLook, snakeParameters.rotatingSpeed * Time.deltaTime);
    }


    void IaRotate()
    {
        if (aimodule.direction != Vector3.zero)
        {
            Quaternion targetLook = Quaternion.LookRotation(aimodule.direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetLook, snakeParameters.rotatingSpeed * Time.deltaTime);
        }
    }

	int rotateCount=0;
	float tempRotatingSpeed;
	Vector3 axis;
    void Rotate()
    {
		tempRotatingSpeed = snakeParameters.rotatingSpeed;
//        Vector3 axis = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
		axis = new Vector3(Virtualjoystick.mee.Horizontal (), 0, Virtualjoystick.mee.Vertical ());

        //		Vector3 dir = axis - Vector3.zero;
        //		float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        //		float distance =Vector3.Distance(axis,Vector3.zero);
        ////		Debug.Log ("distance="+distance);
        //
        ////		Debug.Log ("angle="+angle);
        //		if (angle < 0)
        //			angle = 360 - angle;

        //		Vector3 newPoint=

		if (axis != Vector3.zero) {
			rotateCount++;
			Quaternion targetLook = Quaternion.LookRotation (axis);

			rightEyeBal.transform.rotation = targetLook;
			leftEyeBal.transform.rotation = targetLook;

			if (rotateCount > 4) {
				transform.rotation = Quaternion.Slerp (transform.rotation, targetLook, tempRotatingSpeed * Time.deltaTime);
			} else {
				transform.rotation = Quaternion.Slerp (transform.rotation, targetLook, tempRotatingSpeed/10 * Time.deltaTime);
			}


//			Vector3 axis2 = new Vector3(0,CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));

//			Vector3 axis2 = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"),0);
//			Quaternion targetLook2 = Quaternion.LookRotation(axis2);


//			Debug.Log ("axis="+axis);
//			Vector3 dir = (axis - rightEyeBal.transform.localPosition) * 1;
//			dir = Vector3.ClampMagnitude((rightEyeBal.transform.localPosition-axis2), 0.2f);
//
//
//			dir.z = 0;
////			Vector3 tt = rightEyeBal.transform.localPosition + dir;
//			rightEyeBal.transform.localPosition = dir;
//


//			dummyHead.transform.position = gameObject.transform.position;

			//			Vector3 newPos=Vector3.zero;
			//			newPos.x = transform.position.x + Mathf.Cos(angle)* 500 * Time.deltaTime;
			//			newPos.z = transform.position.z +  Mathf.Sin(angle)* 500 * Time.deltaTime;
			//			transform.position = Vector3.Lerp (transform.position, newPos, 5 * Time.deltaTime);
		} else {
			rotateCount = 0;
		}
        // limit rotation 
    }


}
