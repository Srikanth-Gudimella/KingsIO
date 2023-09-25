using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joyStickPositionSet : MonoBehaviour,IPointerUpHandler,IPointerDownHandler {
	public GameObject joyStickParent;
	// Use this for initialization
	void Start () {
		
	}
	public virtual void OnPointerDown(PointerEventData ped)
	{
//				Debug.Log ("joystick position set OnPointerDown");
//		joyStickParent.transform.position=ped.position-new Vector2(50,50);
//		Virtualjoystick.mee.OnPointerDown (ped);
//		OnDrag (ped);
	}
	public virtual void OnPointerUp(PointerEventData ped)
	{
//		inputvector = Vector3.zero;
//		Joystickimage.rectTransform.anchoredPosition = Vector3.zero;
	}

}
