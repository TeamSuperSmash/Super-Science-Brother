using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager mInstance;

    public bool tutorialOpened = false;
    bool oneFrame = false;

    public GameObject firstTutorialGO;
    public GameObject[] firstTutorial;

    public GameObject mainTutorialGO;
    public GameObject[] mainTutorial;

    int i = 0;

    void Awake()
    {
        mInstance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("J1AButton"))
        {
            SceneManager.LoadScene(1);
            //CloseFirstTutorial();
        }

        if (Input.GetAxis("J1XAxis") >= 1f)
        {
            if (tutorialOpened)
            {
                if (!oneFrame)
                {
                    if (i < firstTutorial.Length - 1)
                    {
                        NextFirstTutorial();
                        oneFrame = true;
                    }
                }

            }
            else
            {
                if (!oneFrame)
                {
                    NextMainTutorial();
                    oneFrame = true;
                }
            }

        }
        else if (Input.GetAxis("J1XAxis") <= -1f)
        {
            if (tutorialOpened)
            {
                if (!oneFrame)
                {
                    if (i > 0)
                    {
                        BackFirstTutorial();
                        oneFrame = true;
                    }
                }
            }
            else
            {
                if (!oneFrame)
                {
                    if (i > 0)
                    {
                        BackMainTutorial();
                        oneFrame = true;
                    }
                }
            }
        }
        else
        {
            if (oneFrame)
            {
                oneFrame = false;
            }
        }
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
