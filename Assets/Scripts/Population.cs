using UnityEngine;
using System.Collections.Generic;

public class Population : MonoBehaviour
{
    public int small = 20;
    public int medium = 10;
    public static Population instance;
    public int big = 5;
    public int photonSnakesCount = 0;
    public float spawnCircleLenght = 1000;
    public GameObject snakePrefab;
    public bool isStopPopulation = false;

    public float RangeX, RangeZ;
	public int snakesIndex,attackingPercentage;
    public GameObject NetworkLocalPlayerPrefab, NetworkRemotePlayerPrefab;

    void Awake()
    {
        instance = this;
        freeLayerIndex.Add(1);
        freeLayerIndex.Add(2);
        //freeLayerIndex.Add(4); water is using for food.
    }
    // Use this for initialization
    void Start()
    {
        
//        if (!isStopPopulation)
//        {
//                SpawnPopulation();
//           
//        }

       
    }
	public void StartPopulation()
	{
		if (!isStopPopulation)
		{
			SpawnPopulation();

		}
	}

    public static GameObject photonObj;

    public void SpawnPopulation()
    {
        return;
        //        if (GameManagerSlither.isForceOfflineMode)
        //        {
        //            small = 1;
        //            medium = 0;
        //            big = 0;
        //        }

        
        //for (int i = 0; i < small; i++)
        //{
        //    SpawnSnake(Random.Range(200, 500));
        //}
        //for (int i = 0; i < medium; i++)
        //{
        //    SpawnSnake(Random.Range(2500, 4000));
        //}
        //for (int i = 0; i < big; i++)
        //{
        //    SpawnSnake(Random.Range(6500, 12500));
        //}

//        Snake.all_AIs_Mine = true;
        Debug.LogError("You are master,You created Snake AI's..!");
    }

    private int snakeLayerIndex = 9;

    [HideInInspector]
    public List<int> freeLayerIndex = new List<int>();

    public List<Transform> spawnPoses;

    public List<Snake> freeSnakesToSpawn = new List<Snake>();

    int posCheckCount = 0;
    public LayerMask onlyPlayerMask;
    void generatePos()
    {
        Vector3 tempPos = Vector3.zero;
        randomSpawnCircle = new Vector3(Random.Range(-RangeX, RangeX), 0, Random.Range(RangeZ, -RangeZ));
        randomSpawnCircle = new Vector3(-RangeX, 0, RangeZ);
        Collider[] Collider = Physics.OverlapSphere(randomSpawnCircle, 400, onlyPlayerMask);
        //		Debug.LogError ("----------- generatePos collider length="+Collider.Length);
        //		for (int i = 0; i < Collider.Length; i++) {
        //			Debug.Log ("GameObject name="+(Collider[i].gameObject.name));
        //		}
        if (randomSpawnCircle.DistanceBtmSuperFast(Camera.main.transform.position) >= 350 * 350)
        {
            tempPos = randomSpawnCircle;
        }
        if ((Collider.Length != 0 && randomSpawnCircle.DistanceBtmSuperFast(Camera.main.transform.position) <= 350 * 350) && posCheckCount < 20)
        {
            //			Debug.LogError ("-------------GeneratePos recheck again");
            posCheckCount++;
            generatePos();
        }
        else if (Collider.Length != 0 && posCheckCount == 20 && tempPos != Vector3.zero)
        {
            randomSpawnCircle = tempPos;
        }
    }
    Vector3 randomSpawnCircle;
    public static int currentSnakeCountIndex = 1;
    public static List<bool> allSnakesVisibleValues = new List<bool>() { true };

    public void resetPopulationOnDie()
    {
        allSnakesVisibleValues.Clear();
        allSnakesVisibleValues.Add(true);
        currentSnakeCountIndex = 1;
		snakesIndex = 0;
    }

    public static void setVisibleIndexesAndCurrentSnakeCountIndexes(Snake snakeObj)
    {
//        Debug.Log("setVisibleIndexesAndCurrentSnakeCountIndexes=" + currentSnakeCountIndex);
        snakeObj.mySnakeRenderIndexInAllSnakes = currentSnakeCountIndex;
        allSnakesVisibleValues.Add(false);

        if (currentSnakeCountIndex > (instance.small + instance.medium + instance.big + instance.photonSnakesCount))
        {
            Debug.LogError("How we got new Snake..? currentSnakeCountIndex=" + currentSnakeCountIndex + " totalrequire=" + (instance.small + instance.medium + instance.big + instance.photonSnakesCount));
        }

        currentSnakeCountIndex++;
    }

    public GameObject SpawnSnake(int points,bool IsMyPlayer)
    {
        Debug.Log("SpawnSnake 111111");
        //Transform spwanPointt = spawnPoses[Random.Range(0, spawnPoses.Count)];
        //Vector3 randomSpawnCircle = new Vector3(Random.Range(-Population.instance.RangeX, Population.instance.RangeX), 0, Random.Range(-Population.instance.RangeZ, Population.instance.RangeZ));
        //if (spwanPointt == null)
        //{
        //    spwanPointt = snakePrefab.transform;
        //}
        Debug.Log("SpawnSnake 2222222");

        //		Vector3 randomSpawnCircle = new Vector3(Random.Range(-Population.instance.RangeX,Population.instance.RangeX), 0, Random.Range(-Population.instance.RangeZ,Population.instance.RangeZ));
        posCheckCount = 0;
        generatePos();
        Debug.Log("SpawnSnake 333333");

        GameObject newsnake;
        Snake snakeparams;

        //if (freeSnakesToSpawn.Count > 0)
        //{
        //    //			Debug.LogError ("-----free snake spawn");
        //    newsnake = freeSnakesToSpawn[0].gameObject;
        //    snakeparams = freeSnakesToSpawn[0];
        //    snakeparams.resetMe();
        //    freeSnakesToSpawn.RemoveAt(0);
        //    //            newsnake.transform.position = spwanPointt.position;//BalueCode
        //    newsnake.transform.position = randomSpawnCircle;//srikanth added
        //}
        //else
        {
            //            newsnake = (GameObject)Instantiate(snakePrefab, spwanPointt.position, snakePrefab.transform.rotation);//balu code
            //			Debug.LogError ("-----new snake spawn");
            if(IsMyPlayer)
            {
                Debug.Log("-------- Instantiate localplayerprefab");
                newsnake = (GameObject)Instantiate(NetworkLocalPlayerPrefab, randomSpawnCircle, snakePrefab.transform.rotation);
            }
            else
            {
                Debug.Log("-------- Instantiate remoteplayerprefab");
                newsnake = (GameObject)Instantiate(NetworkRemotePlayerPrefab, randomSpawnCircle, snakePrefab.transform.rotation);

                //newsnake.GetComponent<PlayerNetworkRemoteSync>().NetworkData = new RemotePlayerNetworkData
                //{
                //    MatchId = matchId,
                //    User = user
                //};

            }
            //newsnake = (GameObject)Instantiate(snakePrefab, randomSpawnCircle, snakePrefab.transform.rotation);//srikanth added

            Debug.Log("SpawnSnake 44444");

            snakeparams = newsnake.GetComponent<Snake>();
            setVisibleIndexesAndCurrentSnakeCountIndexes(snakeparams);
            //set Layer only for New Snake...
            SetAvailbleLayerToMySnake(snakeparams);
            Debug.Log("SpawnSnake 55555");

            //            Vector2 randomSpawnCircleVector2 = Random.insideUnitCircle * spawnCircleLenght;
            //Vector3 randomSpawnCircle = new Vector3(randomSpawnCircleVector2.x, newsnake.transform.position.y, randomSpawnCircleVector2.y);
            //            Vector3 randomSpawnCircle = new Vector3(Random.Range(-Population.instance.RangeX, Population.instance.RangeX), newsnake.transform.position.y, Random.Range(-Population.instance.RangeZ, Population.instance.RangeZ));//srikanth commented
            newsnake.transform.position = randomSpawnCircle;
            snakeparams.addPointsOnFoodForPlayer(points);
            Debug.LogError("------------- Snake isPlayerSet IsMyPlayer="+IsMyPlayer);

            snakeparams.isPlayer = IsMyPlayer;
            snakeparams.SnakeHead.IsPlayer = IsMyPlayer;
        }

        if (currentSnakeCountIndex != allSnakesVisibleValues.Count)
        {
            Debug.LogError("Should be equal " + currentSnakeCountIndex + "==" + allSnakesVisibleValues.Count);
        }
        Debug.Log("SpawnSnake 66666");

        snakeparams.GetARandomTemplate();
        Debug.Log("SpawnSnake 7777777");

        //        GameManagerSlither.setTotalSnakesCountTxt(Population.currentSnakeCountIndex);
       // snakeparams.snakePieces[0].GetComponent<SnakeHead>().SetColorBasedOnTemplate();//srikanth 
        Debug.Log("SpawnSnake 888888");

        //for (int i = 1; i < snakeparams.snakePieces.Count; i++)
        //{
        //    snakeparams.snakePieces[i].GetComponent<Piece>().InitializePiece(i, snakeparams);
        //}
        Debug.Log("SpawnSnake 9999999");

        //////////srikanth 

        GameManagerSlither.instance.AIPlayerNameIndex++;
            //Debug.Log("SpawningSnakes="+ GameManagerSlither.instance.AIPlayerNameIndex);
            //		Debug.Log ("enemy playername ="+GameManagerSlither.instance.AIPlayerNameIndex+":::"+(GameManagerSlither.instance.AIPlayrNamesNew [GameManagerSlither.instance.AIPlayerNameIndex]));
            if (!GameManagerSlither.isForceOfflineMode)
            {
                if (GameManagerSlither.instance.AIPlayerNameIndex > GameManagerSlither.instance.AIOfflinePlayrNames.Length - 1)
                {
                    GameManagerSlither.instance.AIPlayerNameIndex = 0;
                }
                newsnake.GetComponent<Snake>().playerDetails.text = GameManagerSlither.instance.AIOfflinePlayrNames[GameManagerSlither.instance.AIPlayerNameIndex];
                newsnake.GetComponent<Snake>().playerName = GameManagerSlither.instance.AIOfflinePlayrNames[GameManagerSlither.instance.AIPlayerNameIndex];
            }
            else
            {
                if (GameManagerSlither.instance.AIPlayerNameIndex > GameManagerSlither.instance.AIOnlinePlayrNames.Length - 1)
                {
                    GameManagerSlither.instance.AIPlayerNameIndex = 0;
                }
                newsnake.GetComponent<Snake>().playerDetails.text = GameManagerSlither.instance.AIOnlinePlayrNames[GameManagerSlither.instance.AIPlayerNameIndex] + "  " + newsnake.GetComponent<Snake>().getPoints();
                newsnake.GetComponent<Snake>().playerName = GameManagerSlither.instance.AIOnlinePlayrNames[GameManagerSlither.instance.AIPlayerNameIndex];
            }
        return newsnake;

    }

    public void SetAvailbleLayerToMySnake(Snake snakeparams)
    {
        if (snakeLayerIndex < 32)
        {
            snakeparams.gameObject.layer = snakeLayerIndex;
            snakeparams.SnakeHead.gameObject.layer = snakeLayerIndex;
            snakeparams.aiModule.gameObject.layer = snakeLayerIndex;//Srikanth added this to activate AI Escape before this all AI Module having snake1 layer so not escaping from our snake..
            snakeLayerIndex++;
        }
        else
        {
            if (freeLayerIndex.Count > 0)
            {
                snakeparams.gameObject.layer = freeLayerIndex[0];
                freeLayerIndex.RemoveAt(0);
            }
            else
            {
                Debug.LogError("No Snake Layer..???");
                snakeparams.gameObject.layer = 0;
            }
        }
    }


    //Balutm Multiplayer...!

    public GameObject multiPlayerObjRef;

}
