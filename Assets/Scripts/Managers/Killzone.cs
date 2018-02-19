using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Player")
		{
			GameObject go = coll.GetComponentInParent<PlayerScript>().gameObject;
			if(go.name == "Player (1)")
			{
				ScoreManager.instance.players[0].isAlive = false;
			}
			if(go.name == "Player (2)")
			{
				ScoreManager.instance.players[1].isAlive = false;
			}
			if(go.name == "Player (3)")
			{
				ScoreManager.instance.players[2].isAlive = false;
			}
			if(go.name == "Player (4)")
			{
				ScoreManager.instance.players[3].isAlive = false;
			}

			Destroy(go);
		}
	}
}
