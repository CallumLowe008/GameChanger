using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public Dictionary<string, KeyCode> controlKeys = new Dictionary<string, KeyCode>();
    public Dictionary<string, SpriteRenderer> keyVisuals = new Dictionary<string, SpriteRenderer>();

    [Header("General")]
    public bool disableKeyChanges;

    [Header("Key Icons")]
    public Sprite[] keyIcons;

    [Header("Temporary Key References")]
    public SpriteRenderer rightKey;
    public SpriteRenderer leftKey;
    public SpriteRenderer stopKey;

    [Header("Key Press Indication")]
    public Color baseTint;
    public Color pressTint;

    void Start() {
        // Initial Keys
        controlKeys.Add("right", KeyCode.D);
        controlKeys.Add("left", KeyCode.A);
        controlKeys.Add("stop", KeyCode.S);

        keyVisuals.Add("right", rightKey);
        keyVisuals.Add("left", leftKey);
        keyVisuals.Add("stop", stopKey);

        if (disableKeyChanges == false) {
            UpdateKey("right");
            UpdateKey("left");
            UpdateKey("stop");
        }

        ChangeKeyIcon("right");
        ChangeKeyIcon("left");
        ChangeKeyIcon("stop");
    }

    void Update() {
        KeyPressIndication();
    }

    public List<KeyCode> GetKeysInUse() {
        List<KeyCode> keys = new List<KeyCode>();
        foreach (KeyCode key in controlKeys.Values) {
            keys.Add(key);
        }
        return keys;
    }

    public KeyCode UpdateKey(string keyName) {
        if (disableKeyChanges == true) {
            return KeyCode.None;
        }

        List<KeyCode> keysInUse = GetKeysInUse();
        foreach (ControlManager cm in FindObjectsOfType<ControlManager>()) {
            foreach (KeyCode oKey in cm.GetKeysInUse()) {
                keysInUse.Add(oKey);
            }
        }

        KeyCode key = new KeyCode();
        do {
            int index = Random.Range(0, chars.Length); // Random.Next() generates a random number between the two arguments
            key = (KeyCode)System.Enum.Parse(typeof(KeyCode), chars[index].ToString()); // Converts char to string, and string to KeyCode
        }
        while (keysInUse.Contains(key)); // Generates a random key repeatedly until it lands on one that is currently not in use

        controlKeys[keyName] = key; // Updates key dict
        return key;
    }

    void ChangeKeyIcon(string keyName) {
        string keyValue = controlKeys[keyName].ToString();
        if (keyValue != "") {
            keyVisuals[keyName].sprite = keyIcons[chars.IndexOf(keyValue)];
        }
        else {
            Debug.Log("Key not generated properly");
        }
    }

    void KeyPressIndication() {
        foreach (var keyData in controlKeys) {
            if (Input.GetKey(keyData.Value)) {
                keyVisuals[keyData.Key].color = pressTint;
            }
            else {
                keyVisuals[keyData.Key].color = baseTint;
            }

            if (Input.GetKeyUp(keyData.Value)) {
                UpdateKey(keyData.Key);
                ChangeKeyIcon(keyData.Key);
                break;
            }
        }
    }
}
