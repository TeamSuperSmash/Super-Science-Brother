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
	public float cooldown = 0f;

	//UI Bar
	public Image[] uiBar;

	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<SpriteRenderer>();
		coll = GetComponent<BoxCollider2D>();
        
		mass = type.GetMass();
		rend.sprite = type.GetSprite();

		int a = (int)(mass / 50f);

		//Not efficient. Fix it later
//		if(uiBar.Length > 0)
//		{
//			for(int i = 0; i < (int)MaterialType.Total; i++)
//			{
//				uiBar[i].fillAmount = 0f;
//			}
//		}

		for(int i = a - 1; i >= 0; i--)
		{
			uiBar[i].fillAmount = 1f;
		}

		for(int i = a; i < uiBar.Length; i++)
		{
			if(i == a)
			{
				uiBar[i].fillAmount = (mass - i * 50f) / 50f;
			}
			else
			{
				uiBar[i].fillAmount = 0f;
			}
		}

//		if(uiBar.Length > 0)
//		{
//			if(mass / 50f > 4f)
//			{
//				uiBar[4].fillAmount = (mass - 4 * 50f) / 50f;
//			}
//			else if(mass / 50f > 3f)
//			{
//				uiBar[3].fillAmount = (mass - 3 * 50f) / 50f;
//			}
//			else if(mass / 50f > 2f)
//			{
//				uiBar[2].fillAmount = (mass - 2 * 50f) / 50f;
//			}
//			else if(mass / 50f > 1f)
//			{
//				uiBar[1].fillAmount = (mass - 1 * 50f) / 50f;
//			}
//			else if(mass / 50f > 0f)
//			{
//				uiBar[0].fillAmount = mass / 50f;
//			}
//		}
	}

	void Update()
	{
		
		if(uiBar.Length > 0)
		{
			if(mass / 50f > 4f)
			{
				uiBar[4].fillAmount = (mass - 4 * 50f) / 50f;
			}
			else if(mass / 50f > 3f)
			{
				uiBar[3].fillAmount = (mass - 3 * 50f) / 50f;
			}
			else if(mass / 50f > 2f)
			{
				uiBar[2].fillAmount = (mass - 2 * 50f) / 50f;
			}
			else if(mass / 50f >= 1f)
			{
				uiBar[1].fillAmount = (mass - 1 * 50f) / 50f;
			}
			else if(mass / 50f > 0f)
			{
				uiBar[0].fillAmount = mass / 50f;
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

		//Mass resets after some time without laser hit
		if(cooldown > 0f)
		{
			cooldown -= Time.deltaTime;
		}
		else if(cooldown > -1f)
		{
			float rfr = (float)type * 50f;

			if(mass == rfr)
			{
				cooldown = 1f;
				return;
			}
			else if(mass < rfr)
			{
				mass += Time.deltaTime * 2f;
			}
			else if(mass > rfr)
			{
				mass -= Time.deltaTime * 2f;
			}
		}
	}

	public void SetAlive(bool alive)
	{
		isAlive = alive;
		rend.enabled = alive;
		coll.isTrigger = !alive;
	}
}
