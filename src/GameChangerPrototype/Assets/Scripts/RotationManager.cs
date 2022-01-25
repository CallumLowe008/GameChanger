using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [Header("References")]
    public ControlManager controlManager;
    public GameObject level;
    public int id;
    public Transform player;

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
        float currentAngle = level.transform.eulerAngles.z;

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

            level.transform.eulerAngles += new Vector3(0, 0, rotationSpeed) * rotationDirection * Time.deltaTime;

            if (Mathf.Abs(diff) < minBuffer) {
                level.transform.eulerAngles = new Vector3(0, 0, targetAngle);
                isRotating = false;
            }

            if (targetAngle < 0) { // Makes sure that the direction is correct
                targetAngle += 360;
                angleIndex  += 4;
            }
            else if (targetAngle > 360) {
                targetAngle -= 360;
                angleIndex -= 4;
            }
        }
    }

    void SetRotationCenter() {
        List<Transform> children = new List<Transform>(); // Detaches children before setting position
        for (int i = 0; i < level.transform.childCount; i += 1) {
            Transform child = level.transform.GetChild(i);
            children.Add(child);
        }

        level.transform.DetachChildren();

        level.transform.position = player.position;

        foreach (Transform child in children) {
            child.SetParent(level.transform);
        }
    }
}
