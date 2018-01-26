using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public float maxSpeed = 5f;

	public float accelerationForce = 5f;
	public float jumpForce = 50f;

	public float fallMultiplier = 2f;
	public float quickJumpMultiplier = 1.5f;

	public bool isGrounded = false;

	public float playerMass = 50.0f;

	GameObject body;

	Rigidbody2D rb2d;

	void Awake ()
	{
		
	}

	void Start ()
	{
		body = GameObject.Find ("Body");
		rb2d = body.GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		Movement ();
	}

	void Movement ()
	{
		if (Input.GetButton ("Horizontal") && Input.GetAxis ("Horizontal") < 0f) {
			rb2d.velocity += Vector2.left * accelerationForce;
		}
		if (Input.GetButton ("Horizontal") && Input.GetAxis ("Horizontal") > 0f) {
			rb2d.velocity += Vector2.right * accelerationForce;
		}

		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}

		if (Input.GetButtonDown ("Jump") && isGrounded && Input.GetAxis ("Jump") > 0f) {
			rb2d.velocity += Vector2.up * jumpForce;
			isGrounded = false;
		}

		//When player is falling
		if (rb2d.velocity.y < 0 && !isGrounded) {
			rb2d.velocity += Vector2.down * fallMultiplier;
		} else if (rb2d.velocity.y > 0 && !Input.GetButton ("Jump")) {
			rb2d.velocity += Vector2.down * quickJumpMultiplier;
		}
	}
}
