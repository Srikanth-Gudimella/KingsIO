using UnityEngine;
using System.Collections;

public class AudioClipManager : MonoBehaviour
{
	public AudioClip[] Sounds;

	#region "Getter"

	private static AudioClipManager _instance;

	public static AudioClipManager Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<AudioClipManager> ();
			}
			return _instance;
		}
	}

	#endregion

	public void Play (MenuSounds menuSound)
	{
		switch (menuSound) {
		case MenuSounds.BG:
//			Debug.Log ("play menu bg 11111");
			if (canPlay ((int)menuSound)) {
//				Debug.Log ("play menu bg 22222=" + (Sounds [(int)menuSound]));
				BGSoundManager.Instance.PlayAudioClip (Sounds [(int)menuSound]);
			}
			break;
		case MenuSounds.LoadingIn:
			if (canPlay ((int)menuSound)) {
				SoundManager.Instance.PlayAudioClip (Sounds [(int)menuSound], 0.6f);	
			}
			break;
		case MenuSounds.LoadingOut:
			if (canPlay ((int)menuSound)) {
				SoundManager.Instance.PlayAudioClip (Sounds [(int)menuSound], 0.6f);
			}
			break;
		default:
			if (canPlay ((int)menuSound)) {
				SoundManager.Instance.PlayAudioClip (Sounds [(int)menuSound]);
			}
			break;
		}
	}




	public void Play (InGameSounds soundIndex)
	{
		switch (soundIndex) {
		case InGameSounds.BG:
			if (canPlay ((int)soundIndex)) {
				BGSoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex]);
			}
			break;
		default:
			if (canPlay ((int)soundIndex)) {
				switch (soundIndex) {

				case InGameSounds.BG:
					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 1f);//0.5f
					break;
				case InGameSounds.ResultPage:
					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 0.7f);//0.5f
					break;
				case InGameSounds.EatFood:
					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 0.2f);//0.5f
					break;
//				case InGameSounds.LevelComplete:
//					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 0.8f);//0.5f
//					break;
				/*case InGameSounds.LevelCompleteStar:
					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 1f);
					break;
				
				case InGameSounds.Popupclose:
					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 1f);
//					StartCoroutine (BGvolumeChanger (Sounds [(int)soundIndex].length));
					break;
				case InGameSounds.Frozen:
					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 1f);
					//	StartCoroutine (BGvolumeChanger (Sounds [(int)soundIndex].le ngth));
					break;
				case InGameSounds.Rock:
					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 1);
					//	StartCoroutine (BGvolumeChanger (Sounds [(int)soundIndex].le ngth));
					break;
				case InGameSounds.LoadingIn:
					if (canPlay ((int)soundIndex)) {
						SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 0.6f);	
					}
					break;
				case InGameSounds.LoadingOut:
					if (canPlay ((int)soundIndex)) {
						SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 0.6f);
					}
					break;
				case InGameSounds.SuperstarChef:
					if (canPlay ((int)soundIndex)) {
						SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 1);
					}
					break;
				case InGameSounds.ElementsCount:
					if (canPlay ((int)soundIndex)) {
						SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex], 0.5f);
					}*/
					break;
				default :
					SoundManager.Instance.PlayAudioClip (Sounds [(int)soundIndex]);
					break;
				}
			}
			break;
		}
	}

	IEnumerator PlaywithDelay (int soundIndex)
	{
		yield return new WaitForSeconds (0);
		SoundManager.Instance.PlayAudioClip (Sounds [soundIndex], 0.5f);
	}

	public void Play (int soundIndex)
	{
		switch (soundIndex) {
		case 0:
			if (canPlay ((int)soundIndex)) {
				BGSoundManager.Instance.PlayAudioClip (Sounds [soundIndex]);

			}
			break;
		case 4:
			if (canPlay ((int)soundIndex)) {
				SoundManager.Instance.PlayAudioClip (Sounds [soundIndex]);
//				StartCoroutine (BGvolumeChanger (Sounds [soundIndex].length));
			}
			break;
		default:
			if (canPlay ((int)soundIndex)) {
				SoundManager.Instance.PlayAudioClip (Sounds [soundIndex]);
			}
			break;
		}
	}

	public IEnumerator BGvolumeChanger (float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		BGSoundManager.Instance.SetVolume (100);
	}



	private bool canPlay (int index)
	{
		if ((index < Sounds.Length) && Sounds [index] != null) {
			return true;
		}
		return false;
	}

}

public enum MenuSounds
{
	BG,
	Button,
	Loading,
	LoadingIn,
	LoadingOut,
	MenuIntro,
	CharacterSound,
	VSSound
}



public enum InGameSounds
{
	BG = 0,
	Button = 1,
	PageEffect = 2,
	EatFood = 3,
	ResultPage = 4,
	ChallengeComplete = 5,
	playerBooster=6,
	AIBooster=7,
	Fire,
	OpponentShoot,
	Hurt
}
