using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce;

    [Header("Trajectory Plotting")]
    public PlayerMovement playerMovement;
    public int horizontalFacing;
    public int verticalFacing;
    public float trajectoryDrawIterations;
    public float trajectoryDrawIncrement;

    void OnDrawGizmos() {

        if (trajectoryDrawIterations <= 0 || trajectoryDrawIncrement <= 0 || playerMovement == null) {
            return;
        }

        Vector3 playerHeight = Vector3.up * playerMovement.transform.localScale.y/2;

        // Calculate Velocity
        Vector2 velocity = (Vector2.right*horizontalFacing*playerMovement.moveSpeed) + (Vector2.up*verticalFacing*jumpForce);

        // Draw Line
        Gizmos.color = Color.green;
        for (float i = 0; i < (trajectoryDrawIterations-trajectoryDrawIncrement); i += trajectoryDrawIncrement) {
            Gizmos.DrawLine(
                PlotTrajectoryAtTime(transform.position, velocity, i) + playerHeight, 
                PlotTrajectoryAtTime(transform.position, velocity, i += trajectoryDrawIncrement) + playerHeight
            );
        }
    }

    Vector3 PlotTrajectoryAtTime(Vector3 startPos3, Vector2 velocity, float time) {
        Vector2 startPos = new Vector2(startPos3.x, startPos3.y);
        return startPos + (velocity*time) + (Physics2D.gravity*time*time*0.5f*verticalFacing);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
