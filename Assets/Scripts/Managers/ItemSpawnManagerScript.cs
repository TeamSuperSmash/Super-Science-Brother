using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnRange
{
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
};

public class ItemSpawnManagerScript : MonoBehaviour
{
	public static ItemSpawnManagerScript instance;

	public SpawnRange[] spawnPosRange;
	public Sprite[] itemSprites;

	public GameObject itemPrefab;
	public PlayerScript player;

	void Awake()
	{
		if(instance == null) instance = this;
	}

    void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SpawnItem();
		}

		if(Input.GetKeyDown(KeyCode.Return))
		{
			RegenNeighTile();
		}
	}

	void SpawnItem()
	{
		for(int i = 0; i < spawnPosRange.Length; i++)
		{
			for(int j = 0; j < TileManagerScript.instance.tileList.Count; j++)
			{
				if(TileManagerScript.instance.tileList[j].transform.position.x >= spawnPosRange[i].minX 
					&& TileManagerScript.instance.tileList[j].transform.position.x <= spawnPosRange[i].maxX)
				{
					if(TileManagerScript.instance.tileList[j].transform.position.y >= spawnPosRange[i].minY 
						&& TileManagerScript.instance.tileList[j].transform.position.y <= spawnPosRange[i].maxY)
					{
						if(TileManagerScript.instance.tileList[j].isAlive && !TileManagerScript.instance.tileList[j].hasItem)
						{
							Vector2 myPos = TileManagerScript.instance.tileList[j].transform.position;

							myPos.y += 1f;

							GameObject newItem = Instantiate(itemPrefab, myPos, Quaternion.identity) as GameObject;

							DroppedItemScript itemscript = newItem.GetComponent<DroppedItemScript>();

							itemscript.rend = itemscript.GetComponent<SpriteRenderer>();

							itemscript.type = (ItemType)Random.Range(0, (int)ItemType.Total);
							itemscript.parentTile = TileManagerScript.instance.tileList[j];
							itemscript.rend.sprite = itemSprites[(int)itemscript.type];
							itemscript.parentTile.hasItem = true;

							return;
						}
					}
				}
			}
		}
	}

	void RegenNeighTile()
	{
		for(int i = 0; i < TileManagerScript.instance.tileList.Count; i++)
		{
			if(!TileManagerScript.instance.tileList[i].isAlive)
			{
				float boundX = TileManagerScript.instance.tileList[i].GetComponent<SpriteRenderer>().bounds.size.x;
				//float boundY = TileManagerScript.instance.tileList[i].GetComponent<SpriteRenderer>().bounds.size.y;

				for(int j = 0; j < TileManagerScript.instance.tileList.Count; j++)
				{
					if(TileManagerScript.instance.tileList[j].transform.position.x == TileManagerScript.instance.tileList[i].transform.position.x + boundX)
					{
						TileManagerScript.instance.tileList[j].SetAlive(true);

						break;
					}
				}

				for(int j = 0; j < TileManagerScript.instance.tileList.Count; j++)
				{
					if(TileManagerScript.instance.tileList[j].transform.position.x == TileManagerScript.instance.tileList[i].transform.position.x - boundX)
					{
						TileManagerScript.instance.tileList[j].SetAlive(true);

						break;
					}
				}
			}

		}
	}
}
