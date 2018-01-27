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

	void Update()
	{
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;

		Debug.DrawRay (transform.position, Vector2.down, Color.green);
		RaycastHit2D hit = Physics2D.Raycast (position, direction, checkDistance, hitGroundLayer);

		if (hit) isGrounded = true;
		else isGrounded = false;
	}
}
