using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerScript : MonoBehaviour {

	public static TileManagerScript instance;

	//Temp mat boost variable for player
	public float matChangeSpeed = 5f;
	public float matUpdateValue = 50f;

	//Temp variable
	//public bool curGunMode = false;

	//Temporary tile array. Will be replaced by array of sprites
	public Sprite[] tileTypes;
	public GameObject tilePrefab;

	public List<TileScript> tileList = new List<TileScript>();

	void Awake()
	{
		if(instance == null) instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

		for(int i = 0; i < tiles.Length; i++)
		{
			tileList.Add(tiles[i].GetComponent<TileScript>());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
