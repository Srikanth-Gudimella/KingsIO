using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        freeLayerIndex.Add(22);
        freeLayerIndex.Add(23);
    }

    public List<int> freeLayerIndex = new List<int>();
    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("OnTriggerEnter="+obj.name);
        //freeLayerIndex.RemoveAt(0);
        //Debug.Log("OnTriggerEnter=" + obj.name + freeLayerIndex[0]);
    }

    private float counter = 0.1f;
    private void Update()
    {
        Debug.Log("myrend="+ myrend.isVisible);
        transform.position += new Vector3(counter, 0, 0);
    }

    public Renderer myrend;

    void OnBecameVisible()
    {
        //enabled = true;
        //Debug.Log("onVisibleTruee..!"+name);
    }

    private void OnBecameInvisible()
    {
        //Debug.Log("OnHide....!"+ name);
        //counter = -0.5f;
    }
}
