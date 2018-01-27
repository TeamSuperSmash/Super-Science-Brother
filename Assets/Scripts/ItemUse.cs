using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float itemSpawnDist;

    [Range(0.0f, 16.0f)]
    public float fatboySpawnDistX;

    [Range(0.0f, 5.0f)]
    public float fatboySpawnDistY;

    public List<GameObject> itemList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("ItemUse")) {
            Debug.Log("I pressed E!");
            if(ItemSpawnManagerScript.instance.player.inventorySlot != ItemType.Nothing) {
                switch (ItemSpawnManagerScript.instance.player.inventorySlot) {
                    case ItemType.Bazooka:
                        break;
                    case ItemType.BombInc:
                    case ItemType.BombDec:
                        GameObject go = Instantiate(itemList[1], new Vector2(ItemSpawnManagerScript.instance.player.ragdoll.body.position.x + itemSpawnDist, ItemSpawnManagerScript.instance.player.ragdoll.body.position.y), transform.rotation);
                        if(ItemSpawnManagerScript.instance.player.inventorySlot == ItemType.BombInc) {
                            go.GetComponent<MassBomb>().type = BombType.BombIncrease;
                        } else {
                            go.GetComponent<MassBomb>().type = BombType.BombDecrease;
                        }
                        break;
                    case ItemType.Fatboy:
                        Instantiate(itemList[2], new Vector2(ItemSpawnManagerScript.instance.player.ragdoll.body.position.x + fatboySpawnDistX, fatboySpawnDistY), transform.rotation);
                        break;
                    case ItemType.Restore:
                        break;
                    case ItemType.Shield:
                        break;
                }
                ItemSpawnManagerScript.instance.player.inventorySlot = ItemType.Nothing;
            }
        }
	}
}
