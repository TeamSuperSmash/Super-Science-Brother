using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Nothing = -1,
    Bomb = 0,
    Restore = 1,
    Shield = 2,
    Bazooka = 3,
    Fatboy = 4
};

public class ItemPickup : MonoBehaviour
{
    public PlayerScript player;

    void OnTriggerEnter2D(Collider2D coll) {
        if(player.inventorySlot == ItemType.Nothing) {
            switch (coll.transform.tag) {
                case "Bomb":
                    player.inventorySlot = ItemType.Bomb;
                    break;
                case "Restore":
                    player.inventorySlot = ItemType.Restore;
                    break;
                case "Shield":
                    player.inventorySlot = ItemType.Shield;
                    break;
                case "Bazooka":
                    player.inventorySlot = ItemType.Bazooka;
                    break;
                case "Fatboy":
                    player.inventorySlot = ItemType.Fatboy;
                    break;
            }
            Destroy(coll.gameObject);
            //After Destroying we need to change the Animation for the ItemSlot.
        }
    }
}
