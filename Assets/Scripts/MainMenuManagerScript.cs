using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManagerScript : MonoBehaviour
{
	public string startGameScene;
	public string tutorialGameScene;
	public GameObject[] menuWindows;
	public GameObject[] firstSelected;

	void Awake ()
	{
		SoundManagerScript.Instance.PlayBGM (AudioClipID.BGM_MAIN_MENU);
	}

	public void StartGame ()
	{
		SceneManager.LoadScene (startGameScene);
	}

	public void StartTutorial ()
	{
		SceneManager.LoadScene (tutorialGameScene);
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}

	public void OpenMenu (int menu)
	{
		menuWindows [menu].SetActive (true);
		EventSystem.current.SetSelectedGameObject (firstSelected [menu]);
	}

	public void CloseMenu (int menu)
	{
		menuWindows [menu].SetActive (false);
	}

	public void SetupBGM (GameObject slider)
	{
		slider.GetComponent<Slider> ().value = SoundManagerScript.Instance.bgmVolume;
	}

	public void SetupSFX (GameObject slider)
	{
		slider.GetComponent<Slider> ().value = SoundManagerScript.Instance.sfxVolume;
	}

	public void SetupBrightness (GameObject slider)
	{
		slider.GetComponent<Slider> ().value = SoundManagerScript.Instance.brightness;
	}

	public void ChangeBGM (GameObject slider)
	{
		SoundManagerScript.Instance.SetBGMVolume (slider.GetComponent<Slider> ().value);
	}

	public void ChangeSFX (GameObject slider)
	{
		SoundManagerScript.Instance.SetSFXVolume (slider.GetComponent<Slider> ().value);
	}

	public void ChangeBrightness (GameObject slider)
	{
		SoundManagerScript.Instance.SetBrightness (slider.GetComponent<Slider> ().value);
	}

	public void PlayButtonSound ()
	{
		//SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_UI_BUTTON);
	}
}
