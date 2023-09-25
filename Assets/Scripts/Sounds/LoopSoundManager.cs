using UnityEngine;
using System.Collections;

public class LoopSoundManager : MonoBehaviour {

    private static LoopSoundManager _instance;

    private bool isPauseCalled = false;

    public static LoopSoundManager Instance 
    {

        get {
            if(_instance == null) {
                _instance = GameObject.FindObjectOfType<LoopSoundManager>();
            }
            if(_instance == null) {
                GameObject gameObj = new GameObject("LoopSoundManager");
                _instance = gameObj.AddComponent<LoopSoundManager>();
            }
            return _instance;
        }
    }

    void Awake() {
        if (!GetComponent<AudioSource>()) {
            gameObject.AddComponent<AudioSource> ();
        }
        GetComponent<AudioSource>().loop = true;
    }

    public void Pause() {
        if(GetComponent<AudioSource>().isPlaying) {
            isPauseCalled = true;
            GetComponent<AudioSource>().Pause();
        }
    }

    public void Resume() {
        isPauseCalled = false;
        if(!GetComponent<AudioSource>().isPlaying) {
            GetComponent<AudioSource>().Play();
        }
    }

    public void Playclip(AudioClip clip) {
        if(isPauseCalled && GetComponent<AudioSource>().clip) {
            isPauseCalled = false;
            GetComponent<AudioSource>().Play();
            return;
        }
        if(GetComponent<AudioSource>().isPlaying){
            GetComponent<AudioSource>().Stop();
        }

        if(clip) {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
        }
    }

    public void Stop() {
        isPauseCalled = false;
        if(GetComponent<AudioSource>().isPlaying) {
            GetComponent<AudioSource>().Stop();
        }
    }

    public bool IsMute {
        get {
            return GetComponent<AudioSource>().mute;
        }
        set {
            GetComponent<AudioSource>().mute = value;
        }
    }

	public void SetVolume(float percent) {
		GetComponent<AudioSource>().volume = percent/100;
	}

}
