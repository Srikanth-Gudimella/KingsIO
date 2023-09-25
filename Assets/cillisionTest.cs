using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cillisionTest : MonoBehaviour {

    // Use this for initialization
    public List<Transform> myTrans = new List<Transform>();
    public List<Rigidbody> myRigs = new List<Rigidbody>();
    void Start () {
		for(int v = 0; v<80; v++)
        {
            for (int t = 0; t<80; t++)
            {
                GameObject pcc = Instantiate(piecee);
                pcc.SetActive(true); 
                pcc.transform.position = new Vector3(5*v, 0, 5*t);
                myTrans.Add(pcc.transform);
                myRigs.Add(pcc.GetComponent<Rigidbody>());
            }
        }

        Debug.Log("total="+ myRigs.Count);
	}

    public GameObject piecee;
	
	// Update is called once per frame
	void Update () {
        MoveRigidbody();
        //Debug.Log("time="+Time.frameCount % 8);
        //return;
        var sw = System.Diagnostics.Stopwatch.StartNew();
        int ccc = myTrans.Count;
        for (int v = 0; v<ccc; v++)
        {
            //myTrans[v].localPosition = myTrans[v].localPosition + new Vector3(0.1f, 0.001f, 0); //16MS
            myRigs[v].position = myRigs[v].position + new Vector3(0.1f, 0.00001f, 0); //6MS

            myTrans[v].rotation = m_Rigidbody.transform.rotation; // 16MS
            //myRigs[v].rotation = m_Rigidbody.transform.rotation; //5MS
        }
        sw.Reset();
        sw.Start();
        for (int v = 0; v < ccc; v++)
        {

            Vector3 mytemp = myTrans[v].position;//1MS
            //Vector3 mytemp = myRigs[v].position;
        }
        long csharpTime = sw.ElapsedMilliseconds;

        Debug.Log("Time Took="+ csharpTime);
	}

    public Rigidbody m_Rigidbody;
    public float m_Speed = 2;
    void MoveRigidbody()
    {

        m_Rigidbody.velocity = m_Rigidbody.transform.forward * m_Speed;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            //m_Rigidbody.velocity = m_Rigidbody.transform.forward * m_Speed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
            //m_Rigidbody.velocity = -m_Rigidbody.transform.forward * m_Speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Rotate the sprite about the Y axis in the positive direction
            m_Rigidbody.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * m_Speed * 2, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Rotate the sprite about the Y axis in the negative direction
            m_Rigidbody.transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * m_Speed * 2, Space.World);
        }
    }

}
