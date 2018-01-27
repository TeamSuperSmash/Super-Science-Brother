using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileType
{
	GLASS = 0,
	WOOD,
	BRICK,
	CONCRETE,
	METAL,
	TOTAL,
};

public class TileScript : MonoBehaviour
{

	public bool isAlive;
	public bool itemSpawned;

	//Discuss with Eugene
	public bool matCanChange = true;

	SpriteRenderer rend;
	BoxCollider2D myColl;

	public TileType type;
	public int fragValue;

	public float curMatValue;
	float nextMatValue;
	float prevMatValue;

	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<SpriteRenderer> ();
		myColl = GetComponent<BoxCollider2D> ();

		UpdateMat ();

		curMatValue = (int)type * TileManagerScript.instance.matUpdateValue;
	}
	
	// Update is called once per frame
	void Update ()
	{

		//Physics2D.OverlapBoxAll(transform.position, 1f, 0f);

	}

	public void ChangeTileMatPositive ()
	{
		if (matCanChange) {
			curMatValue += Time.deltaTime * TileManagerScript.instance.matChangeSpeed;

			if (curMatValue >= nextMatValue) {
				int tempType = (int)type + 1;
				type = (TileType)tempType;
				UpdateMat ();
			}
		}
	}

	public void ChangeTileMatNegative ()
	{
		if (matCanChange) {
			curMatValue -= Time.deltaTime * TileManagerScript.instance.matChangeSpeed;

			if (curMatValue <= prevMatValue) {
				int tempType = (int)type - 1;
				type = (TileType)tempType;
				UpdateMat ();
			}
		}
	}

	public void UpdateMat ()
	{
		int type_int = (int)type;

		rend.sprite = TileManagerScript.instance.tileTypes [type_int];

		if (type_int >= (int)TileType.METAL) {
			matCanChange = false;
		} else if (type_int == 0) {
			matCanChange = false;
		}

		nextMatValue = (type_int + 1) * TileManagerScript.instance.matUpdateValue;
		prevMatValue = (type_int) * TileManagerScript.instance.matUpdateValue;

	}

	void OnMouseOver ()
	{
		if (Input.GetButton ("Fire1")) {
			if (matCanChange) {
				curMatValue += Time.deltaTime * TileManagerScript.instance.matChangeSpeed;

				if (curMatValue >= nextMatValue) {
					int tempType = (int)type + 1;
					type = (TileType)tempType;
					UpdateMat ();
				}
			}

		} else if (Input.GetButton ("Fire2")) {
			if (matCanChange) {
				curMatValue -= Time.deltaTime * TileManagerScript.instance.matChangeSpeed;

				if (curMatValue <= prevMatValue) {
					int tempType = (int)type - 1;
					type = (TileType)tempType;
					UpdateMat ();
				}
			}
		}
	}


	public void SetAlive (bool alive)
	{
		isAlive = alive;
		rend.enabled = alive;
		myColl.isTrigger = alive;

	}
}
