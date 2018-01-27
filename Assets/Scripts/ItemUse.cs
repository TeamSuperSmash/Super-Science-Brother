using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour {
    public float countdown;
    public float buffTime = 5;

    [Range(0.0f, 1.0f)]
    public float itemSpawnDist;

    [Range(0.0f, 16.0f)]
    public float fatboySpawnDistX;

    [Range(0.0f, 5.0f)]
    public float fatboySpawnDistY;

    public List<GameObject> itemList = new List<GameObject>();

    public PlayerGunScript pgs;

    public bool usingBazooka;

    void Start() {
        countdown = buffTime;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("ItemUse")) {
            if (ItemSpawnManagerScript.instance.player.inventorySlot != ItemType.Nothing) {
                switch (ItemSpawnManagerScript.instance.player.inventorySlot) {
                    case ItemType.Bazooka:
                        ItemSpawnManagerScript.instance.player.inventorySlot = ItemType.Nothing;

                        countdown = buffTime;
                        pgs.bazookaForce = 350;
                        usingBazooka = true;
                        break;
                    case ItemType.BombInc:
                    case ItemType.BombDec:
                        GameObject go = Instantiate(itemList[0], new Vector2(ItemSpawnManagerScript.instance.player.ragdoll.body.position.x + itemSpawnDist, ItemSpawnManagerScript.instance.player.ragdoll.body.position.y), transform.rotation);
                        if(ItemSpawnManagerScript.instance.player.inventorySlot == ItemType.BombInc) {
                            go.GetComponent<MassBomb>().type = BombType.BombIncrease;
                        } else {
                            go.GetComponent<MassBomb>().type = BombType.BombDecrease;
                        }
                        break;
                    case ItemType.Fatboy:
                        Instantiate(itemList[1], new Vector2(ItemSpawnManagerScript.instance.player.ragdoll.body.position.x + fatboySpawnDistX, fatboySpawnDistY), transform.rotation);
                        break;
                    case ItemType.Restore:
                        break;
                    case ItemType.Shield:
                        break;
                }
                ItemSpawnManagerScript.instance.player.inventorySlot = ItemType.Nothing;
            }
        }

        if(usingBazooka) {
            countdown -= Time.deltaTime;
            if(countdown <= 0.0f) {
                usingBazooka = false;
                countdown = buffTime;
                pgs.bazookaForce = 0;
            }
        }
    }
}
