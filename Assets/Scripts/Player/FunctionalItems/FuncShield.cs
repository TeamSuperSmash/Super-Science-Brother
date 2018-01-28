using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncShield : FunctionalItem
{
	public GameObject shieldPrefab;

	public float maxTimeMassShield = 10f;
	public float timerMassShield = 10f;
	public bool massShieldAlive;

	bool playOnce = false;

	void Start ()
	{
		timerMassShield = maxTimeMassShield;
	}

	void Update ()
	{
		if (massShieldAlive) {
			if (!playOnce) {
				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_MASSSHIELD);
				playOnce = true;
			}

			timerMassShield -= Time.deltaTime;

			if (timerMassShield < 0) {
				timerMassShield = maxTimeMassShield;
				massShieldAlive = false;
				shieldPrefab.SetActive (false);
				player.inventorySlot = ItemType.Nothing;
				playOnce = false;
			}
		}
	}

	public override void UseItem ()
	{
		if (!massShieldAlive) {
			timerMassShield = maxTimeMassShield;
			massShieldAlive = true;
			shieldPrefab.SetActive (true);
		}
	}


}
