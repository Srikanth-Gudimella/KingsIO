using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{

	// public variables
	[HideInInspector]
	public int
	AudioSourcesLength = 1;
	public GameObject CustomAudioSourcePrefab;

	public List<AudioClip> Clips;

	// private variables
	private List<PlaySound> AudioSources;
	private	bool isMute = false;

	private static SoundManager _instance;

	public delegate void SetVolumeDelegate (float percent);

	public static event SetVolumeDelegate SetVolumeEvent;

	public delegate void MuteDelegate (bool isMute);

	public static event MuteDelegate MuteEvent;

	public static SoundManager Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<SoundManager> ();
			}
			if (_instance == null) {
				GameObject gameObject = new GameObject ("SoundManager");
				_instance = gameObject.AddComponent<SoundManager> ();
			}
			return _instance;
		}
	}


	void Awake ()
	{
		DontDestroyOnLoad (this);
		if (AudioSources == null) {
			AudioSources = new List<PlaySound> ();
			CreateAudioSource ();
			CreateAudioSource ();
		}
	}

	PlaySound CreateAudioSource ()
	{
		AudioSource audioSource;
		if (CustomAudioSourcePrefab) {
			GameObject gameObj = Instantiate (CustomAudioSourcePrefab) as GameObject;
			audioSource = gameObj.GetComponent<AudioSource> ();
		} else {
			GameObject gameObj = new GameObject ();
			audioSource = gameObj.AddComponent<AudioSource> ();
		}
		audioSource.transform.parent = transform;
		PlaySound playSound = audioSource.gameObject.AddComponent<PlaySound> ();
		audioSource.mute = IsMute;
		audioSource.volume = 1.0f;
		AudioSources.Add (playSound);
		audioSource.name = "AudioSource" + AudioSources.Count;
		return playSound;
	}

	public void PlayAudioClip (AudioClip clip, float volume = 1.0f)
	{
		if (!clip) {
			//			Debug.Log ("SoundClip not given CHECK");
			return;
		}

		bool canPlay = false;
		for (int i = 0; i < AudioSources.Count; i++) {
			if (!AudioSources [i].isPlayingClip) {
				canPlay = true;
				StartCoroutine (AudioSources [i].PlayClip (clip, volume));
				break;
			}
		}
		if (!canPlay) {
			//srikanth
			//			Debug.Log ("Creating new AudioSource");//balutm
			//			StartCoroutine (CreateAudioSource ().PlayClip (clip, volume));
		}
	}


	public void PlayAudioClip (int clipNo)
	{
		PlayAudioClip (Clips [clipNo]);
	}

	public void PlayAudioClip (GameObject customAudioSourcePrefab, AudioClip clip)
	{
		CustomAudioSourcePrefab = customAudioSourcePrefab;
		PlayAudioClip (clip);
	}

	public void PlayAudioClip (GameObject customAudioSourcePrefab, int clipNo)
	{
		CustomAudioSourcePrefab = customAudioSourcePrefab;
		PlayAudioClip (clipNo);
	}

	public void SetVolume (float percent)
	{
		if (SetVolumeEvent != null) {
			SetVolumeEvent (percent * 0.01f);
		}
	}

	public bool IsMute {
		get {
			return isMute;
		}
		set {
			isMute = value;
			if (MuteEvent != null) {
				MuteEvent (value);
			}
		}
	}

	void OnLevelWasLoaded ()
	{
		for (int i = 0; i < AudioSources.Count; i++) {
			AudioSources [i].StopPlaying ();
		}
	}
}
