using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionalItem : MonoBehaviour, PlayerComponent
{
	protected PlayerScript player;

	public void SetPlayer(PlayerScript player)
	{
		this.player = player;
	}

	public ItemType type;

	public abstract void UseItem();
}
