using UnityEngine;
using System.Collections;

public class SETTINGUTILS : MonoBehaviour
{

	public static bool music;
	public static bool sound;
	public static bool Notification = false;
	public static bool Hint;

	private static string musicKEY = "mkey";
	private static string soundKEY = "skey";
	public static string NotifKey = "NotifKey";
	private static string HintKEY = "Hintkey";
	//private static string hintsKEY = "hintkey";

	public const string LIVES_KEY = "GameLifes";

	public static void GetSaved_MusicStatus ()
	{
		if (PlayerPrefs.HasKey (musicKEY)) {

			if (PlayerPrefs.GetInt (musicKEY) == 1) {	
				music = true;
			} else {
				music = false;  
			}
		} else {
			music = true;
			saveMusicKey ();
		}
		SetSoundStatus ();
	}

	public static void GetSaved_SoundStatus ()
	{
		if (PlayerPrefs.HasKey (soundKEY)) {
			
			if (PlayerPrefs.GetInt (soundKEY) == 1) {	
				sound = true;
			} else {
				sound = false;  
			}
		} else {
			sound = true;
			saveSoundKey ();
		}
		SetSoundStatus ();
	}

	public static void saveMusicKey ()
	{
		if (music) { 
			PlayerPrefs.SetInt (musicKEY, 1);
		} else {
			PlayerPrefs.SetInt (musicKEY, 0);
		}
		SetSoundStatus ();
	}

	public static void saveSoundKey ()
	{
		if (sound) { 
			PlayerPrefs.SetInt (soundKEY, 1);
		} else {
			PlayerPrefs.SetInt (soundKEY, 0);
		}
		SetSoundStatus ();
	}
	public static void SetSoundStatus ()
	{
		BGSoundManager.Instance.IsMute = !music;
		LoopSoundManager.Instance.IsMute = !sound;
		SoundManager.Instance.IsMute = !sound;
	}
	public static void GetSaved_HintStatus ()
	{
		if (PlayerPrefs.HasKey (HintKEY)) {
			
			if (PlayerPrefs.GetInt (HintKEY) == 1) {	
				Hint = true;
			} else {
				Hint = false;  
			}
		} else {
			Hint = true;
//			if (TargetBoard.obj) {
//				TargetBoard.obj.HintOn ();
//			}
		}
	}
	public static  bool GetSaved_Notificationtatus ()
	{
		if (PlayerPrefs.HasKey (NotifKey)) {
			
			if (PlayerPrefs.GetInt (NotifKey) == 1) {	
				Notification = true;
			} else {
				Notification = false;  
			}
		} else {
			Notification = true;
//			if (TargetBoard.obj) {
//				TargetBoard.obj.Notific_On ();
//			}
		}
		return Notification;
	}
	public static void saveHintKey ()
	{
		if (Hint) { 
			PlayerPrefs.SetInt (HintKEY, 1);
		} else {
			PlayerPrefs.SetInt (HintKEY, 0);
		}
	}
	public static void saveNotifytKey ()
	{
		if (Notification) { 
			PlayerPrefs.SetInt (NotifKey, 1);
		} else {
			PlayerPrefs.SetInt (NotifKey, 0);
//			PushNotificator.ClearLocalNotifications ();
		}
	}
}
