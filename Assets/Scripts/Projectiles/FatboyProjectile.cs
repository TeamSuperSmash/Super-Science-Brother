using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatboyProjectile : MonoBehaviour {

    [Range(0, 50)]
    public float moveSpeed;

    private Rigidbody2D rb;

	void Awake ()
	{
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
        rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);

        if(this.transform.position.y < -30)
		{
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
	{
        if (coll.gameObject.CompareTag("Tile")) 
		{
            coll.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            coll.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
