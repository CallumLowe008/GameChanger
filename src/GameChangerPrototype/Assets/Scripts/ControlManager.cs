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
        keyMap[0].Add("up", KeyCode.W);
        keyMap[0].Add("right", KeyCode.A);
        keyMap[0].Add("left", KeyCode.S);
        keyMap[0].Add("down", KeyCode.D);
        keyMap[0].Add("stop", KeyCode.LeftShift);

        keyMap[1].Add("up", KeyCode.UpArrow);
        keyMap[1].Add("right", KeyCode.RightArrow);
        keyMap[1].Add("left", KeyCode.LeftArrow);
        keyMap[1].Add("down", KeyCode.DownArrow);
        keyMap[1].Add("stop", KeyCode.RightControl);
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
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        KeyCode key = new KeyCode();
        System.Random rand = new System.Random();
        do {
            int index = rand.Next(0, chars.Length);
            key = (KeyCode)System.Enum.Parse(typeof(KeyCode), chars[index].ToString());
        }
        while (GetKeysInUse().Contains(key));

        keyMap[id][keyName] = key;
        return key;
    }
}
