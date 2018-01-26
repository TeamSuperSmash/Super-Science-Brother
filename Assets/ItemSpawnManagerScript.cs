using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	RESGREN = 0,
	TOTAL,
};

[System.Serializable]
public class SpawnRange
{
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
};

public class ItemSpawnManagerScript : MonoBehaviour {

	public SpawnRange[] spawnPosRange;

	public GameObject itemPrefab;

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
						if(TileManagerScript.instance.tileList[j].isAlive && !TileManagerScript.instance.tileList[j].itemSpawned)
						{
							Vector2 myPos = TileManagerScript.instance.tileList[j].transform.position;

							myPos.y += 0.4f;

							GameObject newItem = Instantiate(itemPrefab, myPos, Quaternion.identity) as GameObject;

							ItemScript itemscript = newItem.GetComponent<ItemScript>();

							itemscript.type = (ItemType)Random.Range(0, (int)ItemType.TOTAL);
							itemscript.origTile = TileManagerScript.instance.tileList[j];
							itemscript.origTile.itemSpawned = true;

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
				float boundY = TileManagerScript.instance.tileList[i].GetComponent<SpriteRenderer>().bounds.size.y;

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
