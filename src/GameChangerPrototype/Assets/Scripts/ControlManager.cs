using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public Dictionary<string, KeyCode>[] keyMap = new Dictionary<string, KeyCode>[] {
        new Dictionary<string, KeyCode>(),
        new Dictionary<string, KeyCode>()
    };

    void Start() {
        // Initial Keys
        keyMap[0].Add("right", KeyCode.D);
        keyMap[0].Add("left", KeyCode.A);
        keyMap[0].Add("stop", KeyCode.S);

        keyMap[1].Add("right", KeyCode.RightArrow);
        keyMap[1].Add("left", KeyCode.LeftArrow);
        keyMap[1].Add("stop", KeyCode.DownArrow);
    }

    public List<KeyCode> GetKeysInUse() {
        List<KeyCode> keys = new List<KeyCode>();

        foreach (Dictionary<string, KeyCode> player in keyMap) {
            foreach (KeyCode key in player.Values) {
                keys.Add(key);
            }
        }

        return keys;
    }

    public KeyCode UpdateKey(int id, string keyName) {
        return KeyCode.None;

        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        KeyCode key = new KeyCode();
        System.Random rand = new System.Random(); // Creates random generator
        do {
            int index = rand.Next(0, chars.Length); // Random.Next() generates a random number between the two arguments
            key = (KeyCode)System.Enum.Parse(typeof(KeyCode), chars[index].ToString()); // Converts char to string, and string to KeyCode
        }
        while (GetKeysInUse().Contains(key)); // Generates a random key repeatedly until it lands on one that is currently not in use

        keyMap[id][keyName] = key; // Updates key dict
        Debug.Log(keyName + ": " + key); // Displays new key in console. Temporary
        return key;
    }
}
