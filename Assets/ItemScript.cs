using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemScript : MonoBehaviour {

	public ItemType type;
	public TileScript origTile;

	public SpriteRenderer rend;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

//			if(ItemSpawnManagerScript.instance.player.inventorySlot == ItemType.Nothing) 
//			{
//				switch (coll.transform.tag) 
//				{
//				case "Bomb":
//					ItemSpawnManagerScript.instance.player.inventorySlot = ItemType.Bomb;
//					break;
//				case "Restore":
//					ItemSpawnManagerScript.instance.player.inventorySlot = ItemType.Restore;
//					break;
//				case "Shield":
//					ItemSpawnManagerScript.instance.player.inventorySlot = ItemType.Shield;
//					break;
//				case "Bazooka":
//					ItemSpawnManagerScript.instance.player.inventorySlot = ItemType.Bazooka;
//					break;
//				case "Fatboy":
//					ItemSpawnManagerScript.instance.player.inventorySlot = ItemType.Fatboy;
//					break;
//				}
//				Destroy(coll.gameObject);
				//After Destroying we need to change the Animation for the ItemSlot.
		}
	}
}
