using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material Data", menuName = "Scriptable Objects/Material Data", order = 1)]
public class MaterialData : ScriptableObject
{
	public MaterialType type;

	public new string name
	{
		get
		{
			return type.ToString();
		}
	}

	public float massValue;
	public Sprite sprite;
}
