using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class PlaySound : MonoBehaviour
{

	[HideInInspector]
	public bool
		isPlayingClip = false;

	void OnEnable ()
	{
		SoundManager.SetVolumeEvent += HandleSetVolumeEvent;
		SoundManager.MuteEvent += HandleMuteEvent;
	}

	void OnDisable ()
	{
		SoundManager.SetVolumeEvent -= HandleSetVolumeEvent;
		SoundManager.MuteEvent -= HandleMuteEvent;
	}

	void OnDestroy ()
	{
		SoundManager.SetVolumeEvent -= HandleSetVolumeEvent;
		SoundManager.MuteEvent -= HandleMuteEvent;
	}

	
	void HandleMuteEvent (bool isMute)
	{
		GetComponent<AudioSource> ().mute = isMute;
	}

	void HandleSetVolumeEvent (float percent)
	{
		GetComponent<AudioSource> ().volume = percent;
	}

	public IEnumerator PlayClip (AudioClip audioClip, float volume)
	{
		isPlayingClip = true;
		GetComponent<AudioSource> ().volume = volume;
		GetComponent<AudioSource> ().PlayOneShot (audioClip);
		yield return new WaitForSeconds (audioClip.length);
		isPlayingClip = false;
	}

	public void StopPlaying ()
	{
		GetComponent<AudioSource> ().Stop ();
//		isPlayingClip = false;
	}
}
