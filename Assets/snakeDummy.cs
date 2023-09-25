using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CnControls;

public class snakeDummy : MonoBehaviour {
	public float speed = 5;
	public float rotatingSpeed = 0.7f;
	public int points;
	public int speedMultiplier = 2;
	int originalSpeedMultiplier;
	int startingPoints = 250; // You can't go lower than this value
	int pieceForPoints = 25; // Each 50p gets 1 piece

	int pointsForScale = 250; // Each 250p the scale increases 
	float scaleOffset = 0.05f; // by this value

	public GameObject snakePartPrefab;
	public GameObject snakePartPlayerPrefab;

	public snakeHeadDummy SnakeHead;
	public List<Transform> snakePieces;
	//    public ColorTemplate colorTemplate;
	public int MaterialIndex;

	public AI aiModule;
	public static snakeDummy player;
	public bool isPlayer;
	public bool dieing;
	public float referenceScale;

	public int scalablePiecesCount = 5;
	public float scaleAdjustOffset;

	public bool isEnableMovement;
	public bool isCollideWithObstacle;
	public float sprintScaleValue=0.2f;

	public string playerName;
	public TextMesh playerDetails;
	public GameObject snakeHeadMesh;

	void Awake() {
		//        GetARandomTemplate();
		//		Debug.Log("snakeAwake");
		AddNewPart ();
	}






	void Start() {
		//		Debug.Log ("snakeStart");
		isEnableMovement=true;
		isCollideWithObstacle = false;
		originalSpeedMultiplier = speedMultiplier;
//		snakePieces = new List<Transform>();
//		snakePieces.Add(SnakeHead.transform);
		isPlayer = SnakeHead.IsPlayer;
		if (isPlayer) { player = this;
		}
//		StartCoroutine(consumeSnakeIfSprint());

		//		GetARandomTemplate();
//		GameManagerSlither.instance.playersListLB.Add (gameObject);
	}

	public void GetARandomTemplate()
	{
		//        ColorTemplate[] colorTemplates = FindObjectsOfType<ColorTemplate>();
		if (!isPlayer) {
//			MaterialIndex = Random.Range (0, GameManagerSlither.instance.snakeMaterials.Length);
			MaterialIndex = 0;

		}
		else
		{
			MaterialIndex = 0;
			//			colorTemplate = GameManager.instance.colorTemplates[0];
		}

	}

	void Update() {
//		ControlSnakeLenght();
//		ControlSnakeScale();
//		ControlSprint();

	}

	IEnumerator consumeSnakeIfSprint() {
		yield break;
		while (true){
			if (speedMultiplier != 1) {
				//                if (isPlayer)
				//                {
				//                    points -= 5; // Consume only the main player
				//                }
				Vector3 spawnPosition = snakePieces[snakePieces.Count - 1].transform.position - snakePieces[snakePieces.Count - 1].forward*2;
				FoodManager.instance.SpawnFood(Random.Range(3,6), FoodManager.instance.foodColorRandomList[Random.Range(0, FoodManager.instance.foodColorRandomList.Length)], spawnPosition, false);
			}
			yield return new WaitForSeconds(0.25f);
		}
	}

//	void ControlSprint()
//	{
//		//        if (points <= 300) { speedMultiplier = 1; return; }
//		// IF IS PLAYER
//		if (isPlayer)
//		{
//			if (CnInputManager.GetButton("Jump"))
//			{
//				speedMultiplier = originalSpeedMultiplier;
//			}
//			else
//			{
//				//				sprintScaleValue
//				speedMultiplier = 1;
//			}
//		}
//		// IF IS BOT
//		if (!isPlayer)
//		{
//			if (aiModule.sprint)
//			{
//				speedMultiplier = originalSpeedMultiplier;
//
//			}
//			else
//			{
//				speedMultiplier = 1;
//			}
//		}
//
//
//	}

	public  void ControlGlow(SpriteRenderer glowEffect) {
		if (speedMultiplier != 1)
		{
			Color glowEffectColor = glowEffect.color;
			glowEffectColor.a = Mathf.MoveTowards(glowEffectColor.a, 1, 2 * Time.deltaTime);
			glowEffect.color = glowEffectColor;

		}

		else
		{
			Color glowEffectColor = glowEffect.color;
			glowEffectColor.a = Mathf.MoveTowards(glowEffectColor.a, 0, 2 * Time.deltaTime);
			glowEffect.color = glowEffectColor;
		}


	}

	void ControlSnakeScale() {
		float scale =(float) points / pointsForScale;

		scale = 1 + scale * scaleOffset;

		referenceScale = Mathf.Lerp(referenceScale, scale,Time.deltaTime*1);
	}


	void ControlSnakeLenght() {
		if (points < startingPoints) points = startingPoints;

		int snakeParts = Mathf.RoundToInt(points / pieceForPoints);
//		if (snakePieces.Count < snakeParts) {
//			AddNewPart();
//		}
//		if (snakePieces.Count > snakeParts)
//		{
//			RemovePart();
//		}

	}
//	void RemovePart() {
//		Destroy(snakePieces[snakePieces.Count - 1].gameObject);
//		snakePieces.RemoveAt(snakePieces.Count - 1);
//	}
//	void AddNewPart() {
//		GameObject newPiece = null;
//		if (isPlayer) {
//			newPiece = (GameObject)Instantiate (snakePartPlayerPrefab, snakePieces [snakePieces.Count - 1].transform.position, snakePartPlayerPrefab.transform.rotation);
//
//			scalablePiecesCount = (snakePieces.Count / 5) +3;
//			//			scaleAdjustOffset = 0.25f/scalablePiecesCount;
//			scaleAdjustOffset = 0.1f/scalablePiecesCount;
//
//
//		} else {
//			newPiece = (GameObject)Instantiate (snakePartPrefab, snakePieces [snakePieces.Count - 1].transform.position, snakePartPrefab.transform.rotation);
//			scalablePiecesCount = (snakePieces.Count / 5) +3;
//			//			scaleAdjustOffset = 0.25f/scalablePiecesCount;
//			scaleAdjustOffset = 0.1f/scalablePiecesCount;
//
//		}
//
//		snakePieces.Add(newPiece.transform);
//		newPiece.GetComponent<Piece>().InitializePiece(snakePieces.Count - 1, this);
//	}
	void AddNewPart() {
//		GameObject newPiece = null;
//		if (isPlayer) {
//			newPiece = (GameObject)Instantiate (snakePartPlayerPrefab, snakePieces [snakePieces.Count - 1].transform.position, snakePartPlayerPrefab.transform.rotation);
//
//			scalablePiecesCount = (snakePieces.Count / 5) +3;
//			//			scaleAdjustOffset = 0.25f/scalablePiecesCount;
//			scaleAdjustOffset = 0.1f/scalablePiecesCount;
//
//
//		} else {
//			newPiece = (GameObject)Instantiate (snakePartPrefab, snakePieces [snakePieces.Count - 1].transform.position, snakePartPrefab.transform.rotation);
//			scalablePiecesCount = (snakePieces.Count / 5) +3;
//			//			scaleAdjustOffset = 0.25f/scalablePiecesCount;
//			scaleAdjustOffset = 0.1f/scalablePiecesCount;
//
//		}
//
//		snakePieces.Add(newPiece.transform);

		for (int i = 1; i < snakePieces.Count; i++) {
//			scalablePiecesCount = (i / 5) +3;

			snakePieces[i].GetComponent<PieceDummy> ().InitializePiece (i, this);
		}
	}






}
