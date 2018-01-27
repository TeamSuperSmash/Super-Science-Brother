using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ragdoll {
    public Transform head;
    public Transform body;
}

public class PlayerScript : MonoBehaviour
{
    public Ragdoll ragdoll;

    public ItemType inventorySlot = ItemType.Nothing;

	public float maxSpeed = 5f;

	public float accelerationForce = 5f;
	public float jumpForce = 50f;

	public float fallMultiplier = 2f;
	public float quickJumpMultiplier = 1.5f;

	public bool isGrounded = false;
    public bool isRight = true;
	
	public float playerMass = 100.0f;

	GameObject body;

	Rigidbody2D rb2d;

	public LayerMask hitGroundLayer;

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
		Jump ();
	}

	void Movement ()
	{
		if (Input.GetButton ("Horizontal") && Input.GetAxis ("Horizontal") < 0f) {
			rb2d.velocity += Vector2.left * accelerationForce;
            isRight = false;
		}
		if (Input.GetButton ("Horizontal") && Input.GetAxis ("Horizontal") > 0f) {
			rb2d.velocity += Vector2.right * accelerationForce;
            isRight = true;
		}

		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}

	void Jump ()
	{
		if (isGrounded) {
			if (Input.GetButtonDown ("Jump")) {
				rb2d.velocity += Vector2.up * jumpForce;
			}
		}

		//When player is falling
		if (rb2d.velocity.y < 0) {
			rb2d.velocity += Vector2.down * fallMultiplier;
		} else if (rb2d.velocity.y > 0 && !Input.GetButton ("Jump")) {
			rb2d.velocity += Vector2.down * quickJumpMultiplier;
		}
	}
}
