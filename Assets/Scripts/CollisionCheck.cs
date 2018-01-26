using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
	PlayerScript player;

	void Awake ()
	{
		player = FindObjectOfType<PlayerScript> ();
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Ground" && this.gameObject.tag == "Player") {
			player.isGrounded = true;
		}
	}
}
