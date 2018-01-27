using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MaterialExtention
{
	public static MaterialData[] datas = Resources.LoadAll<MaterialData>("Materials");

	public static int GetInt(this MaterialType type)
	{
		return (int)type;
	}
	public static MaterialType ToMaterialType(this int i)
	{
		return (MaterialType)i;
	}

	public static MaterialType GetNextMaterial(this MaterialType type)
	{
		int i = type.GetInt() + 1;

		if(i >= MaterialType.Total.GetInt()) return type;
		return i.ToMaterialType();
	}
	public static MaterialType GetPrevMaterial(this MaterialType type)
	{
		int i = type.GetInt() - 1;

		if(i < 0) return type;
		return i.ToMaterialType();
	}

	public static float GetMass(this MaterialType type)
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

	public static Sprite GetSprite(this MaterialType type)
	{
		if(type == MaterialType.Total)
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
