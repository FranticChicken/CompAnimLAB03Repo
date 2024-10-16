using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navControl : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;

    private Animator animator;

    bool isWalking = true;
    bool isGoingToHitPt = false;
    RaycastHit hit;
    public float animatorSpeed;
    public float agentSpeed;
    public Transform dragonTransform;
    


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = animatorSpeed;

        agent.speed = agentSpeed;
        agentSpeed = 2f;

        if (isWalking && isGoingToHitPt == false)
        {
            agent.destination = target.transform.position;
            animatorSpeed = 2;
        }
        else if(isGoingToHitPt == false)
        {
            agent.destination = transform.position;
            animatorSpeed = 1;
            transform.LookAt(dragonTransform);
        }
        

        if(Vector3.Distance(transform.position, hit.point) <= 0.5)
        {
            isGoingToHitPt = false;
        }

        Debug.Log(target.name);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == target.name)
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == target.name)
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                agent.destination = hit.point;
                isGoingToHitPt = true;
            }
        }
    }
}
