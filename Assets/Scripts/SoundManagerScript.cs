using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum AudioClipID
{
	// Main Menu
	BGM_MAIN_MENU = 0,

	// Lose & Win
	BGM_LOSE = 1,
	BGM_WIN = 2,

	// Levels
	BGM_LEVEL1 = 3,
	BGM_LEVEL2 = 4,
	BGM_LEVEL3 = 5,
	BGM_LEVEL4 = 6,
	BGM_LEVEL5 = 7,

	// Creepy
	BGM_CREEPY = 8,

	// UI
	SFX_UI_BUTTON = 100,

	// Saferoom Door
	SFX_SR_OPENDOOR = 101,
	SFX_SR_CLOSEDOOR = 102,

	// Items
	SFX_IT_BLACKFMONEY = 103,
	SFX_IT_VACCINE = 104,

	// Contorl Panel
	SFX_CONTROLP_LACTIVATED = 105,

	// Player
	SFX_PL_WALKING = 108,
	SFX_PL_RECEIVEDDMG = 109,
	SFX_PL_JUMPING = 110,
	SFX_PL_LANDING = 111,

	// Monsters
	SFX_MS_RECEIVEDMG = 112,

	// Guns
	SFX_GUN_RELOAD = 113,
	SFX_GUN_SHOOTINGNORMAL = 114,
	SFX_GUN_SHOOTINGLASER = 115,
	SFX_GUN_CHANGETYPEBULLET = 116,
	SFX_GUN_SHOOTINGMPA = 117,
	SFX_GUN_SWITCHMPA = 118,

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

	public static SoundManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				SoundManagerScript temp = ManagerControllerScript.Instance.soundManager;

				if(temp == null)
				{
					temp = Instantiate(ManagerControllerScript.Instance.soundManagerPrefab, Vector3.zero, Quaternion.identity).GetComponent<SoundManagerScript>();
				}
				mInstance = temp;
				ManagerControllerScript.Instance.soundManager = mInstance;
				DontDestroyOnLoad(mInstance.gameObject);
			}
			return mInstance;
		}
	}
	public static bool CheckInstanceExist()
	{
		return mInstance;
	}
	#endregion Singleton

	public float bgmVolume = 1.0f;
	public float sfxVolume = 1.0f;
	public float brightness = 1.0f;


	public List<AudioClipInfo> audioClipInfoList = new List<AudioClipInfo>();

	public AudioSource bgmAudioSource;
	public AudioSource sfxAudioSource;
	public Image brightnessMask;

	public List<AudioSource> sfxAudioSourceList = new List<AudioSource>();
	public List<AudioSource> bgmAudioSourceList = new List<AudioSource>();

	// Preload before any Start() rins in other scripts
	void Awake () 
	{
		if(SoundManagerScript.CheckInstanceExist())
		{
			Destroy(this.gameObject);
		}

		AudioSource[] audioSourceList = this.GetComponentsInChildren<AudioSource>();

		if(audioSourceList[0].gameObject.name == "BGMAudioSource")
		{
			bgmAudioSource = audioSourceList[0];
			sfxAudioSource = audioSourceList[1];
		}
		else 
		{
			bgmAudioSource = audioSourceList[1];
			sfxAudioSource = audioSourceList[0];
		}
	}

	AudioClip FindAudioClip(AudioClipID audioClipID)
	{
		for(int i=0; i<audioClipInfoList.Count; i++)
		{
			if(audioClipInfoList[i].audioClipID == audioClipID)
			{
				return audioClipInfoList[i].audioClip;
			}
		}

		Debug.LogError("Cannot Find Audio Clip : " + audioClipID);

		return null;
	}

	//! BACKGROUND MUSIC (BGM)
	public void PlayBGM(AudioClipID audioClipID)
	{
		bgmAudioSource.clip = FindAudioClip(audioClipID);
		Debug.Log (audioClipID);
		bgmAudioSource.volume = bgmVolume;
		bgmAudioSource.loop = true;
		bgmAudioSource.Play();
	}

	public void PauseBGM()
	{
		if(bgmAudioSource.isPlaying)
		{
			bgmAudioSource.Pause();
		}
	}

	public void StopBGM()
	{
		if(bgmAudioSource.isPlaying)
		{
			bgmAudioSource.Stop();
		}
	}


	//! SOUND EFFECTS (SFX)
	public void PlaySFX(AudioClipID audioClipID)
	{
		sfxAudioSource.PlayOneShot(FindAudioClip(audioClipID), sfxVolume);
	}

	public void PlayLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToPlay = FindAudioClip(audioClipID);

		for(int i=0; i<sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToPlay)
			{
				if(sfxAudioSourceList[i].isPlaying)
				{
					return;
				}

				sfxAudioSourceList[i].volume = sfxVolume;
				sfxAudioSourceList[i].Play();
				return;
			}
		}

		AudioSource newInstance = gameObject.AddComponent<AudioSource>();
		newInstance.clip = clipToPlay;
		newInstance.volume = sfxVolume;
		newInstance.loop = true;
		newInstance.Play();
		sfxAudioSourceList.Add(newInstance);
	}

	public void PauseLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToPause = FindAudioClip(audioClipID);

		for(int i=0; i<sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToPause)
			{
				sfxAudioSourceList[i].Pause();
				return;
			}
		}
	}	

	public void StopLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);

		for(int i=0; i<sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToStop)
			{
				sfxAudioSourceList[i].Stop();
				return;
			}
		}
	}

	public void ChangePitchLoopingSFX(AudioClipID audioClipID, float value)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);

		for(int i=0; i<sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToStop)
			{
				sfxAudioSourceList[i].pitch = value;
				return;
			}
		}
	}

	public void SetBGMVolume(float value)
	{
		bgmVolume = value;
		bgmAudioSource.volume = bgmVolume;
	}

	public void SetSFXVolume(float value)
	{
		sfxVolume = value;
		sfxAudioSource.volume = sfxVolume;
	}

	public void SetBrightness(float value)
	{
		brightness = value;
		brightnessMask.color = new Color(0, 0, 0, 1 - brightness);
    }

    public void PlayButtonSound()
    {
        SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_UI_BUTTON);
    }
}