using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Đối tượng Player
    public Vector3 offset;    // Khoảng cách giữa Camera và Player
    public float smoothSpeed = 0.125f; // Độ mượt khi Camera di chuyển

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player); // Camera luôn hướng về Player
    }
}

