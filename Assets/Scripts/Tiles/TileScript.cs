using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum MaterialType
{
	Glass = 0,
	Wood,
	Brick,
	Concrete,
	Metal,

	Total,
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

	//UI Bar
	public Image[] uiBar;

	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<SpriteRenderer>();
		coll = GetComponent<BoxCollider2D>();

		//rend.sprite = TileManagerScript.instance.tileTypes[type.GetInt()];
		mass = type.GetMass() + 25f;
		rend.sprite = type.GetSprite();

		//Not efficient. Fix it later
		if(uiBar.Length > 0)
		{
			for(int i = 0; i < (int)MaterialType.Total; i++)
			{
				uiBar[i].fillAmount = 0f;
			}
		}

		if(uiBar.Length > 0)
		{
			uiBar[2].fillAmount = (mass - 2 * 50f) / 100f;
		}
	}

	void Update()
	{

		if(uiBar.Length > 0)
		{
			if(mass / 50f > 4f)
			{
				uiBar[4].fillAmount = (mass - 4 * 50f) / 100f;
			}
			else if(mass / 50f > 3f)
			{
				uiBar[3].fillAmount = (mass - 3 * 50f) / 100f;
			}
			else if(mass / 50f > 2f)
			{
				uiBar[2].fillAmount = (mass - 2 * 50f) / 100f;
			}
			else if(mass / 50f > 1f)
			{
				uiBar[1].fillAmount = (mass - 1 * 50f) / 100f;
			}
			else
			{
				uiBar[0].fillAmount = mass / 100f;
			}
		}

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
