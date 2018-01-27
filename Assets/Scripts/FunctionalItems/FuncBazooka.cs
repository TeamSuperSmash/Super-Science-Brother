using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncBazooka : FunctionalItem
{
	public float countdown;
	public float buffTime = 5;

	public bool usingBazooka;

	void Start()
	{
		countdown = buffTime;
	}

	void Update()
	{
		if(usingBazooka)
		{
			countdown -= Time.deltaTime;
			if(countdown <= 0.0f)
			{
				usingBazooka = false;
				countdown = buffTime;
				player.gun.forceGunFactor = 0;

				player.inventorySlot = ItemType.Nothing;
			}
		}
	}

	public override void UseItem()
	{
		player.inventorySlot = ItemType.Nothing;

		countdown = buffTime;
		player.gun.forceGunFactor = 350;
		usingBazooka = true;

		player.inventorySlot = ItemType.Nothing;
	}
}
