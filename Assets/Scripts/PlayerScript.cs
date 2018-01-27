using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerRagdoll
{
	public Transform head;
	public Transform body;
	public Transform hand_L;
	public Transform hand_R;
	public Transform leg_L;
	public Transform leg_R;

	public Transform gun;
	public Transform fireSpot;
}

public interface PlayerComponent
{
	void SetPlayer (PlayerScript player);
}

public enum PlayerMatType
{
	GLASS = 0,
	WOOD,
	BRICK,
	CONCRETE,
	METAL,
	TOTAL,
};

public class PlayerScript : MonoBehaviour
{
	public PlayerRagdoll ragdoll;

	public ItemType inventorySlot = ItemType.Nothing;

	public float maxSpeed = 5f;

	public float accelerationForce = 5f;
	public float jumpForce = 50f;

	public float fallMultiplier = 2f;
	public float quickJumpMultiplier = 1.5f;

	public bool isGrounded = false;
	public bool isRight = true;
	
	public float playerMass = 100.0f;
	public float nextPlayerMass = 0.0f;
	public float prevPlayerMass = 0.0f;

	public float playerMassChangeThreshold = 50.0f;

	GameObject body;

	Rigidbody2D rb2d;

	public LayerMask hitGroundLayer;

	// Components to change player colour based on different material type.
	SpriteRenderer sRender;

	public PlayerMatType playerType;
	public List<Color> colourList = new List<Color> ();
	public int playerColourType;

	public float maxPlayerMass = 250.0f;
	public float minPlayerMass = 0.0f;

	void Awake ()
	{
		
	}

	void Start ()
	{
		body = GameObject.Find ("Body");
		rb2d = body.GetComponent<Rigidbody2D> ();
		sRender = GetComponentInChildren<SpriteRenderer> ();

		PlayerUpdateMatColour ();
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

	public void PlayerUpdateMatColour ()
	{
		int type_int = (int)playerType;

		sRender.color = colourList [(int)type_int];

		nextPlayerMass = (type_int + 1) * playerMassChangeThreshold;
		prevPlayerMass = (type_int) * playerMassChangeThreshold;
	}

	public void PlayerMassPositive (float massTransfer)
	{
		if (playerMass < maxPlayerMass) {
			playerMass += massTransfer;
		}

		if (playerMass > nextPlayerMass) {
			int tempType = (int)playerType + 1;
			playerType = (PlayerMatType)tempType;
			PlayerUpdateMatColour ();
		}
	}

	public void PlayerMassNegative (float massTransfer)
	{
		if (playerMass > minPlayerMass) {
			playerMass -= massTransfer;
		}

		if (playerMass < prevPlayerMass) {
			int tempType = (int)playerType - 1;
			playerType = (PlayerMatType)tempType;
			PlayerUpdateMatColour ();
		}
	}
}
