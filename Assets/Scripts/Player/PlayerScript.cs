using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerControls
{
	public string horizontalAxis;
	public string jump;
	public string aimXAxis;
	public string aimYAxis;
	public string gunInc;
	public string gunDec;
	public string gunForce;
	public string useItem;
}

[System.Serializable]
public class PlayerRagdoll : PlayerScript
{
	public Transform body;
	public Transform gun;
	public Transform fireSpot;
}

public interface PlayerComponent
{
	void SetPlayer (PlayerScript player);
}

public class PlayerScript : MonoBehaviour
{
	// Designer settings
	private Rigidbody2D rb;

	[Header ("Controls")]
	public string ctrlPrefix;
	public PlayerControls controls;

	[Header ("Components")]
	public GroundCheck[] groundChecks;
	public PlayerGunScript gun;
	public FunctionalItem[] funcItems;
	public SpriteRenderer itemSprite;
    public PlayerRagdoll ragdoll;

    [Header ("Stats")]
	public MaterialType type;
	public float mass = 150.0f;
	public ItemType inventorySlot;
	public SpriteRenderer matRend;
	public Sprite[] matSprites;

	[Header ("Speed")]
	public float maxSpeed;
	public float jumpForce;
	public float fallMultiplier = 2f;
	public float quickJumpMultiplier = 1.5f;
	public bool isRight;

	[Header ("Physics")]
	public bool isGrounded = false;
	public Quaternion lastRot;

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
        ragdoll = GetComponent<PlayerRagdoll>();

		// GroundCheck
		groundChecks = GetComponentsInChildren<GroundCheck> ();

		gun = GetComponent<PlayerGunScript> ();
		if (gun != null)
			gun.SetPlayer (this);

		// FuncItems
		for (int i = 0; i < funcItems.Length; i++) {
			funcItems [i].SetPlayer (this);
		}
	}

	public void SetMaterial (Sprite sprite)
	{
		matRend.sprite = sprite;
	}

	void Start ()
	{
	}

	void Update ()
	{
		UseItem ();
		HeldItem ();

		Color temp = matRend.color;

		temp.a = (mass - (int)type * 50f) / 100f;

		matRend.color = temp; 

		if (mass <= type.GetPrevMaterial ().GetMass ()) {
			type = type.GetPrevMaterial ();
			SetMaterial (type.GetSprite ());
		}

		if (mass >= type.GetNextMaterial ().GetMass ()) {
			type = type.GetNextMaterial ();
			SetMaterial (type.GetSprite ());
		}
	}

	void LateUpdate ()
	{
		Movement ();
		Jump ();
		Aim ();
	}

	void Movement ()
	{
		if(Input.GetButton("Horizontal"))
		{
			Vector2 v = rb.velocity;

			v.x = maxSpeed * Input.GetAxis("Horizontal");

			rb.velocity = v;
		}
	}

	void Jump ()
	{
		isGrounded = false;
		for (int i = 0; i < groundChecks.Length; i++) {
			if (groundChecks [i].isGrounded) {
				isGrounded = true;
				break;
			}
		}

		if (isGrounded) 
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				rb.velocity += Vector2.up * jumpForce;

				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_JUMP1);
			}
		}

		// When player is falling.
		if (rb.velocity.y < 0) {
			rb.velocity += Vector2.down * fallMultiplier;
		} else if (rb.velocity.y > 0 && !Input.GetButton (ctrlPrefix + controls.jump)) {
			rb.velocity += Vector2.down * quickJumpMultiplier;
		}
	}

	void Aim ()
	{
		Vector2 diff = new Vector2 (Input.GetAxis (ctrlPrefix + controls.aimXAxis), Input.GetAxis (ctrlPrefix + controls.aimYAxis));
		if (diff.sqrMagnitude < 0.1f) {
			gun.transform.rotation = lastRot;
			return;
		}
		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
        gun.transform.rotation = lastRot = Quaternion.Euler (0.0f, 0.0f, rotZ - 90.0f);
	}

	void UseItem ()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			if (inventorySlot != ItemType.Nothing && inventorySlot != ItemType.Total) 
			{
				funcItems [inventorySlot.GetInt ()].UseItem ();
			}
		}
	}

	void HeldItem ()
	{
		Sprite item = inventorySlot.GetSprite ();

		if (item == null)
			itemSprite.color = Color.clear;
		else
			itemSprite.color = Color.white;
		
		itemSprite.sprite = item;
	}
}
