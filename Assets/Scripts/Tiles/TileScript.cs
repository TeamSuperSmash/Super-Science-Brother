using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MaterialType
{
	Glass = 0,
	Wood,
	Brick,
	Concrete,
	Metal,

	Total
};

public class TileScript : MonoBehaviour
{
	// Developer
	public SpriteRenderer rend;
	private BoxCollider2D coll;

	[Header("Stats")]
	public MaterialType type;
	public float mass = 150.0f;
	public bool isAlive;
	public bool hasItem;

	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<SpriteRenderer>();
		coll = GetComponent<BoxCollider2D>();

		//rend.sprite = TileManagerScript.instance.tileTypes[type.GetInt()];
		mass = type.GetMass();
		rend.sprite = type.GetSprite();
	}

	void Update()
	{
		if(mass <= type.GetPrevMaterial().GetMass())
		{
			type = type.GetPrevMaterial();
			rend.sprite = type.GetSprite();
		}

		if(mass >= type.GetNextMaterial().GetMass())
		{
			type = type.GetNextMaterial();
			rend.sprite = type.GetSprite();
		}
	}

	public void SetAlive(bool alive)
	{
		isAlive = alive;
		rend.enabled = alive;
		coll.isTrigger = !alive;
	}
}
