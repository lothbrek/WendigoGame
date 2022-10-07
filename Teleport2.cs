using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport2 : MonoBehaviour
{
    public Player player;

    public GameObject targetTransform;

    private void OnTriggerEnter(Collider other)
    {
        // player.transform.position = targetTransform.transform.position;
        
        // TODO: replace following section with Non-NavMeshAgent specifics
        player.agent.Warp(targetTransform.transform.position);
        player.agent.updatePosition = false;
        player.agent.updateRotation = false;
        player.isMoving = false;
    }
    
}
