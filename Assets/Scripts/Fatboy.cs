﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatboy : MonoBehaviour {

    [Range(0, 50)]
    public float moveSpeed;

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);

        if(this.transform.position.y < -30) {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("platform")) {
            Destroy(coll.gameObject);
        }
    }
}
