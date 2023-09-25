//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//[RequireComponent(typeof(PhotonView))]
//public class foodPickUp : Photon.MonoBehaviour, IPunObservable {
//	public bool PickupIsMine;
//	public bool isDestroyed;
//	public bool isMoveToPlayer=false;
//	public GameObject targetPlayer=null;
//	private Vector3 scaleValue;
//	public bool firstTime=false;
//	public LayerMask ristrictMask;
//
//
//	// Use this for initialization
//	void Start () {
////		Debug.Log ("--- foodPickUp Start");
//
//		isDestroyed = false;
//		isMoveToPlayer = false;
//		targetPlayer = null;
//		firstTime = true;
//	}
//
//	private Vector3 remotePosition;
//	private int photonStreamSentCount=0;
//	public void pickedUp()
//	{
////		Debug.Log ("--- foodPickUp pickedUp");
////		gameObject.SetActive (false);
//		firstTime=true;
////		scaleValue=Vector3.one*0.9f;
//		scaleValue = Vector3.one * (Random.Range (0.6f, 1.1f));
////		transform.localScale = scaleValue;	
//
//		isMoveToPlayer = false;
//		targetPlayer = null;
//		regenerateFood ();
////		this.photonView.RPC("PunPickup", PhotonTargets.AllViaServer,viewID);
//
//	}
//	void FixedUpdate()
//	{
//		if(isMoveToPlayer && targetPlayer!=null)
//		{
////			Debug.Log ("----- food picked up fixed update");
//			transform.position = Vector3.Lerp (transform.position, targetPlayer.transform.position+new Vector3(0,0,0.1f), Time.deltaTime*15);//*8
////			scaleValue=scaleValue*0.01f;
////			transform.localScale = scaleValue;
//		}
//	}
//
//	void regenerateFood()
//	{
//		gameObject.SetActive(true);
//		isDestroyed = false;
//
////		Vector3 myFoodPos = new Vector3 ( Random.Range(GameManager.mee.Min.x , GameManager.mee.Max.x )  , Random.Range(GameManager.mee.Min.y , GameManager.mee.Max.y) , 0 )  ;
////		gameObject.transform.localScale = Vector3.one*0.9f;
////		gameObject.transform.localPosition = myFoodPos;
//
//
//		generatePos();
//	}
//
//	void generatePos()
//	{
//		GameObject myPlayer = null;
//
//		Vector3 myPos = new Vector3 ( Random.Range(GameManager.mee.Min.x , GameManager.mee.Max.x )  , Random.Range(GameManager.mee.Min.y , GameManager.mee.Max.y) , 0 )  ;
//
//		Collider2D[] Collider = Physics2D.OverlapBoxAll (myPos, new Vector3 (3, 3, 0), 0, ristrictMask);
//		if (Collider.Length != 0) {
//			//			Debug.LogError ("-------------GeneratePos recheck again");
//			generatePos ();
//		} else {
//			//			Debug.LogError ("-------------GeneratePos create player");
//
////			gameObject.transform.localScale = Vector3.one*0.9f;
//			gameObject.transform.localScale = Vector3.one * (Random.Range (0.6f, 1.1f));
//
//			gameObject.transform.localPosition = myPos;
//
//		}
//
//	}
//
//	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
////		Debug.Log ("OnPhotonSerializeView");
//		if (stream.isWriting)
//		{
////			if (photonStreamSentCount<10 && PhotonNetwork.isMasterClient && gameObject.activeInHierarchy) {
//			if (PhotonNetwork.isMasterClient) {
//				stream.SendNext (transform.position);
//			}
////				photonStreamSentCount++;
////			}
//			//			stream.SendNext((byte)this._characterState);
//		}
//		else
//		{
//			this.remotePosition = (Vector3)stream.ReceiveNext();
//			transform.position = this.remotePosition;
//
//			if (firstTime) {
//				gameObject.SetActive(true);
////				transform.position = this.remotePosition;
//				gameObject.transform.localScale = Vector3.one*0.9f ;
//				firstTime = false;
//			}
//		}
//	}
//}
