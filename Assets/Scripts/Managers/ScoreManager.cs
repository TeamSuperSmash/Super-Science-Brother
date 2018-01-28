using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class PlayerScoreData
{
	public bool isAlive = true;
	public int score = 0;
	public GameObject go;
}

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;

	private void Awake ()
	{
		if (instance == null)
			instance = this;
	}

	public TMP_Text timeText;
	public TMP_Text ggText;

	public bool isRunning = true;
	public float time;
	public float maxTime = 90.0f;
	public float readyTime = 4.0f;
	public bool already = false;
	public List<PlayerScoreData> players;

	// Use this for initialization
	void Start ()
	{
		time = readyTime;
		already = false;
		isRunning = true;

		/*
		for(int i = 0; i < Input.GetJoystickNames().Length; i++)
		{
			players[i].isAlive = true;
		}

		for(int i = Input.GetJoystickNames().Length; i < 4; i++)
		{
			players[i].isAlive = false;
			players[i].go.SetActive(false);
		}
		*/
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isRunning)
			return;

		time -= Time.deltaTime;

		timeText.SetText ((Mathf.FloorToInt (time)).ToString ());


		int aliveCount = 0;

		for (int i = 0; i < players.Count; i++) {
			if (players [i].isAlive) {
				aliveCount++;
			}
		}

		if(aliveCount <= 1) time = 0.0f;

		if (time <= 0.0f) {

			if (!already) {
				already = true;
				time = maxTime;
				return;
			}
			/*
			int aliveCount = 0;

			for (int i = 0; i < players.Count; i++) {
				if (players [i].isAlive) {
					aliveCount++;
				}
			}

			Debug.Log (aliveCount.ToString ());
			*/

			int highScorer = -1;

			for (int i = 0; i < players.Count; i++) {
				if (players [i].isAlive) {
					switch (aliveCount) {
					case 1:
						players [i].score += 2;
						highScorer = i + 1;
						break;
					case 2:
						players [i].score += 1;
						break;
					}
				}

				players [i].isAlive = true;
			}

			isRunning = false;

			if (highScorer >= 0) {
				timeText.SetText (highScorer.ToString ());
				ggText.gameObject.SetActive (true);

				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_WIN);
			} else {
				timeText.SetText ("Draw");
				ggText.gameObject.SetActive (false);

				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_LOSE);
			}

			StartCoroutine (loadMainMenu ());


			already = false;
			time = readyTime;
		}
	}

	IEnumerator loadMainMenu ()
	{
		yield return new WaitForSeconds (10.0f);
		SceneManager.LoadScene ("MainMenuScene1");
	}
}
