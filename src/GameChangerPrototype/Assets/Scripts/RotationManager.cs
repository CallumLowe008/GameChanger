using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    // Respond To Input X
    // Stop Player
    // Rotate Level X
    // Restart Player
    // Update Input

    [Header("References")]
    public ControlManager controlManager;
    public GameObject level;
    public int id;

    [Header("Rotation")]
    public int angleIndex;
    private int targetAngle;
    public float rotationSpeed;
    public float rotationDirection;
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
            if (Input.GetKeyDown(keys["right"])) {
                angleIndex -= 1;
                rotationDirection = -1;
                isRotating = true;
            }
            if (Input.GetKeyDown(keys["left"])) {
                angleIndex += 1;
                rotationDirection = 1;
                isRotating = true;
            }
            targetAngle = angleIndex * 90;
        }

        // Rotate Level
        if (isRotating == true) {
            float diff = targetAngle - currentAngle;

            level.transform.eulerAngles += new Vector3(0, 0, rotationSpeed) * rotationDirection * Time.deltaTime;

            if (Mathf.Abs(diff) < minBuffer) {
                level.transform.eulerAngles = new Vector3(0, 0, targetAngle);
                isRotating = false;
            }

            if (targetAngle < 0) {
                targetAngle += 360;
                angleIndex  += 4;
            }
            else if (targetAngle > 360) {
                targetAngle -= 360;
                angleIndex -= 4;
            }
        }
    }
}
