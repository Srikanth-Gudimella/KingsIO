using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Virtualjoystick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {


	public static Virtualjoystick mee;
	public GameObject BGImg, ControlsBackImg;
	public Image Bgimage;
	private Image Joystickimage;
	private Vector3 inputvector;
	//	public Vector3 joyStickDefaultPos;
	public int Sidecount = 0;
	private Vector3 leftJoyStickPos=new Vector3(-430,-170,0);
	private Vector3 rightJoyStickPos=new Vector3(430,-170,0);

	private void Awake()
	{
		mee = this;
	}
	private void Start()
	{
		//		Bgimage = GetComponent<Image> ();
		Joystickimage = transform.GetChild (0).GetComponent<Image> ();
		//		Debug.Log ("JoyStickType="+GameManager.joyStickType);
		//GameManagerSlither.joyStickType=2;
		SetJoyStickType();

	}
	public void SetJoyStickType()
	{
		switch (GameManagerSlither.joyStickType) {
		case 0:
			gameObject.transform.parent.transform.localPosition = leftJoyStickPos;
			GameManagerSlither.instance.shootBtn.transform.localPosition = new Vector3 (500, GameManagerSlither.instance.shootBtn.transform.localPosition.y, 0);
			//			Bgimage.rectTransform.sizeDelta = new Vector2 (100, 100);
			//			Bgimage = ControlsBackImg.GetComponent<Image>();
			//			BGImg.GetComponent<Image> ().enabled = false;
			break;
		case 1:
			gameObject.transform.parent.transform.localPosition = rightJoyStickPos;
			GameManagerSlither.instance.shootBtn.transform.localPosition = new Vector3 (-500, GameManagerSlither.instance.shootBtn.transform.localPosition.y, 0);
			//			Bgimage = ControlsBackImg.GetComponent<Image>();
			//			BGImg.GetComponent<Image> ().enabled = false;
			break;
		case 2:
			gameObject.transform.parent.transform.localPosition = rightJoyStickPos;
			GameManagerSlither.instance.shootBtn.transform.localPosition = new Vector3 (-500, GameManagerSlither.instance.shootBtn.transform.localPosition.y, 0);

			//			Bgimage = BGImg.GetComponent<Image>();
			//			BGImg.GetComponent<Image> ().enabled = true;
			break;
		}
	}
	public virtual void OnDrag(PointerEventData ped)
	{

		//working 
		//		Debug.Log("OnDrag");
		if (Sidecount == 0) 
		{
			Vector2 pos;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (Bgimage.rectTransform, ped.position, ped.pressEventCamera, out pos)) 

			{
				pos.x = (pos.x / Bgimage.rectTransform.sizeDelta.x);
				pos.y = (pos.y / Bgimage.rectTransform.sizeDelta.y);


				inputvector = new Vector3 (pos.x * 2 + 1, 0, pos.y * 2 - 1);
				//				inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized : inputvector;
				//				Debug.Log("inputVector magnitude="+(inputvector.magnitude));
				inputvector = (inputvector.magnitude > 0.10f) ? inputvector.normalized*0.10f : inputvector;

				//				Debug.Log ("pos " + pos + " input " + inputvector);

				Joystickimage.rectTransform.anchoredPosition = new Vector3 (inputvector.x * (Bgimage.rectTransform.sizeDelta.x / 5), inputvector.z * (Bgimage.rectTransform.sizeDelta.y / 5));

			}
		}
		else 
		{
			Vector2 pos;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (Bgimage.rectTransform, ped.position, ped.pressEventCamera, out pos))
			{


				pos.x = (pos.x / Bgimage.rectTransform.sizeDelta.x);
				pos.y = (pos.y / Bgimage.rectTransform.sizeDelta.y);



				inputvector = new Vector3 (1-(pos.x*2), 0,pos.y * 2 - 1);
				inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized : inputvector;

				//				Debug.Log ("pos " + pos + " input " + inputvector);
				Joystickimage.rectTransform.anchoredPosition = new Vector3 (-inputvector.x * (Bgimage.rectTransform.sizeDelta.x / 2), inputvector.z * (Bgimage.rectTransform.sizeDelta.y / 2));

			}

		}


	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		//		Debug.Log ("OnPointerDown");

		switch (GameManagerSlither.joyStickType) {
		case 0:
			gameObject.transform.parent.transform.localPosition = leftJoyStickPos;
			break;
		case 1:
			gameObject.transform.parent.transform.localPosition = rightJoyStickPos;
			break;		
		case 2:
			gameObject.transform.parent.transform.position=ped.position;//-new Vector2(250,250);
			break;
		}
		//		gameObject.transform.parent.transform.position=ped.position;//-new Vector2(250,250);
		OnDrag (ped);
	}



	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputvector = Vector3.zero;
		Joystickimage.rectTransform.anchoredPosition = Vector3.zero;

		switch (GameManagerSlither.joyStickType) {
		case 0:
			gameObject.transform.parent.transform.localPosition = leftJoyStickPos;
			break;
		case 1:
			gameObject.transform.parent.transform.localPosition = rightJoyStickPos;
			break;		
		case 2:
			gameObject.transform.parent.transform.localPosition = rightJoyStickPos;
			break;
		}
	}

	public float Horizontal()
	{
		if (inputvector.x != 0)
			return inputvector.x;
		else {
			float horizontalValue=Input.GetAxis ("Horizontal");
			//			horizontalValue = (horizontalValue > 0.2f) ? 0.2f : horizontalValue;
			if (horizontalValue > 0.1f) {
				horizontalValue = 0.1f;
			}
			if (horizontalValue < -0.1f) {
				horizontalValue = -0.1f;
			}
			return horizontalValue;
		}
	}

	public float Vertical()
	{
		if (inputvector.z != 0)
			return inputvector.z;
		else {
			float verticalValue=Input.GetAxis ("Vertical");
			if (verticalValue > 0.1f) {
				verticalValue = 0.1f;
			}
			if (verticalValue < -0.1f) {
				verticalValue = -0.1f;
			}

			return verticalValue;
			//			return Input.GetAxis ("Vertical");
		}
	}



}
