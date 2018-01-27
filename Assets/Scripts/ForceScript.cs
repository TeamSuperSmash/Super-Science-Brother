using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceScript : MonoBehaviour
{
	public float power = 5000.0f;
	public float radius = 100.0f;

	public float lifeTime = 0.1f;
	public float lifeTimeCounter = 0.0f;
	
	public float forceAmplify = 10.0f;
	
    public PlayerGunScript pgs;

    void Awake ()
	{

	}

	void Start ()
	{

	}

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

        if(tempRb2d) {
            if (other.gameObject.CompareTag("Player")) {
                Debug.Log("Player toucha the exploda!");
                float tempPlayerMass = other.GetComponentInParent<PlayerScript>().playerMass;

                // Power is inverse to the player's mass
                power = (-tempPlayerMass + 250.0f) * forceAmplify;
            } else {
                power = 250.0f * forceAmplify;
            }

            if (power < 0.0f) {
                power = 0.0f;
            }

            AddExplosionForce2D(tempRb2d, power + pgs.bazookaForce, transform.position, radius);
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
