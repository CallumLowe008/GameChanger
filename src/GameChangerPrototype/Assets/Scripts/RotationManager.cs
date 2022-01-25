using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [Header("References")]
    public ControlManager controlManager;
    public int id;
    public Transform level;
    public Transform player;
    public Transform cam;

    [Header("Rotation")]
    private int angleIndex;
    private int targetAngle;
    private int rotationDirection;
    public float rotationSpeed;
    public float minBuffer;
    public bool isRotating;

    void Start() {
        isRotating = false;
        rotationDirection = 1;
    }

    void Update() {
        Dictionary<string, KeyCode> keys = controlManager.keyMap[id];
        float currentAngle = level.eulerAngles.z;

        // Respond To Input
        if (isRotating == false) {
            string keyPressed = "";
            if (Input.GetKeyDown(keys["right"])) {
                rotationDirection = -1;
                keyPressed = "right";

            }
            if (Input.GetKeyDown(keys["left"])) {
                rotationDirection = 1;
                keyPressed = "left";
            }

            if (keyPressed != "") {
                angleIndex += rotationDirection;
                targetAngle = angleIndex * 90;

                controlManager.UpdateKey(id, keyPressed); // Randomises the pressed key
                SetRotationCenter(); // Moves the rotation center to the player

                isRotating = true;
            }
        }

        // Rotate Level
        if (isRotating == true) {
            float diff = targetAngle - currentAngle;

            level.eulerAngles += new Vector3(0, 0, rotationSpeed) * rotationDirection * Time.deltaTime;

            if (Mathf.Abs(diff) < minBuffer) {
                level.eulerAngles = new Vector3(0, 0, targetAngle);
                isRotating = false;
                
                ResetPosition(); // Stops the level drifting off (which it does due to rotating around the player, meaning it moves down and right constantly)
            }

            if (targetAngle < 0) { // Makes sure that the direction is correct
                targetAngle += 360;
                angleIndex  += 4;
            }
            else if (targetAngle >= 360) {
                targetAngle -= 360;
                angleIndex -= 4;
            }
        }
    }

    void SetRotationCenter() {
        List<Transform> children = new List<Transform>(); // Detaches children before setting position
        for (int i = 0; i < level.childCount; i += 1) {
            Transform child = level.GetChild(i);
            children.Add(child);
        }

        level.DetachChildren();

        level.position = player.position;

        foreach (Transform child in children) {
            child.SetParent(level);
        }
    }

    void ResetPosition() {
        level.position -= player.position;
        cam.position -= player.position;
        player.position -= player.position;
    }
}
