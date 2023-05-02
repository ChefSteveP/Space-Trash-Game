using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance;
    public float damping;
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        // to position the camera will move to is behind the ship at a magnitude of followDistance away.
        Vector3 movePosition = player.position + (-player.transform.forward * followDistance);
        transform.position = Vector3.SmoothDamp(transform.position,movePosition, ref velocity, damping);
        transform.LookAt(player);
    }
}
