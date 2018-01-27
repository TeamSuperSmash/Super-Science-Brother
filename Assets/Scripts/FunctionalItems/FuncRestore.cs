using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncRestore : FunctionalItem
{
	public override void UseItem()
	{
		//Instantly checks nearby missing tiles and regenerates them

		player.inventorySlot = ItemType.Nothing;
	}
}
