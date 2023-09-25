using UnityEngine;
using System.Collections;

public class BGSoundManager : MonoBehaviour
{

	private static BGSoundManager _instance;

	public static BGSoundManager Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<BGSoundManager> ();
			}
			if (_instance == null) {
				GameObject gameObj = new GameObject ("BGSoundManager");
				_instance = gameObj.AddComponent<BGSoundManager> ();
			}
			return _instance;
		}
	}

	void Awake ()
	{
		if (!GetComponent<AudioSource> ()) {
			gameObject.AddComponent<AudioSource> ();
		}
		SetVolume (75);
		DontDestroyOnLoad (this);
	}

	public void PlayAudioClip (AudioClip audioClip)
	{
		if (GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().Stop ();
		}
		GetComponent<AudioSource> ().clip = audioClip;
		GetComponent<AudioSource> ().loop = true;
		GetComponent<AudioSource> ().Play ();
	}

	public void StopPlaying ()
	{
		if (GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().Stop ();
		}
	}

	public void SetVolume (float percent)
	{
		GetComponent<AudioSource> ().volume = percent * 0.01f;
	}

	public bool IsMute {
		get {
			return GetComponent<AudioSource> ().mute;
		}
		set {
			GetComponent<AudioSource> ().mute = value;
		}
	}

	public void ResumeSound()
	{
		GetComponent<AudioSource> ().Play ();
	}
	public void PauseSound()
	{
		GetComponent<AudioSource> ().Pause();
	}
	
}
