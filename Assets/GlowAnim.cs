using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowAnim : MonoBehaviour {
	private Animation anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		Invoke ("startAnim",(Random.Range(1,5)));
	}
	void startAnim()
	{
		anim.Play ();
	}
}
