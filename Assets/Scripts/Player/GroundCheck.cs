using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour//, PlayerComponent
{
	/*
	private PlayerScript player;

	public void SetPlayer(PlayerScript player)
	{
		this.player = player;
	}
	*/
	public LayerMask hitGroundLayer;
	public float checkDistance = 0.5f;
	public bool isGrounded = false;

	MaterialType playerMatType;
	MaterialType tileMatType;
	GameObject tileItself;

	public float timeToBreak = 0.0f;
	public float timeToBreakCounter = 0.0f;

	void Start ()
	{
		playerMatType = GetComponentInParent<PlayerScript> ().type;
	}

	void Update ()
	{
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;

		Debug.DrawRay (transform.position, Vector2.down, Color.green);
		RaycastHit2D hit = Physics2D.Raycast (position, direction, checkDistance, hitGroundLayer);

		if (hit)
			isGrounded = true;
		else
			isGrounded = false;

		if (hit.collider) {
			// Temp tile variable to check between player and tile.
			tileMatType = hit.transform.gameObject.GetComponent<TileScript> ().type;

			// Higher the number, heavier the player.
			if (playerMatType - tileMatType > 0) {
			
				Debug.Log ("Player heavier than the tile!");

				hit.transform.gameObject.SetActive (false);

			} else if (playerMatType - tileMatType < 1) {

				Debug.Log ("Player lighter than the tile!");
			}
		}
	}
}
