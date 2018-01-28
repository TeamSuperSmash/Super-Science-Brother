using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncRestore : FunctionalItem
{
	public float radius;

	public override void UseItem ()
	{
		//Instantly checks nearby missing tiles and regenerates them
		Collider2D[] tileRestoreList = Physics2D.OverlapCircleAll (player.ragdoll.body.position, radius);

		for (int i = 0; i < tileRestoreList.Length; i++) {
			if (tileRestoreList [i].CompareTag ("Tile")) {
				TileScript tilescript = tileRestoreList [i].GetComponent<TileScript> ();

				if (tilescript != null) {
					if (!tilescript.isAlive) {
						tilescript.SetAlive (true);

						SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_RESTORETILES);
					}
				}
			}
		}

		player.inventorySlot = ItemType.Nothing;
	}
}
