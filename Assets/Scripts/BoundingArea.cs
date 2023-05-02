using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingArea : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Transform player;
    private void OnTriggerExit(Collider other) {

        if(other.CompareTag("Player"))
        {
            player = other.gameObject.transform;
            player.position = new Vector3(player.position.x,player.position.y, player.position.z) * 0.9f;
            player.LookAt(new Vector3(0f,0f,0f));
        }
    }
}
