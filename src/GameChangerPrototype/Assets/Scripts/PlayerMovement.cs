using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D body;
    public ControlManager controlManager;
    public RotationManager rotationManager;
    public int id;

    [Header("Movement")]
    public float moveSpeed;
    public int direction;
    public int stopper;

    void Start() {
        direction = 1;
        stopper = 1;
    }

    void Update() {
        if (rotationManager.state == RotationManager.RotationState.rotating) { // Stops player compeltely while level is rotating
            stopper = 0;
            body.gravityScale = 0;
        }
        else {
            stopper = 1;
            body.gravityScale = 1;
        }

        Dictionary<string, KeyCode> playerKeys = controlManager.keyMap[id]; // Fetches input keys from ControlManager
        if (playerKeys.ContainsKey("stop")) {
            if (Input.GetKey(playerKeys["stop"])) {
                stopper = 0;
            }

            if (Input.GetKeyUp(playerKeys["stop"])) {
                stopper = 1;
                controlManager.UpdateKey(id, "stop");
            }
        }

        body.position += new Vector2(moveSpeed * direction, 0) * Time.deltaTime * stopper;
    }
}
