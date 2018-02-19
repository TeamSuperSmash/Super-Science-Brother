using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerComponent
{
	void SetPlayer (PlayerScript player);
}

public class PlayerScript : MonoBehaviour
{
	// Designer settings
	private Rigidbody2D rb;

	[Header ("Components")]
	public GroundCheck groundCheck;
	public PlayerGunScript gun;
	public FunctionalItem[] funcItems;

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

	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
		groundCheck = GetComponent<GroundCheck> ();
		gun = GetComponent<PlayerGunScript> ();

		// FuncItems
		for (int i = 0; i < funcItems.Length; i++)
        {
			funcItems [i].SetPlayer (this);
		}
	}

	public void SetMaterial (Sprite sprite)
	{
		matRend.sprite = sprite;
	}

	void Update ()
    {
        Movement();
        Jump();
        UseItem();
        UpdatePlayerMat();
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
		if (groundCheck.isGrounded) 
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
                groundCheck.isGrounded = false;
				rb.velocity += Vector2.up * jumpForce;

                SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_JUMP1);
			}
		}

		// When player is falling.
		if (rb.velocity.y < 0)
        {
			rb.velocity += Vector2.down * fallMultiplier;
		} else if (rb.velocity.y > 0 && !Input.GetButton ("Jump"))
        {
			rb.velocity += Vector2.down * quickJumpMultiplier;
		}
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

    void UpdatePlayerMat()
    {
        Color temp = matRend.color;

        temp.a = (mass - (int)type * 50f) / 100f;

        matRend.color = temp;

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
    }
}
