using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D body;
    public ControlManager controlManager;
    public RotationManager rotationManager;

    [Header("Movement")]
    public float moveSpeed;
    public int direction;
    public int stopper;
    public Vector2 storedVelocity;

    void Start() {
        direction = 1;
        stopper = 1;
        storedVelocity = Vector3.zero;
    }

    void Update() {
        if (rotationManager.state == RotationManager.RotationState.rotating) { // Stops player compeltely while level is rotating
            stopper = 0;
            body.gravityScale = 0;
            if (storedVelocity == Vector2.zero) {
                storedVelocity = body.velocity;
            }
            body.velocity = Vector2.zero;
        }
        else {
            stopper = 1;
            body.gravityScale = 1;
            if (storedVelocity != Vector2.zero) {
                body.velocity = storedVelocity;
                storedVelocity = Vector3.zero;
            }
        }

        Dictionary<string, KeyCode> playerKeys = controlManager.controlKeys; // Fetches input keys from ControlManager
        if (playerKeys.ContainsKey("stop")) {
            if (Input.GetKey(playerKeys["stop"])) {
                stopper = 0;
            }

            if (Input.GetKeyUp(playerKeys["stop"])) {
                stopper = 1;
            }
        }

        body.position += new Vector2(moveSpeed * direction, 0) * Time.deltaTime * stopper;
    }
}
