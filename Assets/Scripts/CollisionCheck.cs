using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
	PlayerScript player;

	public LayerMask hitGroundLayer;

	public float checkDistance = 0.5f;

	void Awake ()
	{
		player = FindObjectOfType<PlayerScript> ();
	}

	void Update ()
	{
		if (!IsGround ()) {
			player.isGrounded = false;
			return;
		} else {
			player.isGrounded = true;
		}
	}

	public bool IsGround ()
	{
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;
		float distance = checkDistance;

		Debug.DrawRay (position, direction, Color.green);
		RaycastHit2D hit = Physics2D.Raycast (position, direction, distance, hitGroundLayer);
		if (hit.collider != null) {
			return true;
		}
		return false;
	}
}
