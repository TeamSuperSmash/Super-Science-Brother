using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManagerScript : MonoBehaviour
{
	public string startGameScene;
	public GameObject[] menuWindows;

	public void StartGame()
	{
		SceneManager.LoadScene(startGameScene);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void OpenMenu(int menu)
	{
		menuWindows[menu].SetActive(true);
	}

	public void CloseMenu(int menu)
	{
		menuWindows[menu].SetActive(false);
	}

	public void SetupBGM(GameObject slider)
	{
		slider.GetComponent<Slider>().value = SoundManagerScript.Instance.bgmVolume;
	}

	public void SetupSFX(GameObject slider)
	{
		slider.GetComponent<Slider>().value = SoundManagerScript.Instance.sfxVolume;
	}

	public void SetupBrightness(GameObject slider)
	{
		slider.GetComponent<Slider>().value = SoundManagerScript.Instance.brightness;
	}

	public void ChangeBGM(GameObject slider)
	{
		SoundManagerScript.Instance.SetBGMVolume(slider.GetComponent<Slider>().value);
	}

	public void ChangeSFX(GameObject slider)
	{
		SoundManagerScript.Instance.SetSFXVolume(slider.GetComponent<Slider>().value);
	}

	public void ChangeBrightness(GameObject slider)
	{
		SoundManagerScript.Instance.SetBrightness(slider.GetComponent<Slider>().value);
	}

    public void PlayButtonSound()
    {
        //SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_UI_BUTTON);
    }
}
