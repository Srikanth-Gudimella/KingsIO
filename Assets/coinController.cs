//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class coinController : MonoBehaviour {
//	public static coinController mee;
//	public LayerMask ristrictMask;
//
//	void Awake()
//	{
//		mee = this;
//	}
//	// Use this for initialization
//	void Start () {
//		Vector3 myPos = new Vector3 ( Random.Range(GameManager.mee.Min.x , GameManager.mee.Max.x )  , Random.Range(GameManager.mee.Min.y , GameManager.mee.Max.y) , 0 )  ;
//		gameObject.transform.localPosition = myPos;
//	}
//
//	public void pickedUp()
//	{
////		Debug.Log ("--- booster pickedUp");
//		regenerateCoin ();
//	}
//	void regenerateCoin()
//	{
//		gameObject.SetActive(true);
////		Vector3 myPos = new Vector3 ( Random.Range(GameManager.mee.Min.x , GameManager.mee.Max.x )  , Random.Range(GameManager.mee.Min.y , GameManager.mee.Max.y) , 0 )  ;
////		gameObject.transform.localPosition = myPos;
//		generatePos ();
//	}
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
//			gameObject.transform.localPosition = myPos;
//
//		}
//
//	}
//	private Vector3 remotePosition;
//
//	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
//		//		Debug.Log ("OnPhotonSerializeView");
//		if (stream.isWriting)
//		{
//			//			if (photonStreamSentCount<10 && PhotonNetwork.isMasterClient && gameObject.activeInHierarchy) {
//			if (PhotonNetwork.isMasterClient) {
//				stream.SendNext (transform.position);
//			}
//		}
//		else
//		{
//			this.remotePosition = (Vector3)stream.ReceiveNext();
//			gameObject.SetActive(true);
//			transform.position = this.remotePosition;
//		}
//	}
//}