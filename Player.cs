using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class Player : MonoBehaviour
{
    public Camera camera;
    public CinemachineVirtualCamera virtualCinemachinCamera;

    private RaycastHit hit;

    public NavMeshAgent agent;

    public Animator playerAnimator;

    public float speed;
    public float heightOffset = 0.4f; // character height offset
    public bool isRunning = false;

    public bool isMoving = false;
    
    // look for ground tag
    private string groundTag = "Ground";
    private string waterTag  = "Water";
    private string teleTag   = "Teleporter";

    public float TouchSensitivity_x = 20f;
    public float TouchSensitivity_y = 20f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }
    
    void Update()
    {


        transform.position = new Vector3(transform.position.x,
            Terrain.activeTerrain.SampleHeight(transform.position) + heightOffset,
            transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;

            if (isMoving == true)
            {
                transform.position = new Vector3(transform.position.x,
                    Terrain.activeTerrain.SampleHeight(transform.position) + heightOffset,
                    transform.position.z + 1 * speed * Time.deltaTime);
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag(groundTag) || hit.collider.CompareTag(waterTag) ||
                        hit.collider.CompareTag(teleTag))
                    {
                        agent.SetDestination(hit.point);
                    }
                }
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isRunning = false;
        }
        else
        {
            isRunning = true;
        }
        playerAnimator.SetBool("isWalking", isRunning);
    }

    public float GetAxisCustom(string axisName)
    {
        switch (axisName)
        {
            case "Mouse X":
                if (Input.touchCount > 0)
                {
                    //Is mobile touch
                    return Input.touches[0].deltaPosition.x / TouchSensitivity_x;
                }
                else if (Input.GetMouseButton(1))
                {
                    // is mouse click
                    return Input.GetAxis("Mouse X");
                }
                break;
            case "Mouse Y":
                if (Input.touchCount > 0)
                {
                    //Is mobile touch
                    return Input.touches[0].deltaPosition.y / TouchSensitivity_y;
                }
                else if (Input.GetMouseButton(1))
                {
                    // is mouse click
                    return Input.GetAxis(axisName);
                }
                break;
            default:
                Debug.LogError("Input <" + axisName + "> not recognized.", this);
                break;
        }

        return 0f;
    }

}
