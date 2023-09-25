using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems; 

[System.Serializable]
public enum eMovementDirection
{
	Up, Down, Left, Right
}




public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public iTween.EaseType effectType;   
	public eMovementDirection Direction;
	
	bool _pressed = false;
	private bool mb_btnDown;
	public float valueX = -0.2f, valueY = -0.2f,timeValue=0.5f;
	void Start()
	{

	}
	
	public void OnPointerDown(PointerEventData eventData)
	{
		_pressed = true;
//				print ("Button Down");
		iTween.PunchScale (this.gameObject, iTween.Hash ("x",valueX,"y",valueY,"time",timeValue,"eastype",effectType,"IgnoreTimeScale",true));  //0.95
	}
	
	public void OnPointerUp(PointerEventData eventData)
	{
		_pressed = false;
//				print ("Button Up");
//		iTween.ScaleTo (this.gameObject, iTween.Hash ("x",1,"y",1,"time",0f,"eastype",effectType,"IgnoreTimeScale",true));        //1

	}
	
//	void Update()
//	{
//		if (_pressed)
//		{
//			if(mb_btnDown)
//				return;
//			ButtonDownAnim();
//		}
//		else
//		{
//			if(!mb_btnDown)
//				return;
//			ButtonUpAnim();  
//				
//		}
//		
//		switch(Direction)
//		{
//		case eMovementDirection.Up:
//			break;
//			
//			// (etc...)
//			
//		}
//	}

//	public void ButtonDownAnim()
//	{
//		mb_btnDown = true;
//
//		iTween.ScaleTo (this.gameObject, iTween.Hash ("x",0.9,"y",0.9,"time",0.1f,"eastype",effectType,"IgnoreTimeScale",true));  //0.95
//	}
//
//	public void ButtonUpAnim()
//	{
//		mb_btnDown = false;
//
//		iTween.ScaleTo (this.gameObject, iTween.Hash ("x",1,"y",1,"time",0f,"eastype",effectType,"IgnoreTimeScale",true));        //1
//	}
}
