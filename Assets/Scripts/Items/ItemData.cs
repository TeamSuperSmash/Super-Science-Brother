using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Objects/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
	public ItemType type;

	public new string name
	{
		get
		{
			return type.ToString();
		}
	}

	public Sprite sprite;
}
