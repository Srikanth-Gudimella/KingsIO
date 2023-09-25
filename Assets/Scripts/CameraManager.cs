using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    public static Snake playerSnake;
    public static GameObject snakeHead;

    public static CameraManager mee;

    private void Awake()
    {
        mee = this;
    }

    Camera cam;
    float startOrtographic;
    float initialPosY;
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 60;
        cam = GetComponent<Camera>();
        cam.eventMask = 0;
        startOrtographic = cam.orthographicSize;
        startOrtographic = cam.fieldOfView;
        initialPosY = transform.position.y;
    }

    public static void setMySnakeAsPlayer(Snake _player,GameObject _snakeHead)
    {
        playerSnake = _player;
        snakeHead = _snakeHead;
    }

    // Update is called once per frame
    void LateUpdate()
    {
		if (playerSnake != null && !GameManagerSlither.instance.isPlayerBlasted)
        {
            Follow();
            Zoom();
        }

    }

    void Zoom()
    {
                float scale = playerSnake.referenceScale;
//        		cam.fieldOfView = Mathf.Lerp(cam.orthographicSize,startOrtographic + scale*20,1);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,startOrtographic + scale*4,1);

    }

    void Follow()
    {
        Vector3 playerPosition = playerSnake.SnakeHead.transform.position;
		if (playerPosition.z > 300) {
			playerPosition.z = 300;
		}
		else if (playerPosition.z < -300) {
			playerPosition.z = -300;
		}
		if (playerPosition.x > 600) {
			playerPosition.x = 600;
		}
		else if (playerPosition.x < -600) {
			playerPosition.x = -600;
		}
        //        playerPosition.y = transform.position.y;
        float scale = playerSnake.referenceScale;
        playerPosition.y = initialPosY + (scale - 1) * 50;
        transform.position = Vector3.Lerp(transform.position, playerPosition, 10 * Time.deltaTime);
        //		playerSnake.rotatingSpeed = 5 - (snakeHead.transform.localScale.x*1.1f);
    }
}
