using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncFatBoy : FunctionalItem
{
	public GameObject fatBoyPrefab;

	[Range(-16.0f, 16.0f)]
	public float fatboySpawnDistX;

	[Range(0.0f, 5.0f)]
	public float fatboySpawnDistY;

	public override void UseItem()
	{
		Instantiate(fatBoyPrefab, new Vector2(player.transform.position.x + fatboySpawnDistX, player.transform.position.y + fatboySpawnDistY), transform.rotation);

		player.inventorySlot = ItemType.Nothing;
	}
}
