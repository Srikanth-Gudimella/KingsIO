using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeVisibleCodeMover : MonoBehaviour
{
    [HideInInspector]
    public Snake mySnake;
    [HideInInspector]
    public int myPieceIndex = -1;
    // Use this for initialization

    void OnBecameVisible()//OnBecameVisible
    {
        if (mySnake.isPlayer)
            return;
        //Debug.Log("onBecameVisible="+gameObject.name);
#if UNITY_EDITOR
        if (Camera.current.name == "SceneCamera")
            return;
#endif

        if (!mySnake.isSnakeInCame)
        {
            mySnake.resetBeforeSmooth();
        }

        mySnake.isSnakeInCame = true;
        mySnake.myVisiblePointsMover[myPieceIndex] = true;
        AllSankesInCam = 0;
        //Debug.Log("Population.currentSnakeCountIndex="+ Population.currentSnakeCountIndex+ " Snake.allSnakesVisibleValuesCount="+ Population.allSnakesVisibleValues.Count);
        for (int v = 0; v < Population.currentSnakeCountIndex; v++)
        {
            if (Population.allSnakesVisibleValues[v])
            {
                AllSankesInCam++;
            }
        }

//        GameManagerSlither.setInCamSnakesCountTxt(AllSankesInCam);
    }

    void OnBecameInvisible()
    {
        if (mySnake.isPlayer)
            return;
#if UNITY_EDITOR
        if (Camera.current != null && Camera.current.name == "SceneCamera")
            return;
#endif
        //Debug.Log("myPieceIndex="+ myPieceIndex+ " mySnake.myVisiblePointsMover="+ mySnake.myVisiblePointsMover.Count);
        mySnake.myVisiblePointsMover[myPieceIndex] = false;

        int snakeVisiblePointsCount = mySnake.myVisiblePointsMover.Count;
        for (int v = 0; v < snakeVisiblePointsCount; v++)
        {
            if (mySnake.myVisiblePointsMover[v] == true)
            {
                return;
            }
        }
        mySnake.isSnakeInCame = false;
    }

    //public bool myVisible = false;

    private static int AllSankesInCam = 0;
}
