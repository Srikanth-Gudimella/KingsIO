using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingmover : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        pieceDistance = mysnakee.gameObject.GetComponent<Renderer>().bounds.size.z - (mysnakee.gameObject.GetComponent<Renderer>().bounds.size.z / 3);
        Debug.Log("tempHeight=" + pieceDistance);
        //InvokeRepeating("Updatee", 1, 1f);
    }

    public GameObject objj;

    // Update is called once per frame
    void Update()
    {
        return;
        //transform.position += new Vector3(0, 0, 1);
        if (myPoints.Count <= currentIndex)
        {
            //myPoints.Add(mysnakee.position.z);
            myPoints.Add(mysnakee.position);
            myPointsRot.Add(mysnakee.rotation);
            if (currentIndex == 0)
                myPieces.Add(refObj.transform);
            else
                myPieces.Add(Instantiate(refObj).transform);

            myPieces[myPieces.Count - 1].gameObject.GetComponentInChildren<TextMesh>().text = "" + currentIndex;
            myPieces[myPieces.Count - 1].gameObject.name = currentIndex + "";
            Debug.Log("created AT=" + currentIndex);

            resetMyPieces();

            currentIndex++;
        }
        else
        {
            //myPoints[currentIndex] = mysnakee.position.z;
            int tempLastPos = currentIndex - 1;
            if (tempLastPos < 0)
                tempLastPos = myPoints.Count - 1;

            float tempDist = Vector3.Distance(mysnakee.position, myPoints[tempLastPos]);
            //Debug.Log("distance=" + tempDist);
            if (tempDist > pieceDistance)
            {
                myPoints[currentIndex] = mysnakee.position;
                myPointsRot[currentIndex] = mysnakee.rotation;


                currentIndex++;

                if (currentIndex >= maxIdex)
                    currentIndex = 0;
            }

            resetMyPieces();
        }

        if (currentIndex >= maxIdex)
            currentIndex = 0;

    }

    private float pieceDistance = 0f;

    public Transform mysnakee;

    private int currentIndex = 0;
    public int maxIdex = 10;

    public List<Vector3> myPoints = new List<Vector3>();
    public List<Quaternion> myPointsRot = new List<Quaternion>();


    public List<Transform> myPieces = new List<Transform>();

    public GameObject refObj;

    private int frameCounter = 0;
    private void resetMyPieces()
    {
        if (frameCounter > 10)
        {
            frameCounter = 0;
            int tempCounter = currentIndex;
            if (tempCounter < 0)
            {
                tempCounter = myPoints.Count - 1;
            }

            for (int v = 0; v < myPieces.Count; v++)
            {
                //myPieces[v].position = new Vector3(0, 0, myPoints[tempCounter]);
                //Debug.Log("tempCounter=" + tempCounter);
                myPieces[v].position = myPoints[tempCounter];
                myPieces[v].rotation = myPointsRot[tempCounter];

                tempCounter--;

                if (tempCounter < 0)
                {
                    tempCounter = myPoints.Count - 1;
                }
            }
        }

        frameCounter++;
    }
}
