using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor_Key : MonoBehaviour
{
    public string keyColour;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            foreach (KeyDoor_Door door in GameObject.FindObjectsOfType<KeyDoor_Door>()) {
                if (door.doorColour == keyColour) {
                    Destroy(door.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
