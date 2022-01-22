using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D body;
    public ControlManager controlManager;
    public int id;

    [Header("Movement")]
    public float moveSpeed;
    public int direction;
    public int stopper;

    void Start() {
        direction = 1;
    }

    void Update() {
        Dictionary<string, KeyCode> playerKeys = controlManager.keyMap[id];
        if (playerKeys.ContainsKey("stop")) {
            if (Input.GetKey(playerKeys["stop"])) {
                stopper = 0;
            }

            if (Input.GetKeyUp(playerKeys["stop"])) {
                stopper = 1;
                KeyCode key = controlManager.UpdateKey(id, "stop");
                Debug.Log(key);
            }
        }

        body.position += new Vector2(moveSpeed * direction, 0) * Time.deltaTime * stopper;
    }
}
