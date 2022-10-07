using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WanderingFrogAI : MonoBehaviour
{
     //references
     public Player player;
     public BoxCollider collider;
   
    public float moveSpeed = 3f;
    public float runSpeed = 5f; //run faster to lake since froggy is scared
    public float rotSpeed = 100f; //rotation speed
    public float lookAtSpeed = 1f; // speed to look at lake target while rotating

    [SerializeField] private bool isWandering = false;
    [SerializeField] private bool isRotatingLeft = false;
    [SerializeField] private bool isRotatingRight = false;
    [SerializeField] private bool isWalking = false;
    [SerializeField] private bool isGoingToLake = false;
    public float minDistance = 2f;
    
    public AudioSource audi1;
    public AudioClip clip1;

    [SerializeField] private float waitTime = 0.5f; //initial wait time of 1/2 second

    [SerializeField] private GameObject waterCollider;

    public Animator animator;

    public CollisionEnterSound collisionEnterSound;

    public Rigidbody rigidBody;
    
    private Coroutine LookCoroutine; //stop any existing coroutines and start this one

    public void Start()
    {
        audi1 = GetComponent<AudioSource>();
        collisionEnterSound.Audi1 = audi1;
        collisionEnterSound.Audi1.clip = clip1;
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);

        //remove when can check out code and update frogs to take player object at base
        if (player == null)
            player = FindObjectOfType<Player>();   
        
        if (waterCollider == null)
            waterCollider = GameObject.FindGameObjectWithTag("WaterCollider");
    }

    public void StartRotating()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAtWaterCollider());
    }

    private IEnumerator LookAtWaterCollider()
    {
        float stepSpeed = lookAtSpeed * Time.deltaTime;
        Vector3 targetDirection = waterCollider.transform.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, stepSpeed, 0.0f);
            
        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);
            
        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        yield return new WaitForSeconds(lookAtSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float step = runSpeed * Time.deltaTime;
        

        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            animator.Play("FrogIdleAnim");
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            animator.Play("FrogIdleAnim");
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            animator.Play("FrogJumpAnim");
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (isGoingToLake == true)
        {
            StartRotating();
            isWandering = false;

            animator.Play("FrogJumpAnim");
            transform.position = Vector3.MoveTowards(transform.position, waterCollider.transform.position, step);
            
        }
        
        if (Vector3.Distance(transform.position, player.transform.position) <= minDistance)
        {

            if(!collisionEnterSound.Audi1.isPlaying)
                collisionEnterSound.Audi1.Play();

            if (Vector3.Distance(transform.position, waterCollider.transform.position) > minDistance)
            {
                isGoingToLake = true;
                Debug.Log("going to lake");
            }

            
        }
        if (Vector3.Distance(transform.position, waterCollider.transform.position) <= 5)
        {
            this.gameObject.SetActive(false);
            // change to delete later
            // use item pooling as well
        }
        
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(2, 3); //rotation time
        int rotateWait = Random.Range(1, 3); // wait to rotate
        int rotateLorR = Random.Range(1, 2); // rotate left or right
        int walkWait = Random.Range(1, 3); //randomize time to walk and sit
        int walkTime = Random.Range(2, 4);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        
        isWandering = false;
    }
}
