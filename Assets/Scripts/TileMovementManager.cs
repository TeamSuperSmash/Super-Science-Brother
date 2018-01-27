using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TileMovementGroup
{
	public GameObject[] movingTiles;
};

public class TileMovementManager : MonoBehaviour {

	public Vector2[] movementWaypoints;
	public GameObject[] tileGroups;

	public float duration;
	public float timer;
	float moveTime = 300f;

	void Start()
	{
		timer = duration / 30;
	}

	void Update()
	{
		timer -= Time.deltaTime;

		if(timer <= 0f)
		{
			MoveTiles();

			timer = duration;
		}
	}

	IEnumerator MoveTile(GameObject obj, Vector2 vect)
	{
		float t = 0;
		float rate = 1f / moveTime;

		while(t < 1)
		{
			t += Time.deltaTime * rate;
			obj.transform.position = Vector3.Lerp(obj.transform.position, vect, t);
			yield return null;
		}
	}

	void MoveTiles()
	{
		List<GameObject> templist = new List<GameObject>();

		for(int i = 0; i < tileGroups.Length; i++)
		{
			templist.Add(tileGroups[i]);
		}

		for(int i = 0; i < templist.Count; i++)
		{
			if(i > 0)
			{
				tileGroups[i] = templist[i-1];
			}
			else
			{
				tileGroups[i] = templist[templist.Count - 1];
			}

		}

		for(int i = 0; i < tileGroups.Length; i++)
		{
			StartCoroutine(MoveTile(tileGroups[i], movementWaypoints[i]));

//			float t = 0;
//			float rate = 1f / moveTime;
//
//			while(t < 1)
//			{
//				t += Time.deltaTime * rate;
//				tileGroups[i].transform.position = Vector3.Lerp(tileGroups[i].transform.position, movementWaypoints[i], t);
//			}

//			for(int j = 0; j < tileGroups[i].movingTiles.Length; j++)
//			{

//			}
		}
	}

}
