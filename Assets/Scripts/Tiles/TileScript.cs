using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileType
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
	private SpriteRenderer rend;
	private BoxCollider2D coll;

	[Header("Stats")]
	public TileType type;
	public float mass;
	public bool isAlive;
	public bool hasItem;

	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<SpriteRenderer>();
		coll = GetComponent<BoxCollider2D>();

		//rend.sprite = TileManagerScript.instance.tileTypes[type.GetInt()];
		mass = type.GetMass();
	}

	void OnMouseOver()
	{
		if(Input.GetButton("Fire1"))
		{
			mass += Time.deltaTime * TileManagerScript.instance.matChangeSpeed;

			if(mass >= type.GetNextTile().GetMass())
			{
				type = type.GetNextTile();
				rend.sprite = TileManagerScript.instance.tileTypes[type.GetInt()];
			}
		}
		else if(Input.GetButton("Fire2"))
		{
			mass -= Time.deltaTime * TileManagerScript.instance.matChangeSpeed;

			if(mass <= type.GetPrevTile().GetMass())
			{
				type = type.GetPrevTile();
				rend.sprite = TileManagerScript.instance.tileTypes[type.GetInt()];
			}
		}
	}

	public void SetAlive(bool alive)
	{
		isAlive = alive;
		rend.enabled = alive;
		coll.isTrigger = alive;

	}
}
