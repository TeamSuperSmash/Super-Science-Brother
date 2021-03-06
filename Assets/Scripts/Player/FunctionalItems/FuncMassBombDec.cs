﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncMassBombDec : FunctionalItem
{
	public GameObject massBombPrefab;

	[Range(0.0f, 1.0f)]
	public float itemSpawnDist;

	public override void UseItem()
	{
		GameObject go = Instantiate(massBombPrefab, new Vector2(player.ragdoll.body.position.x + itemSpawnDist, player.ragdoll.body.position.y), transform.rotation);
		go.GetComponent<MassBombProjectile>().isBombIncrease = false;

		player.inventorySlot = ItemType.Nothing;
	}
}
