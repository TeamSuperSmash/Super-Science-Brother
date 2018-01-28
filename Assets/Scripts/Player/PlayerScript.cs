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

public class PlayerScript : MonoBehaviour
{
	//Developer
	private Rigidbody2D rb;

	[Header ("Controls")]
	public string ctrlPrefix;
	public PlayerControls controls;

	[Header ("Components")]
	public PlayerRagdoll ragdoll;
	public GroundCheck[] groundChecks;
	public PlayerGunScript gun;
	public FunctionalItem[] funcItems;
	public SpriteRenderer itemSprite;

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
		rb = ragdoll.body.GetComponent<Rigidbody2D> ();

		//GroundCheck
		groundChecks = GetComponentsInChildren<GroundCheck> ();
		/*
		for(int i = 0; i < groundChecks.Length; i++)
		{
			groundChecks[i].SetPlayer(this);
		}
		*/

		gun = GetComponent<PlayerGunScript> ();
		if (gun != null)
			gun.SetPlayer (this);

		//FuncItems
		for (int i = 0; i < funcItems.Length; i++) {
			funcItems [i].SetPlayer (this);
		}
	}

	public void SetMaterial (Sprite sprite)
	{
		matRend.sprite = sprite;
//		ragdoll.head.GetComponent<SpriteRenderer>().sprite = sprite;
//		ragdoll.body.GetComponent<SpriteRenderer>().sprite = sprite;
//		ragdoll.hand_L.GetComponent<SpriteRenderer>().sprite = sprite;
//		ragdoll.hand_R.GetComponent<SpriteRenderer>().sprite = sprite;
//		ragdoll.leg_L.GetComponent<SpriteRenderer>().sprite = sprite;
//		ragdoll.leg_R.GetComponent<SpriteRenderer>().sprite = sprite;
	}

	void Start ()
	{
		//SetMaterial(type.GetSprite());
		lastRot = ragdoll.hand_R.rotation;
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
		if (Input.GetAxis (ctrlPrefix + controls.horizontalAxis) != 0.0f) {
			Vector2 v = rb.velocity;
			v.x = maxSpeed * Input.GetAxis (ctrlPrefix + controls.horizontalAxis);
			rb.velocity = v;
			isRight = Input.GetAxis (ctrlPrefix + controls.horizontalAxis) > 0f;
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

		if (isGrounded) {
			if (Input.GetButtonDown (ctrlPrefix + controls.jump) || Input.GetMouseButton (0)) {
				rb.velocity += Vector2.up * jumpForce;

				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_JUMP1);
			}
		}

		//When player is falling
		if (rb.velocity.y < 0) {
			rb.velocity += Vector2.down * fallMultiplier;
		} else if (rb.velocity.y > 0 && !Input.GetButton (ctrlPrefix + controls.jump)) {
			rb.velocity += Vector2.down * quickJumpMultiplier;
		}
	}

	void Aim ()
	{
		Vector2 diff = new Vector2 (Input.GetAxis (ctrlPrefix + controls.aimXAxis), Input.GetAxis (ctrlPrefix + controls.aimYAxis));
		//Vector2 diff = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (diff.sqrMagnitude < 0.1f) {
			ragdoll.hand_R.rotation = lastRot;
			return;
		}
		//Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - ragdoll.hand_R.position;
		//diff.Normalize ();

		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		ragdoll.hand_R.rotation = lastRot = Quaternion.Euler (0.0f, 0.0f, rotZ - 90.0f);
	}

	void UseItem ()
	{
		if (Input.GetButtonDown (ctrlPrefix + controls.useItem)) {
			if (inventorySlot != ItemType.Nothing && inventorySlot != ItemType.Total) {
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
