using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncShield : FunctionalItem
{
	public GameObject shieldPrefab;

	public float maxTimeMassShield = 10f;
	public float timerMassShield = 10f;
	public bool massShieldAlive;

	void Start()
	{
		timerMassShield = maxTimeMassShield;
	}

	void Update()
	{
		if(massShieldAlive)
		{
			timerMassShield -= Time.deltaTime;

			if(timerMassShield < 0)
			{
				timerMassShield = maxTimeMassShield;
				massShieldAlive = false;
				shieldPrefab.SetActive(false);
				player.inventorySlot = ItemType.Nothing;
			}
		}
	}

	public override void UseItem()
	{
		if(!massShieldAlive)
		{
			timerMassShield = maxTimeMassShield;
			massShieldAlive = true;
			shieldPrefab.SetActive(true);
		}
	}


}
