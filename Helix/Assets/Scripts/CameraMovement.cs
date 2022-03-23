using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    public float PositionOffset;
    private Vector3 Velocity;
    public float SmoothDamp;

    private void FixedUpdate()
    {
        Vector3 TargetPosition = Player.position + offset;
        if (Player.position.y < transform.position.y + PositionOffset)
            transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref Velocity, SmoothDamp);
    }
}
