using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour, PlayerComponent
{
	private PlayerScript player;

	public void SetPlayer (PlayerScript player)
	{
		this.player = player;
	}

	[Header ("Settings")]
	public LayerMask layerToHit;

	[Header ("Stats")]
	public bool usingMassGun;
	public bool isDraining;
	[TooltipAttribute ("How much mass is changed per hit rate.")]
	public float massTransferRate = 10.0f;

	[Header ("Force Gun Stats")]
	public bool isCooldownForceGun = false;
	public float forceGunFactor = 0.0f;
	public float forceGunCooldown = 2.0f;
	public float forceGunTimer = 0.0f;

	[Header ("Prefabs")]
	public GameObject forceBallPrefab;

	[Header ("Counter For Sounds")]
	float sfxTimer0 = 1.0f;
	float sfxTimer1 = 1.0f;
	
	// Update is called once per frame
	void Update ()
	{
		CheckMassGun ();
		CheckForceGun ();
	}

	void CheckMassGun ()
	{
		if (Input.GetMouseButton (0)) {
			usingMassGun = true;
			isDraining = false;

			sfxTimer1 += Time.deltaTime;

			if (sfxTimer1 > sfxTimer0) {
				sfxTimer1 = 0.0f;
				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_MASSGUN1);
			}

		} else if (Input.GetMouseButton(1)) {
			usingMassGun = true;
			isDraining = true;

			sfxTimer1 += Time.deltaTime;

			if (sfxTimer1 > sfxTimer0) {
				sfxTimer1 = 0.0f;
				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_MASSGUN2);
			}

		} else {
			usingMassGun = false;
		}

		if (usingMassGun) {
			MassShoot ();
		}
	}

	void CheckForceGun ()
	{
		if (!isCooldownForceGun) {
			if (Input.GetKeyDown (KeyCode.F)) {
				forceGunTimer = forceGunCooldown;
				isCooldownForceGun = true;

				ForceShoot ();

				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_FORCEPUSH);
			}
		} else {
			forceGunTimer -= Time.deltaTime;
			if (forceGunTimer <= 0.0f) {
				forceGunTimer = forceGunCooldown;
				isCooldownForceGun = false;
			}
		}
	}

	void MassShoot ()
	{
		Vector2 diff = new Vector2 (Input.GetAxis (player.ctrlPrefix + player.controls.aimXAxis), Input.GetAxis (player.ctrlPrefix + player.controls.aimYAxis));
		//Vector2 diff = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (diff.sqrMagnitude < 0.1f)
			return;

		//Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (player.ragdoll.fireSpot.position.x, player.ragdoll.fireSpot.position.y);

		//RaycastHit2D hit = Physics2D.Raycast(firePointPosition, (mousePosition - firePointPosition) * 1000.0f, 100.0f, layerToHit);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, diff * 1000.0f, 100.0f, layerToHit);

		Debug.DrawLine (firePointPosition, diff * 1000.0f, (isDraining ? Color.green : Color.yellow));
		Debug.Log ("Player shoot the lazer!");

		if (hit) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);

			if (hit.collider.CompareTag ("Player")) {
				PlayerScript targetPlayer = hit.collider.gameObject.GetComponentInParent<PlayerScript> ();

				if (isDraining) {
					if (player.mass <= PlayerSettings.maxMass && targetPlayer.mass >= PlayerSettings.minMass) {
						targetPlayer.mass -= massTransferRate * Time.deltaTime;
						player.mass += massTransferRate * Time.deltaTime;
					}
				} else {
					if (player.mass >= PlayerSettings.minMass && targetPlayer.mass <= PlayerSettings.maxMass) {
						targetPlayer.mass += massTransferRate * Time.deltaTime;
						player.mass -= massTransferRate * Time.deltaTime;
					}
				}
			} else if (hit.collider.CompareTag ("Tile")) {
				TileScript targetTile = hit.collider.gameObject.GetComponent<TileScript> ();

				if (isDraining) {
					if (player.mass <= PlayerSettings.maxMass && targetTile.mass >= PlayerSettings.minMass) {
						targetTile.mass -= massTransferRate * Time.deltaTime;
						player.mass += massTransferRate * Time.deltaTime;
					}
				} else {
					if (player.mass >= PlayerSettings.minMass && targetTile.mass <= PlayerSettings.maxMass) {
						targetTile.mass += massTransferRate * Time.deltaTime;
						player.mass -= massTransferRate * Time.deltaTime;
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
