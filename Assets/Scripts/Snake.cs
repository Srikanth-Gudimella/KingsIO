using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine.UI;
public class Snake : MonoBehaviour
{
    public float speed = 5;
    public float rotatingSpeed = 1f;
    public int points = 0;
    public int speedMultiplier = 1;
    int originalSpeedMultiplier = 1; 	
    public int startingPoints = 250; // You can't go lower than this value
    int pieceForPoints = 25; // Each 50p gets 1 piece

    int pointsForScale = 250; // Each 250p the scale increases 
    float scaleOffset = 0.05f; // by this value

    public GameObject snakePartPrefab;
    public GameObject snakePartPlayerPrefab;
	public GameObject snakeFirstPiecePrefab;

    public SnakeHead SnakeHead;
    public List<Transform> snakePieces;
    //    public ColorTemplate colorTemplate;
    public int MaterialIndex;
	public int FaceIndex;

    public AI aiModule;
    public static Snake player;
    public bool isPlayer;
    public bool dieing;
    public float referenceScale;

    public int scalablePiecesCount = 5;
    public float scaleAdjustOffset;

    public bool isEnableMovement;
    public bool isCollideWithObstacle;
    public float sprintScaleValue = 0.2f;

    public string playerName;
    public TextMesh playerDetails;
    public TextMesh playerScoreDetails;
    private int myPreviousPointsWhenInited = 0;
    public bool isAttackingPlayerObj;
	public GameObject playerBullet;
	public float strength;
	public GameObject healthFillBar;
	public GameObject[] HeadObjs;
    public GameObject DeathParticles;
    public void addPointsOnFoodForPlayer(int _points)
    {		
		
        if (points == 0 && _points != 0)
            myPreviousPointsWhenInited = _points;

        points += _points;

      
        if (points < startingPoints) points = startingPoints;


			if (isPlayer)
			{
			playerDetails.text = PlayerPrefs.GetString("myName", "")+"  "+(points-200);
			}
			else
			{
			playerDetails.text = playerName+"  "+(points-200);
			}

			tempTotalPoints = points;

			if (tempTotalPoints > 10000)
				tempTotalPoints = 10000;
			referenceScale = 1f + (tempTotalPoints * 0.001f);
			if (referenceScale > 8) {
				referenceScale = 8;
			}
			SnakeHead.gameObject.transform.localScale = new Vector3(referenceScale, 1, referenceScale);
    }

    //For all the players we get here. even for Player also..
    public void addPointsFromPhoton(int _points)
    {
        //Debug.Log("points from Photon=" + _points);
        points = _points;
        snakePartsBasedOnFood = (int)(points / pieceForPoints);
        if (_points < 0)
            removeMyPartsTillHere();
    }

    public int getPoints()
    {
		return (points-200);
    }

//    public static bool all_AIs_Mine = false;
//    public bool thisSnakeIsPhotonAI = false;
	Transform randomSpawnCircle;
	int posCheckCount = 0;
	void generatePos()
	{
		randomSpawnCircle=GameManagerSlither.instance.myPlayerSpawnPoses[Random.Range(0, GameManagerSlither.instance.myPlayerSpawnPoses.Count)];
	}
	//snakeHead
	//AIModule
    void Awake()
    {
        Debug.LogError("---- Snake Awake");

        //		Debug.Log ("isPlayer="+isPlayer);
        //decide head and assign values
        for (int i = 0; i < HeadObjs.Length; i++) {
			HeadObjs [i].SetActive (false);
		}
        DeathParticles.SetActive(false);
    }
    public int mySnakeRenderIndexInAllSnakes = 0;

    void Start()
    {
        Debug.LogError("------------- Snake Start 11111 isPlayer="+isPlayer);
        //if (isPlayer)
        //{
        //    //			GameManagerSlither.selectedSnakeIndex = 13;
        //    FaceIndex = GameManagerSlither.selectedSnakeIndex;
        //}
        //else
        //{
        //    GetARandomTemplate();
        //}
        HeadObjs[FaceIndex].SetActive(true);
        SnakeHead = HeadObjs[FaceIndex].GetComponent<SnakeHead>();
        setBulletType();

        aiModule = SnakeHead.aimodule;

        snakePieces = new List<Transform>();
        snakePieces.Add(SnakeHead.transform);
        SnakeHead.RangeX = Population.instance.RangeX;
        SnakeHead.RangeZ = Population.instance.RangeZ;
        //Debug.Log("added Head..");
        cacheCountPieces = snakePieces.Count;

        if (isPlayer)
        {
            //			generatePos();
            spintFillerCount = 0;
            //			GameManagerSlither.instance.boosterFillBar.GetComponent<Image> ().fillAmount = spintFillerCount;
            randomSpawnCircle = GameManagerSlither.instance.myPlayerSpawnPoses[Random.Range(0, GameManagerSlither.instance.myPlayerSpawnPoses.Count)];

            transform.position = randomSpawnCircle.position;//srikanth added
        }
        myHeadTrans = SnakeHead.transform;
        //PieceMoverObj.parent = SnakeHead.transform;

        //Head is first point in myVisiblePoints;
        myVisiblePointsMover = new List<bool>();

        SnakeVisibleCodeMover myHeadVisible = SnakeHead.GetComponent<SnakeHead>().snakeHeadObj.GetComponent<SnakeVisibleCodeMover>();
        myHeadVisible.mySnake = this;
        myVisiblePointsMover.Add(false);
        myHeadVisible.myPieceIndex = 0;

        //			for (int i = 0; i < 20; i++) {
        addPointsOnFoodForPlayer(0);
        //			}
        //			if (myPointsPos.Count < snakePartsBasedOnFood)
        //			{
        //				checkNAddPart ();
        //			}
        strength = 20;

        nakamaManager = GameObject.FindGameObjectWithTag("NakamaManager").GetComponent<NakamaManager>();

        if (isPlayer) {
			if (GameManagerSlither.startWithBigSize) {
				GameManagerSlither.startWithBigSize = false;
				addPointsOnFoodForPlayer ((500));
			} 
		}
        pieceDistance = SnakeHead.GetComponent<SnakeHead>().snakeHeadObj.GetComponent<Renderer>().bounds.size.z - (SnakeHead.GetComponent<SnakeHead>().snakeHeadObj.GetComponent<Renderer>().bounds.size.z / 1.5f);
        SnakeHead.GetComponent<SnakeHead>().snakeHeadObj.SetLayer(gameObject.layer, true);
        SnakeHead.GetComponent<SnakeHead>().snakeHeadObj.transform.parent.gameObject.SetLayer(gameObject.layer, true);
        SnakeHead.gameObject.SetLayer(gameObject.layer, true);

        if (isPlayer)
        {
            SnakeHead.transform.GetChild(2).gameObject.layer = 5;// to activate attacking UI layer..?
        }

        //		Debug.Log ("snakeStart");
        isEnableMovement = true;
        isCollideWithObstacle = false;
        originalSpeedMultiplier = speedMultiplier;


        Debug.LogError("------------- Snake Start 22222 isPlayer=" + isPlayer+ "::SnakeHead.IsPlayer="+ SnakeHead.IsPlayer);
        SnakeHead.IsPlayer = isPlayer;
        //isPlayer = SnakeHead.IsPlayer;
        if (isPlayer)
        {
            if (player != null)
            {
                Debug.LogError("Already Player set...?");
            }
            player = this;
            //Debug.Log("----------- selectedSnakeIndex=" + (GameManagerSlither.selectedSnakeIndex));

            mySnakeRenderIndexInAllSnakes = 0;
            //Population.allSnakesVisibleValues.Add(true);//aready default added true
            isSnakeInCame = true;
        }

//        StartCoroutine(consumeSnakeIfSprint());
        //		GetARandomTemplate();
        GameManagerSlither.instance.playersListLB.Add(gameObject);
        
            //if (isPlayer == false)
            //    name = playerName;
            //else
            //    name = "MyPlayer";


		Population.instance.snakesIndex++;

		if ((Population.instance.snakesIndex % (100/Population.instance.attackingPercentage))==0 && !isPlayer)
		{
			aiModule.isAttackingPlayer = true;
			aiModule.attackingPercentage = 2;
		}
		else
		{
			aiModule.isAttackingPlayer = true;
			aiModule.attackingPercentage = 3;
		}
        isAttackingPlayerObj = aiModule.isAttackingPlayer;
        //if(!isPlayer)
        //Invoke(nameof(reduceStregthTest), 2);
    }
    void reduceStregthTest()
    {
        reduceStrength(100);
    }
    public void GetARandomTemplate()
    {
		//Debug.Log ("GetARandomTemplate");
        //        ColorTemplate[] colorTemplates = FindObjectsOfType<ColorTemplate>();
        if (!isPlayer)
        {
            //			colorTemplate = GameManager.instance.colorTemplates [Random.Range (0, GameManager.instance.colorTemplates.Length)];
//            MaterialIndex = Random.Range(0, GameManagerSlither.instance.snakeMaterials.Length);
			//FaceIndex = Random.Range(0, HeadObjs.Length);
            FaceIndex = 0;
        }
        //		else
        //		{
        //			MaterialIndex = 0;
        ////			colorTemplate = GameManager.instance.colorTemplates[0];
        //		}

    }

    public bool immediateDieForTesting = false;
//	int tempLastPos=0;
    //Start ArrayBased Snake
    void LateUpdate()
    {
//		Debug.Log ("snakeParts="+snakePartsBasedOnFood);
        if (dieing)
            return;
        ControlSprint();
        if (immediateDieForTesting)
        {
            immediateDieForTesting = false;
            Debug.Log("ImmediateDieFortesting");
            DeathRoutine();
            return;
        }
		//if (strength < 100) {
		//	strength +=0.04f;
		//	float scaleX = strength * 1 / 100;
		//	healthFillBar.transform.localScale = new Vector3 (scaleX, 1, 4);
		//}
       

    }

    private Vector3 myHeadLastPos;
    private Transform myHeadTrans;
    private float pieceDistance = 0f;
    private int currentIndex = 0;
    public List<Vector3> myPointsPos = new List<Vector3>();
    public List<Quaternion> myPointsRot = new List<Quaternion>();
    //public float moveSpeed = 15f;
    private int frameCounter = 0;

    private float myHeadSpeedForOneFrame = 0;

    //private bool quickSetPosForFirstTimeForMoveSmooth = true;
    private void moveSmoothlyToArray()
    {

		int tempCounter = currentIndex - 1;
		if (tempCounter < 0 || tempCounter == snakePieces.Count)
			tempCounter = myPointsPos.Count - 1;
		myHeadSpeedForOneFrame = (speed) * Time.deltaTime;

		for (int v = 1; v < cacheCountPieces; v++)
		{
			//snakePieces[v].localPosition = myPointsPos[tempCounter];
			snakePieces[v].localPosition = Vector3.MoveTowards(snakePieces[v].localPosition, myPointsPos[tempCounter], myHeadSpeedForOneFrame * speedf * speedMultiplier);
			//                        snakePieces[v].rotation = Quaternion.Lerp(snakePieces[v].rotation, myPointsRot[tempCounter], myHeadSpeedForOneFrame * speedf);


			Vector3 direction = snakePieces[v-1].position - snakePieces[v].position;
			if (direction != Vector3.zero)
			{
				snakePieces[v].rotation = Quaternion.LookRotation(direction);
			}

			tempCounter--;

			if (tempCounter < 0)
				tempCounter = myPointsPos.Count - 1;
		}


    }

    public void resetBeforeSmooth()
    {
        int tempCounter = currentIndex - 1;

        if (tempCounter < 0 || tempCounter == snakePieces.Count)
            tempCounter = myPointsPos.Count - 1;


        //PieceMoverObj.localScale = new Vector3(1, 1, 1);
        //PieceMoverObj.localPosition = new Vector3(0, 0, 0);

        for (int v = 1; v < cacheCountPieces; v++)
        {
            snakePieces[v].localPosition = myPointsPos[tempCounter];
            snakePieces[v].rotation = myPointsRot[tempCounter];
            snakePieces[v].gameObject.SetActive(true);

            tempCounter--;

            if (tempCounter < 0)
                tempCounter = myPointsPos.Count - 1;
        }
    }

    public float speedf = 1;
    private Vector3 lastSetPos;
    private int currentPieceForMove = 1;
    private void moveSpeedlyToArray()
    {
        if (dieing)
            return;
        if (snakePieces.Count > currentPieceForMove && myHeadLastPos != lastSetPos)
        {
            lastSetPos = myHeadLastPos;
            snakePieces[currentPieceForMove].localPosition = lastSetPos;
            currentPieceForMove++;
            if (currentPieceForMove >= snakePieces.Count)
            {
                currentPieceForMove = 1;
            }
        }
    }

    private int cacheCountPieces = 0;



//    IEnumerator consumeSnakeIfSprint()
//    {
//        yield break;
//        while (true)
//        {
//            if (speedMultiplier != 1)
//            {
//                //                if (isPlayer)
//                //                {
//                //                    points -= 5; // Consume only the main player
//                //                }
////				isSpintActive=true;
////                addPointsOnFoodForPlayer(-10);
////				Debug.Log("consume if spint");
//				Vector3 spawnPosition = snakePieces[cacheCountPieces - 1].transform.position - snakePieces[cacheCountPieces - 1].forward * 20;
//				FoodManager.instance.SpawnFood(Random.Range(3, 6), FoodManager.instance.foodColorRandomList[Random.Range(0, FoodManager.instance.foodColorRandomList.Length)], spawnPosition, false,true);
//            }
//            yield return new WaitForSeconds(0.2f);
//        }
//    }

	public float spintFillerCount;
	bool readyToShoot=true,IsActiveShoot=true;
    void ControlSprint()
    {
        //        if (points <= 300) { speedMultiplier = 1; return; }
        // IF IS PLAYER
        if (isPlayer)
        {
			
			if (CnInputManager.GetButton("Jump"))
			{
				if (readyToShoot && IsActiveShoot) {
					readyToShoot = false;
					IsActiveShoot = false;
//					GameObject bullet = PoolingSystem.Instance.InstantiateAPS ("playerBullet", SnakeHead.mouthObj.transform.position, SnakeHead.mouthObj.transform.rotation);
//					bullet.SetLayer(gameObject.layer, false);
					SnakeHead.mouthAnim.Play ();
					StartCoroutine (playerShoot(0.2f,0.5f));
//					AudioClipManager.Instance.Play (InGameSounds.OpponentShoot);
//					GameObject bullet = (GameObject)Instantiate(playerBullet, SnakeHead.mouthObj.transform.position,SnakeHead.mouthObj.transform.rotation);
				}
			}
			else
			{
				readyToShoot = true;
			}
        }

    }
    private NakamaManager nakamaManager;

    void ShootEvent()
    {
        bool IsShoot = true;
        nakamaManager.SendMatchState(
            OpCodes.shoot,
            MatchDataJson.Input(IsShoot)
        ); ;
    }
    float bulletScaleValue;
	IEnumerator playerShoot(float WaitTime,float delayActiveTime)
	{
		yield return new WaitForSeconds (WaitTime);
		GameObject bullet = PoolingSystem.Instance.InstantiateAPS ("playerBullet"+(SnakeHead.bulletType), SnakeHead.mouthObj.transform.position, SnakeHead.mouthObj.transform.rotation);
		bullet.SetLayer(gameObject.layer, false);
		bullet.GetComponent<BulletAction> ().setBulletScaleValue (referenceScale);
		AudioClipManager.Instance.Play (InGameSounds.Fire);
        ShootEvent();
        yield return new WaitForSeconds (delayActiveTime);
		IsActiveShoot = true;
	}
    public IEnumerator shoot()
    {
        SnakeHead.mouthAnim.Play();
        yield return new WaitForSeconds(0.2f);
        GameObject bullet = PoolingSystem.Instance.InstantiateAPS("playerBullet" + (SnakeHead.bulletType), SnakeHead.mouthObj.transform.position, SnakeHead.mouthObj.transform.rotation);
        bullet.SetLayer(gameObject.layer, false);
        bullet.GetComponent<BulletAction>().setBulletScaleValue(referenceScale);
        //AudioClipManager.Instance.Play(InGameSounds.Fire);
    }
    public void ControlGlow(SpriteRenderer glowEffect)
    {
		return;
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

    void ControlSnakeScale()
    {
        float scale = (float)points / pointsForScale;

        scale = 1 + scale * scaleOffset;

        //        referenceScale = Mathf.Lerp(referenceScale, scale,Time.deltaTime*1);
        rotatingSpeed = 5 - (referenceScale * 1.1f);
    }

    public int snakePartsBasedOnFood = 1;
    void RemovePart()
    {
        //Debug.Log("removed extra part..." + name + " cacheCountPieces=" + cacheCountPieces);
        /*Destroy(snakePieces[cacheCountPieces - 1].gameObject);*/

        freePieces.Add(snakePieces[cacheCountPieces - 1].gameObject);

        snakePieces.RemoveAt(cacheCountPieces - 1);
        cacheCountPieces = snakePieces.Count;
        freePieces[freePieces.Count - 1].transform.parent = null;
        freePieces[freePieces.Count - 1].SetActive(false);
        freePieces[freePieces.Count - 1].transform.position = new Vector3(5000, 0, 0);
        freePieces[freePieces.Count - 1].GetComponentInChildren<MeshRenderer>().material = null;
    }

    public List<GameObject> freePieces = new List<GameObject>();

    public void resetMe()
    {
        /*if (!allSnakes.Contains(this))
        {
            allSnakes.Add(this);

        }*/
        points = 0;
//        Debug.LogError("Reset Mee..!");
		currentIndex=0;
        addPointsOnFoodForPlayer(myPreviousPointsWhenInited);
       
        Invoke("delaydieing", 1f);
        //Debug.Log("Reset=snakePieces" + snakePieces.Count + " snakePartsBasedOnFood=" + snakePartsBasedOnFood);
        while (snakePieces.Count > snakePartsBasedOnFood)
        {
            RemovePart();
        }
        snakePieces[0].localPosition = Vector3.zero;
        myVisiblePointsMover.Clear();
        myVisiblePointsMover.Add(false);//first head.

        for (int i = 1; i < snakePieces.Count; i++)
        {
            Piece pieceComponenet = snakePieces[i].gameObject.GetComponent<Piece>();
            pieceComponenet.scalePiece();
            snakePieces[i].position = snakePieces[0].position; //reset to head..!

            pieceComponenet.mySnakeVisible.mySnake = this;
            myVisiblePointsMover.Add(false);
            pieceComponenet.mySnakeVisible.myPieceIndex = myVisiblePointsMover.Count - 1;
        }

        int removingIndexFrom = snakePartsBasedOnFood - 1;

        if (myPointsPos.Count > removingIndexFrom && removingIndexFrom + (myPointsPos.Count - removingIndexFrom) <= myPointsPos.Count)
        {
            myPointsPos.RemoveRange(removingIndexFrom, myPointsPos.Count - removingIndexFrom);
            myPointsRot.RemoveRange(removingIndexFrom, myPointsRot.Count - removingIndexFrom);
        }



        for (int v = 0; v < myPointsPos.Count; v++)
        {
            myPointsPos[v] = myHeadTrans.localPosition;
            myPointsRot[v] = myHeadTrans.rotation;
        }

        //Debug.Log("ResetDone=" + snakePieces.Count + "_" + myPointsPos.Count + "_" + myPointsRot.Count + "_" + snakePartsBasedOnFood);
        if (isPlayer)
            Debug.Break();
        cacheCountPieces = snakePieces.Count;

		strength = 100;
    }

    private void removeMyPartsTillHere()
    {
        while (snakePieces.Count > snakePartsBasedOnFood)
        {
            RemovePart();
        }

        myVisiblePointsMover.Clear();
        myVisiblePointsMover.Add(false);//first head.

        for (int i = 1; i < snakePieces.Count; i++)
        {
            Piece pieceComponenet = snakePieces[i].gameObject.GetComponent<Piece>();
            pieceComponenet.scalePiece();
            //snakePieces[i].position = snakePieces[0].position; //reset to head..!

            //pieceComponenet.mySnakeVisible.mySnake = this;
            myVisiblePointsMover.Add(false);
            pieceComponenet.mySnakeVisible.myPieceIndex = myVisiblePointsMover.Count - 1;
        }

        int removingIndexFrom = snakePartsBasedOnFood - 1;

        if (myPointsPos.Count > removingIndexFrom && removingIndexFrom + (myPointsPos.Count - removingIndexFrom) <= myPointsPos.Count)
        {
            myPointsPos.RemoveRange(removingIndexFrom, myPointsPos.Count - removingIndexFrom);
            myPointsRot.RemoveRange(removingIndexFrom, myPointsRot.Count - removingIndexFrom);
        }

		cacheCountPieces = snakePieces.Count;
    }

    private void delaydieing()
    {
        dieing = false;
//        GameManagerSlither.setDyingSnakesCountTxt(-1);
    }

	int tempTotalPoints;
    void AddNewPart()
    {
        GameObject newPiece = null;
        if (isPlayer)
        {
            if (freePieces.Count > 0)
            {
                newPiece = freePieces[0].gameObject;
                freePieces.RemoveAt(0);
            }
            else
            {
				if (snakePieces.Count == 1) {
					newPiece = (GameObject)Instantiate (snakeFirstPiecePrefab, snakePieces [snakePieces.Count - 1].transform.position, Quaternion.identity);
				} else {
					newPiece = (GameObject)Instantiate (snakePartPlayerPrefab, snakePieces [snakePieces.Count - 1].transform.position, Quaternion.identity);
				}
            }
        }
        else
        {
            if (freePieces.Count > 0)
            {
                newPiece = freePieces[0].gameObject;
                freePieces.RemoveAt(0);
            }
            else
			{
				if (snakePieces.Count == 1) {
					newPiece = (GameObject)Instantiate (snakeFirstPiecePrefab, snakePieces [snakePieces.Count - 1].transform.position, Quaternion.identity);
				} else {

					newPiece = (GameObject)Instantiate (snakePartPrefab, snakePieces [snakePieces.Count - 1].transform.position, Quaternion.identity);
				}
            }

        }

        //Debug.Log("New PieceBefore=" + snakePieces[snakePieces.Count - 1].gameObject.name);
        newPiece.transform.position = snakePieces[snakePieces.Count - 1].transform.position;
        newPiece.SetActive(true);


        newPiece.SetLayer(gameObject.layer, true);
        scalablePiecesCount = (snakePieces.Count / 5) + 3;
        //scaleAdjustOffset = 0.25f/scalablePiecesCount;
		scaleAdjustOffset = 0.35f / scalablePiecesCount;

        snakePieces.Add(newPiece.transform);
        newPiece.name = "piece" + (snakePieces.Count - 1);
        Piece myPiceComponenet = newPiece.GetComponent<Piece>();
        myPiceComponenet.InitializePiece(snakePieces.Count - 1, this);

        cacheCountPieces = snakePieces.Count;

        for (int i = 1; i < cacheCountPieces; i++)
        {
            snakePieces[i].gameObject.GetComponent<Piece>().scalePiece();
        }

        myPiceComponenet.mySnakeVisible.mySnake = this;
        myVisiblePointsMover.Add(false);
        myPiceComponenet.mySnakeVisible.myPieceIndex = myVisiblePointsMover.Count - 1;

		tempTotalPoints = points;

		if (tempTotalPoints > 2500)
			tempTotalPoints = 2500;
		referenceScale = 0.8f + (tempTotalPoints * 0.0005f);
//		SnakeHead.gameObject.transform.localScale = new Vector3(referenceScale, 1, referenceScale);
		pieceDistance = 3.05f + (tempTotalPoints / 800);
//		rotatingSpeed = 3.2f - (tempTotalPoints / 1400f);


    }
    public List<bool> myVisiblePointsMover = new List<bool>();
	[HideInInspector]
	public bool isSnakeInCame = false;

    public void DeathRoutine()
    {
        Debug.Log("---DeathRoutine");
        if (dieing) return;

        dieing = true;
//        GameManagerSlither.setDyingSnakesCountTxt(1);
		//		Debug.Log ("--------- DeathRoutine 1111111");
		//if (SnakeHead.transform.position.DistanceBtmSuperFast (Camera.main.transform.position) <= 250 * 250) {
		//	//			Debug.Log ("--------- DeathRoutine 222222222");
		//	for (int i=0;i<10;i++) {
		//			Vector3 randomCircle = Random.insideUnitSphere * 35;
		//			randomCircle.y = 0;
		//			int value = Random.Range (3, 5);//srikanth added
		//		FoodManager.instance.SpawnFoodDestroyable (value, FoodManager.instance.foodColorRandomList [Random.Range (0, FoodManager.instance.foodColorRandomList.Length)], SnakeHead.gameObject.transform.position + randomCircle,SnakeHead.gameObject.transform.position, false,true);

		//	}
		//}

        //if (!isPlayer)
        //Population.instance.SpawnSnake(Random.Range(250, 1000));
        //StartCoroutine(FadeToDeathRoutine());
        //btm2018 testing
        if (isPlayer)
        {
            GameManagerSlither.instance.OnGameOver_Event();
			BGSoundManager.Instance.StopPlaying ();
//			gameObject.SetActive(false);
			AudioClipManager.Instance.Play (InGameSounds.ResultPage);
            //			if (GameManagerSlither.instance.IsEnableWatchVideoToResume && !GameManagerSlither.instance.IsPauseYesClicked) {
            //				GameManagerSlither.instance.isPlayerBlasted = true;
            //				GameManagerSlither.instance.destroyPlayer ();
            //				ResumePage.instance.Invoke ("Open", 2);
            ////				ResumePage.instance.Open ();
            //				GameManagerSlither.instance.IsEnableWatchVideoToResume = false;
            //			} else {
            SnakeHead.gameObject.SetActive(false);
            DeathParticles.transform.position = SnakeHead.gameObject.transform.position;
            DeathParticles.SetActive(true);
			//GameManagerSlither.instance.destroyPlayer ();//this is death animation or replace it
            //OnLocalPlayerDied();
            Invoke(nameof(OnLocalPlayerDied), 1);
            //Destroy(player, 0.5f);

           // GameManagerSlither.instance.Invoke("openResultPage", 2);

			//}
        }
        else
        {
            //allSnakes.Remove(this);
            //Destroy(this.gameObject);
            Population.instance.freeSnakesToSpawn.Add(this);
            //Debug.Log("added to freeSnakesToSpawn=" + name);
//            if (GameManagerSlither.instance.playersListLB.Contains(this.gameObject))
//                GameManagerSlither.instance.playersListLB.Remove(this.gameObject);

            //Population.instance.SpawnSnake(Random.Range(250, 500));
        }
    }
    private async void OnLocalPlayerDied()
    {
        // Send a network message telling everyone that we died.

        WaitForOtherPlayersPop.Instane.Open();

        Debug.LogError("---- OnLocalPlayerDied Died event call");
        await nakamaManager.SendMatchStateAsync(OpCodes.Died, MatchDataJson.Died(player.transform.position));

        // Remove ourself from the players array and destroy our GameObject after 0.5 seconds.
        GameManagerSlither.instance.players.Remove(GameManagerSlither.instance.localUser.SessionId);
        GameManagerSlither.instance.keysToRemove.Remove(GameManagerSlither.instance.localUser.SessionId);

        Destroy(this.gameObject, 0.5f);
    }

    public void ShowPlayerName()
    {
        //playerDetailsText.Remove(0, playerDetailsText.Length);
        if (isPlayer)
        {
//			Debug.LogError ("---- ShowPlayerName");
            playerDetails.text = PlayerPrefs.GetString("myName", "");
			playerName=PlayerPrefs.GetString("myName", "");
			GameManagerSlither.instance.myPlayerName = playerName;
			GameManagerSlither.instance.myScore = startingPoints;
			GameManagerSlither.instance.myPlayerRank = 20;
//            Debug.Log("myPlayerName=" + gameObject.name);

			playerDetails.text = PlayerPrefs.GetString("myName", "")+"  "+(points-200);
        }
        else
        {
//            playerDetails.text = playerName;
			playerDetails.text = playerName+"  "+(points-200);
//            Debug.Log("OhterPlayerName=" + gameObject.name);
        }
    }

    public void ShowScore()
    {
        //playerScoreDetails.text = points.ToString();
    }

    //Photon..


	public void reduceStrength(int amount)
	{
        //Debug.Log("reducestrength=" + amount + "::currentstrength=" + strength+"::IsPlayer="+isPlayer);
		strength -= amount;
		//strength -= strength;
        if (strength < 0)
			strength = 0;
		float scaleX = strength * 2 / 100;
		healthFillBar.transform.localScale = new Vector3 (scaleX,1,4);
		SnakeHead.BulletHitAnim.Play ();
		SnakeHead.BulletHitPlayerAnim.Play ();
        //Debug.Log("final strength=" + strength);

        //		if (isPlayer)
        //			strength = 0;
  //      if (strength <= 0) {
		//	DeathRoutine();
		//	//		if (!snakeParameters.isPlayer && GameManagerSlither.challengeModeType==3 && obj.gameObject.layer == LayerMask.NameToLayer ("snake1")) {
		//	//if (!isPlayer && collidedObj.layer == LayerMask.NameToLayer ("snake1")) {
		//	//	Debug.Log ("Enemies killed count="+(GameManagerSlither.instance.enemiesKilledCount));
		//	//	GameManagerSlither.instance.enemiesKilledCount++;
		//	//	if (GameManagerSlither.challengeModeType == 3) {
		//	//		GameManagerSlither.instance.checkTargetKills ();
		//	//	}
		//	//}
		//}
		if (isPlayer) {
            //Debug.Log("reduce strength before damage event call");
            DamangeEvent();

            if (strength <= 0)
            {
                DeathRoutine();
            }
            GameManagerSlither.instance.camAnim.Play ();
			AudioClipManager.Instance.Play (InGameSounds.Hurt);
            //Debug.Log("reduce strength damage event call");
        }

    }
    void DamangeEvent()
    {
        Debug.Log("--- DamaeEvent");
        int damangevalue = 20;
        if (GameManagerSlither.instance.players.Count >= 2)
        {
            nakamaManager.SendMatchState(
                OpCodes.damage,
                MatchDataJson.damage(damangevalue)
            );
        }
    }
    public void PlayerDeathAnimation()
    {
        Debug.LogError("--- Player Death Animation");
        //GameManagerSlither.instance.destroyPlayer();//this is remote player die animation
        SnakeHead.gameObject.SetActive(false);
        DeathParticles.transform.position = SnakeHead.gameObject.transform.position;
        DeathParticles.SetActive(true);
        Destroy(this.gameObject, 1.5f);

    }
    public void setBulletType()
	{
//		Debug.Log ("setBulletType faceindex="+FaceIndex);
		switch (FaceIndex) {
		case 0:
		case 3:
		case 4:
		case 6:
		case 7:
		case 9:
		case 11:
		case 12:
		case 14:
			SnakeHead.bulletType = "Normal";
			break;
		case 1:
		case 5:
		case 10:
			SnakeHead.bulletType = "Arrow";
			break;
		case 2:
			SnakeHead.bulletType = "Blue";
			break;
		case 8:
			SnakeHead.bulletType = "Green";
			break;
		case 13:
			SnakeHead.bulletType = "Fire";
			break;
		}
	}
}
