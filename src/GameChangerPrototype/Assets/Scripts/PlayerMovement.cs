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

        KeyCode value;
        if (controlManager.keyMap[id].TryGetValue("stop", out value)) {
            if (Input.GetKey(value)) {
                stopper = 0;
            }
            else {
                stopper = 1;
            }
        }   

        body.position += new Vector2(moveSpeed * direction, 0) * Time.deltaTime * stopper;
    }
}
