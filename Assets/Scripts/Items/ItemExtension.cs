using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemExtention
{
	public static ItemData[] datas = Resources.LoadAll<ItemData>("Items");

	public static int GetInt(this ItemType type)
	{
		return (int)type;
	}
	public static ItemType ToItemType(this int i)
	{
		return (ItemType)i;
	}

	public static Sprite GetSprite(this ItemType type)
	{
		if(type == ItemType.Nothing || type == ItemType.Total)
			return null;
		
		for(int i = 0; i < datas.Length; i++)
		{
			if(datas[i].type == type)
			{
				return datas[i].sprite;
			}
		}

		return null;
	}
}
