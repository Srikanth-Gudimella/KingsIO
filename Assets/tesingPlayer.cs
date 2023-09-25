using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesingPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.logMyPos(gameObject);
	}

    private void OnsTriggerEnter(Collider other)
    {
        //Debug.Log("somethiTrigger="+other.name);
    }
}
