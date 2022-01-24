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
        Gizmos.color = Color.magenta;
        for (var i = 0; i < movementPoints.Length-1; i += 1) {
            Gizmos.DrawLine(movementPoints[i], movementPoints[i+1]);
        }
    }

    void Start() {
        transform.position = movementPoints[0];

        movementComplete = false;
        targetIndex = 1;
        isMoving = false;
        stopAlarm = 0;
        iterDirection = 1;
    }

    void Update() {
        if (movementComplete == false) {
            if (isMoving) {
                Vector3 positionDifference = movementPoints[targetIndex] - transform.position;
                Vector3 moveDirection = positionDifference.normalized;
                transform.position += moveDirection * movementSpeed * Time.deltaTime;

                if (positionDifference.magnitude < lockDistance) {
                    transform.position = movementPoints[targetIndex];
                    targetIndex += iterDirection;
                    if (targetIndex < 0 || targetIndex >= movementPoints.Length) {
                        targetIndex -= iterDirection;
                        if (doesLoop) {
                            iterDirection = -iterDirection;
                        }
                        else {
                            movementComplete = true;
                        }
                    }
                    
                    isMoving = false;
                }
            }
            else {
                stopAlarm += Time.deltaTime;
                if (stopAlarm >= stopLength) {
                    stopAlarm = 0;
                    isMoving = true;
                }
            }
        }
    }
}
