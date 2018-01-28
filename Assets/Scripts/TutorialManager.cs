using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager mInstance;

    public bool tutorialOpened = false;

    public GameObject firstTutorialGO;
    public GameObject[] firstTutorial;

    public GameObject mainTutorialGO;
    public GameObject[] mainTutorial;

    int i = 0;

    void Awake()
    {
        mInstance = this;
    }

    public void NextFirstTutorial()
    {
        firstTutorial[i].SetActive(false);
        i++;
        firstTutorial[i].SetActive(true);
    }

    public void BackFirstTutorial()
    {
        firstTutorial[i].SetActive(false);
        i--;
        firstTutorial[i].SetActive(true);
    }

    public void CloseFirstTutorial()
    {
        tutorialOpened = false;
        firstTutorial[i].SetActive(false);
        firstTutorialGO.SetActive(false);
        i = 0;
    }

    public void NextMainTutorial()
    {
        mainTutorial[i].SetActive(false);
        i++;
        mainTutorial[i].SetActive(true);
    }

    public void BackMainTutorial()
    {
        mainTutorial[i].SetActive(false);
        i--;
        mainTutorial[i].SetActive(true);
    }

    public void OpenMainTutorial()
    {
        if (!tutorialOpened)
        {
            tutorialOpened = true;

            mainTutorialGO.SetActive(true);
            mainTutorial[i].SetActive(true);
        }
    }

    public void CloseMainTutorial()
    {
        tutorialOpened = false;
        mainTutorial[i].SetActive(false);
        mainTutorialGO.SetActive(false);
        i = 0;
    }

    public void PlayButtonSound()
    {
        //SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_UI_BUTTON);
    }
}
