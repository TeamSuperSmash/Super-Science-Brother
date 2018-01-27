using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour
{
	public LayerMask toHit;

    public float bazookaForce = 0;

	public float hitRate = 1.0f;
	public float hitRateCounter = 0.0f;

	// How much mass is changed per hit rate.
	public float massTransferRate = 10.0f;

	RaycastHit2D hitPositiveMode;
	RaycastHit2D hitNegativeMode;

	public Transform forceOutPos;
	public GameObject forceEffect;

	public bool canShootForce = false;
	public float forceShootCooldown = 2.0f;
	public float forceShootCooldownCounter = 0.0f;

	public float playerMaxMass = 200.0f;
	public float playerMinMass = 0.0f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton ("Fire1")) {
			PlayerShoot0 ();
		} else if (Input.GetButton ("Fire2")) {
			PlayerShoot1 ();
		} else {
			hitRateCounter = 0.0f;
		}

		if (canShootForce) {
			if (Input.GetKeyDown (KeyCode.F)) {
				canShootForce = false;
				ForceShoot ();
			}
		} else if (!canShootForce) {
			forceShootCooldownCounter += Time.deltaTime;
			if (forceShootCooldownCounter > forceShootCooldown) {
				forceShootCooldownCounter = 0.0f;
				canShootForce = true;
			}
		}
	}

	void PlayerShoot0 ()
	{
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (transform.position.x, transform.position.y);

		hitPositiveMode = Physics2D.Raycast (firePointPosition, (mousePosition - firePointPosition) * 1000.0f, 100.0f, toHit);

		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 1000.0f, Color.green);
		Debug.Log ("Player shoot the expand lazer!");

		if (hitPositiveMode.collider != null) {
			Debug.DrawLine (firePointPosition, hitPositiveMode.point, Color.red);
			Debug.Log ("Player expanding an object!");

			if (hitPositiveMode.transform.CompareTag ("Player")) {
				hitRateCounter += Time.deltaTime;

				if (hitRateCounter > hitRate) {
					hitRateCounter = 0.0f;

					GameObject tempPlayer = hitPositiveMode.transform.gameObject;
					if (tempPlayer.GetComponentInParent<PlayerScript> ().playerMass < playerMaxMass) {
						tempPlayer.GetComponentInParent<PlayerScript> ().playerMass += massTransferRate;
					}
				}			
			} else {
				hitRateCounter = 0.0f;
			}
		}
	}

	void PlayerShoot1 ()
	{
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (transform.position.x, transform.position.y);

		hitNegativeMode = Physics2D.Raycast (firePointPosition, (mousePosition - firePointPosition) * 1000.0f, 100.0f, toHit);

		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 1000.0f, Color.yellow);
		Debug.Log ("Player shoot the detract lazer!");

		if (hitNegativeMode.collider != null) {
			Debug.DrawLine (firePointPosition, hitNegativeMode.point, Color.red);
			Debug.Log ("Player detracting an object!");

			if (hitNegativeMode.transform.CompareTag ("Player")) {

				hitRateCounter += Time.deltaTime;

				if (hitRateCounter > hitRate) {
					hitRateCounter = 0.0f;

					GameObject tempPlayer = hitNegativeMode.transform.gameObject;
					if (tempPlayer.GetComponentInParent<PlayerScript> ().playerMass > playerMinMass) {
						tempPlayer.GetComponentInParent<PlayerScript> ().playerMass -= massTransferRate;
					}
				}			
			} else {
				hitRateCounter = 0.0f;
			}
		}
	}

	void ForceShoot ()
	{
		Instantiate (forceEffect, new Vector3 (forceOutPos.transform.position.x, forceOutPos.transform.position.y, 0.0f), Quaternion.identity);
	}

	void ChangeShootMode ()
	{

	}
}
