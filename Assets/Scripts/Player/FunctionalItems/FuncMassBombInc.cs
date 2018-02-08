﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncMassBombInc : FunctionalItem
{
	public GameObject massBombPrefab;

	[Range(0.0f, 1.0f)]
	public float itemSpawnDist;

	public override void UseItem()
	{
		GameObject go = Instantiate(massBombPrefab, new Vector2(player.transform.position.x + itemSpawnDist, player.transform.position.y), transform.rotation);
		go.GetComponent<MassBombProjectile>().isBombIncrease = true;

		player.inventorySlot = ItemType.Nothing;
	}
}
