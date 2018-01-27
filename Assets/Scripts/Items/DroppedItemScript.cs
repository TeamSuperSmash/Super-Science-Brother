using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Nothing = -1,

	BombInc = 0,
	BombDec,
	Restore,
	Shield,
	Bazooka,
	Fatboy,

	Total
};

public class DroppedItemScript : MonoBehaviour
{
	//Developer
	public SpriteRenderer rend;

	[Header("Item Stats")]
	public ItemType type;

	[Header("Entity Dependencies")]
	public TileScript parentTile;

	void Awake()
	{
		rend = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		rend.sprite = type.GetSprite();
	}

	void OnTriggerStay2D(Collider2D coll) 
	{
		if(coll.gameObject.tag == "Player")
		{
			PlayerScript player = coll.GetComponentInParent<PlayerScript>();

			if(!player) return;

			if(player.inventorySlot != ItemType.Nothing)
				return;
			
			player.inventorySlot = type;
			if(parentTile) parentTile.hasItem = false;

			Destroy(this.gameObject);

			//After Destroying we need to change the Animation for the ItemSlot.
		}
	}
}
