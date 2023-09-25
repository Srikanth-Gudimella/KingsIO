using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour
{
    public int PieceIndex;
    public Snake snakeParameters;
    Transform reference;
    float pieceDistanceOffset = 1.2f;
    float pieceYDistanceOffset = 0.01f;
    public SpriteRenderer glow;
    public GameObject snakeBody;
    [HideInInspector]
    public SnakeVisibleCodeMover mySnakeVisible;
    public void InitializePiece(int index, Snake parameters)
    {
//		Debug.Log ("initializePiece index="+index);
        PieceIndex = index;
        snakeParameters = parameters;
        transform.parent = parameters.transform;
        reference = snakeParameters.snakePieces[index - 1];
        referenceTransform = reference.transform;

		transform.position = reference.transform.position - reference.transform.forward * (pieceDistanceOffset * snakeParameters.transform.localScale.x) - Vector3.up * pieceYDistanceOffset;
//		transform.position = snakeParameters.SnakeHead.transform.position;//+new Vector3(0,0,10) - reference.transform.forward * (3 * pieceDistanceOffset * snakeParameters.transform.localScale.x) - Vector3.up * pieceYDistanceOffset;

        SetColorBasedOnTemplate();

        //		Invoke ("addConnectedBody",0.01f);
    }
    //	void addConnectedBody()
    //	{
    ////		if (PieceIndex > 1) {
    //			HingeJoint joint= gameObject.AddComponent<HingeJoint> ();
    //			joint.connectedBody = snakeParameters.snakePieces [PieceIndex-1].GetComponent<Rigidbody> ();
    //
    //			//			gameObject.GetComponent<HingeJoint> ().autoConfigureConnectedAnchor = true;
    ////		}
    //	}

    private void Awake()
    {
        mySnakeVisible = snakeBody.GetComponent<SnakeVisibleCodeMover>();
        if(mySnakeVisible == null)
        {
            mySnakeVisible = snakeBody.AddComponent<SnakeVisibleCodeMover>();
        }
    }

    void Start()
    {
        //transform.localScale = new Vector3(0, 0, 0);

        //        SetColorBasedOnTemplate();
        //		InvokeRepeating ("UpdateTest",1,0.01f);
    }


    void SetColorBasedOnTemplate()
    {
        //        int colorIndex = PieceIndex;
        //        while (colorIndex >= snakeParameters.colorTemplate.colors.Length) {
        //            colorIndex -= snakeParameters.colorTemplate.colors.Length;
        //        }
        //		if (snakeParameters.isPlayer) {
        ////			spriteRenderer.color = snakeParameters.colorTemplate.colors [colorIndex];
        //		} else {
        ////			spriteRenderer.color = snakeParameters.colorTemplate.colors [colorIndex];
        //		}
        //		snakeBody.GetComponent<MeshRenderer>().material=GameManagerSlither.instance.snakeMaterials[snakeParameters.MaterialIndex];
        if (snakeParameters.isPlayer)
        {
            if (PieceIndex % 2 == 0)
            {
                snakeBody.GetComponent<MeshRenderer>().material = GameManagerSlither.instance.snakeMaterials[GameManagerSlither.selectedSnakeIndex];

            }
            else
            {
                snakeBody.GetComponent<MeshRenderer>().material = GameManagerSlither.instance.snakeMaterials2[GameManagerSlither.selectedSnakeIndex];

            }
        }
        else
        {
            if (PieceIndex % 2 == 0)
            {
                snakeBody.GetComponent<MeshRenderer>().material = GameManagerSlither.instance.snakeMaterials[snakeParameters.MaterialIndex];

            }
            else
            {
                snakeBody.GetComponent<MeshRenderer>().material = GameManagerSlither.instance.snakeMaterials2[snakeParameters.MaterialIndex];

            }
        }

    }
    float increaseScaleValue = 1.1f;
    /*void LateUpdate() {
        //		if (snakeParameters.isPlayer) {
        //Move ();
        transform.position = Vector3.Lerp(transform.position, referenceTransform.position, Time.deltaTime * 10);

        //if (isVisible) {
			//Rotate ();
			//snakeParameters.ControlGlow(glow);

		//}
//		}
//		if (snakeParameters.isPlayer)
//		Debug.Log("snake speed multiplier="+snakeParameters.speedMultiplier);
//		Debug.Log ("pieceIndex="+PieceIndex);

//        transform.localScale = new Vector3(snakeParameters.referenceScale, snakeParameters.referenceScale, snakeParameters.referenceScale);
//			if (PieceIndex <= snakeParameters.scalablePiecesCount) {
////			transform.localScale = new Vector3(snakeParameters.referenceScale+(snakeParameters.scalablePiecesCount*snakeParameters.scaleAdjustOffset)-(PieceIndex*snakeParameters.scaleAdjustOffset), snakeParameters.referenceScale, snakeParameters.referenceScale);
//				transform.localScale = new Vector3 (snakeParameters.referenceScale - ((snakeParameters.scalablePiecesCount - PieceIndex) * snakeParameters.scaleAdjustOffset), snakeParameters.referenceScale, snakeParameters.referenceScale + snakeParameters.sprintScaleValue / 2);
//
//			} else if (PieceIndex >= (snakeParameters.snakePieces.Count - snakeParameters.scalablePiecesCount)) {			
//				transform.localScale = new Vector3 (snakeParameters.referenceScale - ((snakeParameters.scalablePiecesCount) - (snakeParameters.snakePieces.Count - PieceIndex)) * (1f / (snakeParameters.scalablePiecesCount)), snakeParameters.referenceScale, snakeParameters.referenceScale + snakeParameters.sprintScaleValue / 2);
//			} else {
//				transform.localScale = new Vector3 (snakeParameters.referenceScale * increaseScaleValue, snakeParameters.referenceScale, snakeParameters.referenceScale * increaseScaleValue + snakeParameters.sprintScaleValue / 2);
//			}



    }*/
    public void scalePiece()
    {
//		Debug.Log ("scalePiece pieceInd="+PieceIndex+":::scalePiecesCount="+(snakeParameters.scalablePiecesCount)+"::snakePiecesCunt="+(snakeParameters.snakePieces.Count));
        if (PieceIndex <= snakeParameters.scalablePiecesCount)
        {
            //			transform.localScale = new Vector3(snakeParameters.referenceScale+(snakeParameters.scalablePiecesCount*snakeParameters.scaleAdjustOffset)-(PieceIndex*snakeParameters.scaleAdjustOffset), snakeParameters.referenceScale, snakeParameters.referenceScale);
//			if (PieceIndex == 1) {
//				transform.localScale = new Vector3 (snakeParameters.referenceScale - ((snakeParameters.scalablePiecesCount-2) * snakeParameters.scaleAdjustOffset),
//					snakeParameters.referenceScale,
//					snakeParameters.referenceScale + snakeParameters.sprintScaleValue * 0.5f);
//			} else {
				
				transform.localScale = new Vector3 (snakeParameters.referenceScale - ((snakeParameters.scalablePiecesCount - PieceIndex) * snakeParameters.scaleAdjustOffset),
					snakeParameters.referenceScale,
					snakeParameters.referenceScale + snakeParameters.sprintScaleValue * 0.5f);
//			}

        }
        else if (PieceIndex >= (snakeParameters.snakePieces.Count - snakeParameters.scalablePiecesCount))
        {
            transform.localScale = new Vector3(snakeParameters.referenceScale - ((snakeParameters.scalablePiecesCount) - (snakeParameters.snakePieces.Count - PieceIndex)) * (1f / (snakeParameters.scalablePiecesCount)),
												snakeParameters.referenceScale,
												snakeParameters.referenceScale + snakeParameters.sprintScaleValue * 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(snakeParameters.referenceScale * increaseScaleValue,
												snakeParameters.referenceScale,
												snakeParameters.referenceScale * increaseScaleValue + snakeParameters.sprintScaleValue * 0.5f);
        }
    }


    //transform.rotation = 
    void Rotate()
    {
        Vector3 direction = referenceTransform.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetLook = Quaternion.LookRotation(direction);
            //					transform.rotation = Quaternion.Lerp (transform.rotation, targetLook, snakeParameters.rotatingSpeed * Time.deltaTime * 30 );				
            transform.rotation = targetLook;
        }

    }
    //        float MOVlerpTime = 0.50f;
    //	Vector3 referencePosition=Vector3.zero;
    //	float frameRateCorrection;
    Transform referenceTransform;

    //	float refPositionX,refPositionz,forwardPosX,forwardPosZ;
    //	Vector3 transPositionValue,refPositionValue;
    /*void Move()
    {
//		transPositionValue = transform.position;
//		refPositionValue = referenceTransform.position;
		transform.position = Vector3.Lerp (transform.position, referenceTransform.position, Time.deltaTime * 10);       
    }*/

}
