using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public Sprite[] keyIcons;
    private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public Dictionary<string, KeyCode> controlKeys = new Dictionary<string, KeyCode>();
    public Dictionary<string, SpriteRenderer> keyVisuals = new Dictionary<string, SpriteRenderer>();

    [Header("Temporary Key References")]
    public SpriteRenderer rightKey;
    public SpriteRenderer leftKey;
    public SpriteRenderer stopKey;

    void Start() {
        // Initial Keys
        controlKeys.Add("right", KeyCode.D);
        controlKeys.Add("left", KeyCode.A);
        controlKeys.Add("stop", KeyCode.S);

        keyVisuals.Add("right", rightKey);
        keyVisuals.Add("left", leftKey);
        keyVisuals.Add("stop", stopKey);

        UpdateKeyVisual("right", controlKeys["right"].ToString());
        UpdateKeyVisual("left", controlKeys["left"].ToString());
        UpdateKeyVisual("stop", controlKeys["stop"].ToString());
    }

    public List<KeyCode> GetKeysInUse() {
        List<KeyCode> keys = new List<KeyCode>();
        foreach (KeyCode key in controlKeys.Values) {
            keys.Add(key);
        }
        return keys;
    }

    public KeyCode UpdateKey(string keyName) {
        KeyCode key = new KeyCode();
        System.Random rand = new System.Random(); // Creates random generator
        do {
            int index = rand.Next(0, chars.Length); // Random.Next() generates a random number between the two arguments
            key = (KeyCode)System.Enum.Parse(typeof(KeyCode), chars[index].ToString()); // Converts char to string, and string to KeyCode
        }
        while (GetKeysInUse().Contains(key)); // Generates a random key repeatedly until it lands on one that is currently not in use

        controlKeys[keyName] = key; // Updates key dict
        UpdateKeyVisual(keyName, key.ToString());
        return key;
    }

    private void UpdateKeyVisual(string keyName, string keyValue) {
        if (keyValue != "") {
            keyVisuals[keyName].sprite = keyIcons[chars.IndexOf(keyValue)];
        }
        else {
            Debug.Log("Key not generated properly");
        }
    }
}
