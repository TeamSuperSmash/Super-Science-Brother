using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileExtention
{
	public static TileData[] datas = Resources.LoadAll<TileData>("Tiles");

	public static int GetInt(this TileType type)
	{
		return (int)type;
	}
	public static TileType ToTileType(this int i)
	{
		return (TileType)i;
	}

	public static TileType GetNextTile(this TileType type)
	{
		int i = type.GetInt() + 1;

		if(i >= TileType.Total.GetInt()) return type;
		return i.ToTileType();
	}
	public static TileType GetPrevTile(this TileType type)
	{
		int i = type.GetInt() - 1;

		if(i < 0) return type;
		return i.ToTileType();
	}

	public static float GetMass(this TileType type)
	{
		for(int i = 0; i < datas.Length; i++)
		{
			if(datas[i].type == type)
			{
				return datas[i].massValue;
			}
		}

		return 0.0f;
	}
}
