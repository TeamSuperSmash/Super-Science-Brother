using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Scriptable Objects/Tile Data", order = 1)]
public class TileData : ScriptableObject
{
	public TileType type;

	public new string name
	{
		get
		{
			return type.ToString();
		}
	}

	public float massValue;
}
