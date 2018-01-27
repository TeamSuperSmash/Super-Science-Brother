using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	public ItemType type;
	public TileScript origTile;

	public SpriteRenderer rend;

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if(coll.tag == "Player")
		{
			if(ItemSpawnManagerScript.instance.player.inventorySlot == ItemType.Nothing) 
			{
				ItemSpawnManagerScript.instance.player.inventorySlot = type;
			}

			origTile.itemSpawned = false;

			Destroy(this.gameObject);

			//After Destroying we need to change the Animation for the ItemSlot.
		}
	}
}
