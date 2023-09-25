using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PieceDummy : MonoBehaviour {
	public int PieceIndex;
	public snakeDummy snakeParameters;
	Transform reference;
	public SpriteRenderer spriteRenderer;
	float pieceDistanceOffset = 1.3f;
	float pieceYDistanceOffset = 0.01f;
	public SpriteRenderer glow;
	public GameObject snakeBody;
	public void InitializePiece(int index, snakeDummy parameters) {
		PieceIndex = index;
		snakeParameters = parameters;
		transform.parent = parameters.transform;
		reference = snakeParameters.snakePieces[index - 1];
//		transform.position = reference.transform.position - reference.transform.forward * (pieceDistanceOffset * snakeParameters.transform.localScale.x) - Vector3.up* pieceYDistanceOffset;

	}

	void Start()
	{
//		transform.localScale = new Vector3(0, 0, 0);

		SetColorBasedOnTemplate();
		if (Application.loadedLevelName != "UpgradePage") {
			pieceDistanceOffset = 0.5f;
		} 
	}


	void SetColorBasedOnTemplate()
	{

//		snakeBody.GetComponent<MeshRenderer>().material=GameManagerSlither.instance.snakeMaterials[snakeParameters.MaterialIndex];

	}
	float increaseScaleValue=1.1f;
	void Update() {
		Move();
		Rotate();
//		snakeParameters.ControlGlow(glow);
		//		Debug.Log ("pieceIndex="+PieceIndex);

		//        transform.localScale = new Vector3(snakeParameters.referenceScale, snakeParameters.referenceScale, snakeParameters.referenceScale);
//		if (PieceIndex <= snakeParameters.scalablePiecesCount) {
//			//			transform.localScale = new Vector3(snakeParameters.referenceScale+(snakeParameters.scalablePiecesCount*snakeParameters.scaleAdjustOffset)-(PieceIndex*snakeParameters.scaleAdjustOffset), snakeParameters.referenceScale, snakeParameters.referenceScale);
//			transform.localScale = new Vector3 (snakeParameters.referenceScale - ((snakeParameters.scalablePiecesCount - PieceIndex) * snakeParameters.scaleAdjustOffset), snakeParameters.referenceScale, snakeParameters.referenceScale + snakeParameters.sprintScaleValue / 2);
//
//		} else if (PieceIndex >= (snakeParameters.snakePieces.Count - snakeParameters.scalablePiecesCount)) {			
//			transform.localScale = new Vector3 (snakeParameters.referenceScale - ((snakeParameters.scalablePiecesCount) - (snakeParameters.snakePieces.Count - PieceIndex)) * (1f / (snakeParameters.scalablePiecesCount)), snakeParameters.referenceScale, snakeParameters.referenceScale + snakeParameters.sprintScaleValue / 2);
//		} else {
//			transform.localScale = new Vector3 (snakeParameters.referenceScale * increaseScaleValue, snakeParameters.referenceScale, snakeParameters.referenceScale * increaseScaleValue + snakeParameters.sprintScaleValue / 2);
//		}
	}



	void Rotate()
	{
		Vector3 referencePosition = reference.transform.position-reference.transform.forward* (1 * snakeParameters.transform.localScale.x);
		referencePosition.y = transform.position.y;

		Vector3 direction = referencePosition - transform.position;



		if (direction != Vector3.zero)
		{
			Quaternion targetLook = Quaternion.LookRotation(direction);

			transform.rotation = Quaternion.Lerp(transform.rotation, targetLook, snakeParameters.rotatingSpeed*Time.deltaTime*30*snakeParameters.speedMultiplier);
		}

	}
	float MOVlerpTime = 0.50f;
	void Move()
	{

		float frameRateCorrection = Time.deltaTime*10;//smooth delta time
		MOVlerpTime = Mathf.Lerp(0.3f, 0.95f, frameRateCorrection);
//		Debug.Log ("reference="+reference+"::snakeparameters="+snakeParameters);
		Vector3 referencePosition = reference.transform.position - reference.transform.forward* ((pieceDistanceOffset  * snakeParameters.transform.localScale.x));
		referencePosition.y = transform.position.y;
		transform.position = Vector3.Lerp(transform.position, referencePosition, MOVlerpTime/2);

	}


}
