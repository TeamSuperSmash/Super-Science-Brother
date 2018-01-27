using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour, PlayerComponent
{
	private PlayerScript player;

	public void SetPlayer(PlayerScript player)
	{
		this.player = player;
	}

	[Header("Settings")]
	public LayerMask layerToHit;

	[Header("Stats")]
	public bool usingMassGun;
	public bool isDraining;
	[TooltipAttribute("How much mass is changed per hit rate.")]
	public float massTransferRate = 10.0f;

	[Header("Force Gun Stats")]
	public bool isCooldownForceGun = false;
	public float forceGunFactor = 0.0f;
	public float forceGunCooldown = 2.0f;
	public float forceGunTimer = 0.0f;

	[Header("Prefabs")]
	public GameObject forceBallPrefab;
	
	// Update is called once per frame
	void Update ()
	{
		CheckMassGun ();
		CheckForceGun();
	}

	void CheckForceGun()
	{
		if (!isCooldownForceGun)
		{
			if (Input.GetKeyDown (KeyCode.F))
			{
				forceGunTimer = forceGunCooldown;
				isCooldownForceGun = true;

				ForceShoot ();
			}
		}
		else
		{
			forceGunTimer -= Time.deltaTime;
			if (forceGunTimer <= 0.0f)
			{
				forceGunTimer = forceGunCooldown;
				isCooldownForceGun = false;
			}
		}
	}

	void CheckMassGun ()
	{
		if (Input.GetButton ("Fire1"))
		{
			usingMassGun = true;
			isDraining = false;
		}
		else if (Input.GetButton ("Fire2"))
		{
			usingMassGun = true;
			isDraining = true;
		}
		else
		{
			usingMassGun = false;
		}

		if(usingMassGun)
		{
			MassShoot();
		}
	}

	void MassShoot()
	{
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (player.ragdoll.fireSpot.position.x, player.ragdoll.fireSpot.position.y);

		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, (mousePosition - firePointPosition) * 1000.0f, 100.0f, layerToHit);

		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 1000.0f, (isDraining ? Color.green : Color.yellow));
		Debug.Log ("Player shoot the lazer!");

		if (hit)
		{
			Debug.DrawLine (firePointPosition, hit.point, Color.red);

			if (hit.collider.CompareTag ("Player"))
			{
				PlayerScript targetPlayer = hit.collider.gameObject.GetComponentInParent<PlayerScript>();

				if(isDraining)
				{
					if (targetPlayer.mass < PlayerSettings.maxMass)
					{
						targetPlayer.mass += massTransferRate * Time.deltaTime;
					}
				}
				else
				{
					if (targetPlayer.mass > PlayerSettings.minMass)
					{
						targetPlayer.mass -= massTransferRate * Time.deltaTime;
					}
				}
			}
		}
	}

	void ForceShoot ()
	{
		Instantiate (forceBallPrefab, player.ragdoll.fireSpot.position, Quaternion.identity);
	}
}
