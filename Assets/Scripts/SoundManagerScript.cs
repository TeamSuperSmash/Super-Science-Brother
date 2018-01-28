using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum AudioClipID
{
	// Main Menu
	BGM_MAIN_MENU = 0,

	// Events
	SFX_EXPMASSBOMB = 1,
	SFX_LOSE = 2,
	SFX_RESTORETILES = 3,
	SFX_WIN = 4,

	// Misc
	SFX_BREAKTHRESHOLD1 = 5,
	SFX_BREAKTHRESHOLD2 = 6,
	SFX_FATBOY = 7,
	SFX_FORCEPUSH = 8,
	SFX_GETTINGPUSH = 9,
	SFX_MASSGUN1 = 10,
	SFX_MASSGUN2 = 11,
	SFX_MASSSHIELD = 12,

	// Voices
	SFX_JUMP1 = 13,
	SFX_JUMP2 = 14,
	SFX_JUMP3 = 15,
	SFX_JUMP4 = 16,

	// Musics
	BGM_LEVEL1ARENA = 100,
	BGM_LEVEL2ARENA = 101,
	BGM_LEVEL3ARENA = 102,

	TOTAL = 9001
}

[System.Serializable]
public class AudioClipInfo
{
	public AudioClipID audioClipID;
	public AudioClip audioClip;
}

public class SoundManagerScript : MonoBehaviour
{
	#region Singleton

	private static SoundManagerScript mInstance;

	public static SoundManagerScript Instance {
		get {
			if (mInstance == null) {
				SoundManagerScript temp = ManagerControllerScript.Instance.soundManager;

				if (temp == null) {
					temp = Instantiate (ManagerControllerScript.Instance.soundManagerPrefab, Vector3.zero, Quaternion.identity).GetComponent<SoundManagerScript> ();
				}
				mInstance = temp;
				ManagerControllerScript.Instance.soundManager = mInstance;
				DontDestroyOnLoad (mInstance.gameObject);
			}
			return mInstance;
		}
	}

	public static bool CheckInstanceExist ()
	{
		return mInstance;
	}

	#endregion Singleton

	public float bgmVolume = 1.0f;
	public float sfxVolume = 1.0f;
	public float brightness = 1.0f;


	public List<AudioClipInfo> audioClipInfoList = new List<AudioClipInfo> ();

	public AudioSource bgmAudioSource;
	public AudioSource sfxAudioSource;
	public Image brightnessMask;

	public List<AudioSource> sfxAudioSourceList = new List<AudioSource> ();
	public List<AudioSource> bgmAudioSourceList = new List<AudioSource> ();

	// Preload before any Start() rins in other scripts
	void Awake ()
	{
		if (SoundManagerScript.CheckInstanceExist ()) {
			Destroy (this.gameObject);
		}

		AudioSource[] audioSourceList = this.GetComponentsInChildren<AudioSource> ();

		if (audioSourceList [0].gameObject.name == "BGMAudioSource") {
			bgmAudioSource = audioSourceList [0];
			sfxAudioSource = audioSourceList [1];
		} else {
			bgmAudioSource = audioSourceList [1];
			sfxAudioSource = audioSourceList [0];
		}
	}

	AudioClip FindAudioClip (AudioClipID audioClipID)
	{
		for (int i = 0; i < audioClipInfoList.Count; i++) {
			if (audioClipInfoList [i].audioClipID == audioClipID) {
				return audioClipInfoList [i].audioClip;
			}
		}

		Debug.LogError ("Cannot Find Audio Clip : " + audioClipID);

		return null;
	}

	//! BACKGROUND MUSIC (BGM)
	public void PlayBGM (AudioClipID audioClipID)
	{
		bgmAudioSource.clip = FindAudioClip (audioClipID);
		Debug.Log (audioClipID);
		bgmAudioSource.volume = bgmVolume;
		bgmAudioSource.loop = true;
		bgmAudioSource.Play ();
	}

	public void PauseBGM ()
	{
		if (bgmAudioSource.isPlaying) {
			bgmAudioSource.Pause ();
		}
	}

	public void StopBGM ()
	{
		if (bgmAudioSource.isPlaying) {
			bgmAudioSource.Stop ();
		}
	}


	//! SOUND EFFECTS (SFX)
	public void PlaySFX (AudioClipID audioClipID)
	{
		sfxAudioSource.PlayOneShot (FindAudioClip (audioClipID), sfxVolume);
	}

	public void PlayLoopingSFX (AudioClipID audioClipID)
	{
		AudioClip clipToPlay = FindAudioClip (audioClipID);

		for (int i = 0; i < sfxAudioSourceList.Count; i++) {
			if (sfxAudioSourceList [i].clip == clipToPlay) {
				if (sfxAudioSourceList [i].isPlaying) {
					return;
				}

				sfxAudioSourceList [i].volume = sfxVolume;
				sfxAudioSourceList [i].Play ();
				return;
			}
		}

		AudioSource newInstance = gameObject.AddComponent<AudioSource> ();
		newInstance.clip = clipToPlay;
		newInstance.volume = sfxVolume;
		newInstance.loop = true;
		newInstance.Play ();
		sfxAudioSourceList.Add (newInstance);
	}

	public void PauseLoopingSFX (AudioClipID audioClipID)
	{
		AudioClip clipToPause = FindAudioClip (audioClipID);

		for (int i = 0; i < sfxAudioSourceList.Count; i++) {
			if (sfxAudioSourceList [i].clip == clipToPause) {
				sfxAudioSourceList [i].Pause ();
				return;
			}
		}
	}

	public void StopLoopingSFX (AudioClipID audioClipID)
	{
		AudioClip clipToStop = FindAudioClip (audioClipID);

		for (int i = 0; i < sfxAudioSourceList.Count; i++) {
			if (sfxAudioSourceList [i].clip == clipToStop) {
				sfxAudioSourceList [i].Stop ();
				return;
			}
		}
	}

	public void ChangePitchLoopingSFX (AudioClipID audioClipID, float value)
	{
		AudioClip clipToStop = FindAudioClip (audioClipID);

		for (int i = 0; i < sfxAudioSourceList.Count; i++) {
			if (sfxAudioSourceList [i].clip == clipToStop) {
				sfxAudioSourceList [i].pitch = value;
				return;
			}
		}
	}

	public void SetBGMVolume (float value)
	{
		bgmVolume = value;
		bgmAudioSource.volume = bgmVolume;
	}

	public void SetSFXVolume (float value)
	{
		sfxVolume = value;
		sfxAudioSource.volume = sfxVolume;
	}

	public void SetBrightness (float value)
	{
		brightness = value;
		brightnessMask.color = new Color (0, 0, 0, 1 - brightness);
	}

	public void PlayButtonSound ()
	{
		//SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_UI_BUTTON);
	}
}