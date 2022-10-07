using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SittingFrog : MonoBehaviour
{
    //references
    public Player player;
    public BoxCollider collider;

    public AudioSource audi1;
    public AudioClip clip1;

    public Animator animator;
    public float minDistance = 2f;

    public CollisionEnterSound collisionEnterSound;

    public Rigidbody rigidBody;
    
    public void Start()
    {
        audi1 = GetComponent<AudioSource>();
        collisionEnterSound.Audi1 = audi1;
        collisionEnterSound.Audi1.clip = clip1;
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);

        //remove when can check out code and update frogs to take player object at base
        if (player == null)
            player = FindObjectOfType<Player>();   
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, player.transform.position) <= minDistance)
        {

            if(!collisionEnterSound.Audi1.isPlaying)
                collisionEnterSound.Audi1.Play();
            
        }

    }

}