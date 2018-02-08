using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGunProjectile : MonoBehaviour
{
	public float forceAmplify = 10.0f;
	public float radius = 100.0f;

	public float lifeTime = 0.1f;
	public float lifeTimeCounter = 0.0f;

    void Update ()
	{
		CheckLifeTime ();
	}

	void AddExplosionForce2D (Rigidbody2D body, float explodeForce, Vector3 explodePos, float explodeRadius)
	{
		Vector3 dir = (body.transform.position - explodePos);
		float calculate = 1 - (dir.magnitude / explodeRadius);

		if (calculate < 0) {
			calculate = 0;
		}

		body.AddForce (dir.normalized * explodeForce * calculate);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Rigidbody2D tempRb2d = other.GetComponent<Rigidbody2D> ();

		if (tempRb2d) {
			float power = 0.0f;

			if (other.gameObject.CompareTag ("Player"))
            {
				Debug.Log ("Player toucha the exploda!");
                SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_GETTINGPUSH);

                PlayerScript tempPlayer = other.gameObject.GetComponent<PlayerScript> ();
                PlayerGunScript tempGun = other.gameObject.GetComponentInChildren<PlayerGunScript>();

                // Power is inverse to the player's mass.
                power = ((-tempPlayer.mass + 250.0f) * forceAmplify) + tempGun.forceGunFactor;
			}
            else
            {
				power = 250.0f * forceAmplify;
			}

			if (power < 0.0f) {
				power = 0.0f;
			}

            // Reset velocity to improve consistency.
			Vector2 v = tempRb2d.velocity;
			v.y = 0.0f;
			tempRb2d.velocity = v;

			AddExplosionForce2D (tempRb2d, power, transform.position, radius);
		}
	}

	void CheckLifeTime ()
	{
		lifeTimeCounter += Time.deltaTime;

		if (lifeTimeCounter > lifeTime) {
			lifeTimeCounter = 0.0f;

			Destroy (this.gameObject);
		}
	}
}
