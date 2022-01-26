using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] movementPoints;
    public float movementSpeed;
    public float stopLength;
    public bool doesLoop;
    public float lockDistance;

    public bool movementComplete;
    private int targetIndex;
    private bool isMoving;
    private float stopAlarm;
    private int iterDirection;

    void OnDrawGizmosSelected() {
        // Draws gizmo path when platform object is selected
        Gizmos.color = Color.magenta;
        for (var i = 0; i < movementPoints.Length-1; i += 1) {
            Gizmos.DrawLine(movementPoints[i], movementPoints[i+1]);
        }
    }

    void Start() {
        transform.localPosition = movementPoints[0];

        movementComplete = false;
        targetIndex = 1;
        isMoving = false;
        stopAlarm = 0;
        iterDirection = 1;
    }

    void Update() {
        if (movementComplete == false) {
            if (isMoving) {
                Vector3 positionDifference = movementPoints[targetIndex] - transform.localPosition;
                transform.localPosition += positionDifference.normalized * movementSpeed * Time.deltaTime;

                if (positionDifference.magnitude < lockDistance) {
                    transform.localPosition = movementPoints[targetIndex]; // Locks to the target position when it is close enough
                    targetIndex += iterDirection;
                    if (targetIndex < 0 || targetIndex >= movementPoints.Length) {
                        targetIndex -= iterDirection;
                        if (doesLoop) {
                            iterDirection = -iterDirection; // Reverses the direction of the array iteration
                        }
                        else {
                            movementComplete = true; // If 'doesLoop' is false, disable platform movement
                        }
                    }
                    
                    isMoving = false;
                }
            }
            else {
                stopAlarm += Time.deltaTime; // Alarm dependent on the 'stopLength' property
                if (stopAlarm >= stopLength) {
                    stopAlarm = 0;
                    isMoving = true;
                }
            }
        }
    }
}
