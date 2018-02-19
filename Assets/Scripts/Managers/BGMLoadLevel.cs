using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMLoadLevel : MonoBehaviour
{
	public AudioClipID audioClipBGM;

	void Start ()
	{
		SoundManagerScript.Instance.PlayBGM (audioClipBGM);
	}
}
