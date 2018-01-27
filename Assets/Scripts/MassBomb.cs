﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BombType {
    BombIncrease,
    BombDecrease,
};

public class MassBomb : MonoBehaviour {

    [Range(0.0f, 32.0f)]
    public float bombRadi;

    [Range(0.0f, 30.0f)]
    public float forceX;

    [Range(0.0f, 15.0f)]
    public float forceY;

    public float delay = 1.5f;

    float countdown;

    public BombType type;

    PlayerScript player;

    Rigidbody2D rb;

    void Start() {
        rb.AddForce(new Vector2(forceX * 30, forceY * 30));
    }

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        countdown = delay;
	}

    // Update is called once per frame
    void Update() {
        countdown -= Time.deltaTime;
        if(countdown <= 0) {
            Explode();
            Debug.Log("Kaboom");
            Destroy(this.gameObject);
        }
    }

    void Explode() {
        //Integrating the Tile Mass First
        Collider2D[] bombReturnList = Physics2D.OverlapCircleAll(this.transform.position, bombRadi);
        for (int i = 0; i < bombReturnList.Length; i++) {
            if (bombReturnList[i].CompareTag("Tile")) {
                if (this.type == BombType.BombIncrease) {
                    bombReturnList[i].GetComponent<TileScript>().curMatValue += (bombReturnList[i].GetComponent<TileScript>().curMatValue / 2);
                } else {
                    bombReturnList[i].GetComponent<TileScript>().curMatValue -= (bombReturnList[i].GetComponent<TileScript>().curMatValue / 2);
                }
            }
        }
    }
}