using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce;

    [Header("Trajectory Plotting")]
    public PlayerMovement playerMovement;
    public float trajectoryDrawIterations;
    public float trajectoryDrawIncrement;

    void OnDrawGizmosSelected() {

        if (trajectoryDrawIterations <= 0 || trajectoryDrawIncrement <= 0 || playerMovement == null) {
            return;
        }

        Vector3 playerHeight = Vector3.up * playerMovement.transform.localScale.y/2;

        // Calculate Velocity
        Vector2 vel = (Vector2.right*playerMovement.moveSpeed) + (Vector2.up*jumpForce);
        Vector3 velocity = new Vector3(vel.x, vel.y, 0);

        // Draw Line
        Gizmos.color = Color.green;
        for (float i = 0; i < (trajectoryDrawIterations-trajectoryDrawIncrement); i += trajectoryDrawIncrement) {
            Gizmos.DrawLine(
                PlotTrajectoryAtTime(transform.position, velocity, i) + playerHeight, 
                PlotTrajectoryAtTime(transform.position, velocity, i + trajectoryDrawIncrement) + playerHeight
            );
        }
    }

    Vector3 PlotTrajectoryAtTime(Vector3 startPos, Vector3 velocity, float time) {
        startPos += (velocity*time) + (Physics.gravity*time*time*0.5f);
        startPos = RotateAroundPoint(startPos, transform.position, transform.eulerAngles);
        return startPos;
    }

    Vector3 RotateAroundPoint(Vector3 pos, Vector3 pivot, Vector3 angles) {
        Vector3 dir = pos - pivot;
        dir = Quaternion.Euler(angles) * dir;
        pos = dir + pivot;
        return pos;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
