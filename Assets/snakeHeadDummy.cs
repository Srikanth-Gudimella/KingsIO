using UnityEngine;
using System.Collections;
using CnControls;
public class snakeHeadDummy : MonoBehaviour {
	// Controls the snake movement
	public snakeDummy snakeParameters;
	public bool IsPlayer;
	public SpriteRenderer spriteRenderer;
	public AIDummy aimodule;
	public SpriteRenderer glow;
	public GameObject snakeHeadObj;

	void Start() {
		//		Debug.Log ("snakeHeadStart");
		SetColorBasedOnTemplate();
	}

	public void SetColorBasedOnTemplate() {		
//		snakeHeadObj.GetComponent<MeshRenderer>().material=GameManagerSlither.instance.snakeMaterials[snakeParameters.MaterialIndex];
	}


	void OnTriggerEnter(Collider obj){

		if (snakeParameters.dieing) return;


//		if (obj.transform.tag == "Snake" && obj.transform.root != transform.root) {
//
//			SnakeHead snakeHead = obj.transform.GetComponent<SnakeHead>();
//			if (snakeHead != null) {
//				// Collided with an other head
//
//				if (snakeHead.snakeParameters.points < snakeParameters.points ) { // Little is stronger
//					Die();
//				}
//			}
//			else
//			{
//				Die();
//			}
//
//		}
	

//		if(obj.transform.tag=="EndPath")
//		{
////			Debug.Log ("triggered with endpath name="+(gameObject.name));
//			snakeParameters.isEnableMovement = false;
//			aimodule.direction = Vector3.zero;
//		}
		if(obj.transform.tag=="Obstacle" && !snakeParameters.isCollideWithObstacle)
		{
			Debug.Log ("triggered with obstacle="+obj.gameObject.name);
			directionObj = snakeParameters.snakePieces[1].gameObject;
			snakeParameters.isEnableMovement = false;
			snakeParameters.isCollideWithObstacle = true;
			//			Debug.Log ("pos="+transform.position);
			dir = (transform.position - obj.transform.position);
		}
		else if (obj.transform.tag == "Obstacle2") {
//			Debug.Log ("triggered with obstacle 2222222222 ="+obj.gameObject.name);
			aimodule.direction=new Vector3(aimodule.direction.x*-1 ,0 ,aimodule.direction.z*1 );
			dir = aimodule.direction;
		}
		if(obj.transform.tag=="generatePath")
		{
//			upgradePage.mee.generatePath ();

		}
	}
	Vector3 dir;
	private GameObject directionObj;
	void OnTriggerExit(Collider obj){
//		if(obj.transform.tag=="EndPath")
//		{
//			snakeParameters.isEnableMovement = true;
//		}
		if(obj.transform.tag=="Obstacle")
		{
//			Debug.Log ("triggerExit with obstacle");

			directionObj = null;
			snakeParameters.isEnableMovement = true;
			snakeParameters.isCollideWithObstacle = false;

		}
	}



	bool isInSideArea;
	void Update()
	{
		if (snakeParameters.dieing) return;

		snakeParameters.ControlGlow(glow);

		Move();
		transform.localScale = new Vector3(snakeParameters.referenceScale, snakeParameters.referenceScale, snakeParameters.referenceScale);


		//        if (Vector3.Distance(transform.position, Vector3.zero) <= Population.instance.spawnCircleLenght)
		//        {
		/*if (snakeParameters.isEnableMovement)
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
		else {
			if (snakeParameters.isCollideWithObstacle) {
				RotateTowardsTail();
			} else {
				RotateTowardsCenter ();
			}
		}*/
		if (Application.loadedLevelName == "UpgradePage") {
			isInSideArea = true;
		}
		else
		{
			if (transform.position.x > -160 && transform.position.x < 300 &&
			   transform.position.z > -200 && transform.position.z < 200) {
				isInSideArea = true;
			} else {
				isInSideArea = false;
			}
		}
		if (isInSideArea && !snakeParameters.isCollideWithObstacle)
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
//				Debug.Log ("rotate towards tail");
				RotateTowardsTail();
			}
			else
			{
				RotateTowardsCenter();
			}
		}
	}

	void RotateTowardsCenter() {
		Quaternion targetLook = Quaternion.LookRotation((Vector3.zero+new Vector3(160,0,0))-transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetLook, snakeParameters.rotatingSpeed * Time.deltaTime);
	}
	void RotateTowardsTail() {
		//		Vector3 newPos = (transform.position * dir) * -10;
		Quaternion targetLook = Quaternion.LookRotation(new Vector3(dir.x,0,dir.z));
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

	void Rotate()
	{    
		Vector3 axis = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
		//		Vector3 dir = axis - Vector3.zero;
		//		float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
		//		float distance =Vector3.Distance(axis,Vector3.zero);
		////		Debug.Log ("distance="+distance);
		//
		////		Debug.Log ("angle="+angle);
		//		if (angle < 0)
		//			angle = 360 - angle;

		//		Vector3 newPoint=

		if (axis != Vector3.zero)
		{
			Quaternion targetLook = Quaternion.LookRotation(axis);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetLook, snakeParameters.rotatingSpeed * Time.deltaTime);


			//			Vector3 newPos=Vector3.zero;
			//			newPos.x = transform.position.x + Mathf.Cos(angle)* 500 * Time.deltaTime;
			//			newPos.z = transform.position.z +  Mathf.Sin(angle)* 500 * Time.deltaTime;
			//			transform.position = Vector3.Lerp (transform.position, newPos, 5 * Time.deltaTime);
		}
		// limit rotation 
	}

	void Move()
	{
		//		transform.position.y = 0;
		//		Debug.Log("speedmultiplier="+snakeParameters.speedMultiplier);
		transform.position += transform.forward * snakeParameters.speed*Time.deltaTime*snakeParameters.speedMultiplier;
	}


}
