using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
    void SetPlayer(PlayerScript player);
}

public class PlayerScript : NetworkBehaviour
{
	//Developer
	private Rigidbody2D rb;

	[Header("Components")]
	public PlayerRagdoll ragdoll;
	public GroundCheck[] groundChecks;
	public PlayerGunScript gun;
	public FunctionalItem[] funcItems;
	public SpriteRenderer itemSprite;

	[Header("Stats")]
	public MaterialType type;
	public float mass = 150.0f;
	public ItemType inventorySlot;

	[Header("Speed")]
	public float maxSpeed;
	public float jumpForce;
	public float fallMultiplier = 2f;
	public float quickJumpMultiplier = 1.5f;
	public bool isRight;

	[Header("Physics")]
	public bool isGrounded = false;

	void Awake ()
	{
		rb = ragdoll.body.GetComponent<Rigidbody2D>();

		//GroundCheck
		groundChecks = GetComponentsInChildren<GroundCheck>();
		/*
		for(int i = 0; i < groundChecks.Length; i++)
		{
			groundChecks[i].SetPlayer(this);
		}
		*/

		gun = GetComponent<PlayerGunScript>();
		if(gun != null) gun.SetPlayer(this);

		//FuncItems
		for(int i = 0; i < funcItems.Length; i++)
		{
			funcItems[i].SetPlayer(this);
		}
	}

	public void SetMaterial(Sprite sprite)
	{
		ragdoll.head.GetComponent<SpriteRenderer>().sprite = sprite;
		ragdoll.body.GetComponent<SpriteRenderer>().sprite = sprite;
		ragdoll.hand_L.GetComponent<SpriteRenderer>().sprite = sprite;
		ragdoll.hand_R.GetComponent<SpriteRenderer>().sprite = sprite;
		ragdoll.leg_L.GetComponent<SpriteRenderer>().sprite = sprite;
		ragdoll.leg_R.GetComponent<SpriteRenderer>().sprite = sprite;
	}

	void Start()
	{
		SetMaterial(type.GetSprite());
	}

	void Update()
    {
        HeldItem();

        if (mass <= type.GetPrevMaterial().GetMass())
        {
            type = type.GetPrevMaterial();
            SetMaterial(type.GetSprite());
        }

        if (mass >= type.GetNextMaterial().GetMass())
        {
            type = type.GetNextMaterial();
            SetMaterial(type.GetSprite());
        }

        if (!isLocalPlayer)
        {
            return;
        }
		UseItem ();
	}

	void LateUpdate()
	{
        if (!isLocalPlayer)
        {
            return;
        }
        Movement ();
		Jump ();
		Aim ();
	}

	void Movement ()
	{
		if (Input.GetAxis ("Horizontal") != 0.0f)
		{
			Vector2 v = rb.velocity;
			v.x = maxSpeed * Input.GetAxis ("Horizontal");
			rb.velocity = v;
			isRight = Input.GetAxis ("Horizontal") > 0f;
		}
	}

	void Jump ()
	{
		isGrounded = false;
		for(int i = 0; i < groundChecks.Length; i++)
		{
			if(groundChecks[i].isGrounded)
			{
				isGrounded = true;
				break;
			}
		}

		if (isGrounded)
		{
			if (Input.GetButtonDown ("Jump"))
			{
				rb.velocity += Vector2.up * jumpForce;
			}
		}

		//When player is falling
		if (rb.velocity.y < 0)
		{
			rb.velocity += Vector2.down * fallMultiplier;
		}
		else if (rb.velocity.y > 0 && !Input.GetButton ("Jump"))
		{
			rb.velocity += Vector2.down * quickJumpMultiplier;
		}
	}

	void Aim ()
	{
		Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - ragdoll.hand_R.position;
		diff.Normalize ();

		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		ragdoll.hand_R.rotation = Quaternion.Euler (0.0f, 0.0f, rotZ - 90.0f);
	}

	void UseItem()
	{
		if (Input.GetButtonDown("ItemUse"))
		{
			if (inventorySlot != ItemType.Nothing && inventorySlot != ItemType.Total)
			{
				funcItems[inventorySlot.GetInt()].UseItem();
			}
		}
	}

	void HeldItem()
	{
		Sprite item = inventorySlot.GetSprite();

		if(item == null)
			itemSprite.color = Color.clear;
		else
			itemSprite.color = Color.white;
		
		itemSprite.sprite = item;
	}
}
