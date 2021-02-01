using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patroller : MonoBehaviour
{ 
    NavMeshAgent agent;
   
    Animator anim;
    public Transform target;
    Vector3 lastKnownPosition;
    public Transform eye;
    float Attack;
    float speed, agentSpeed;

    float enemyRange = 20;


    bool patrolling;
    bool arrived;
    public Transform[] patrolTargets;
    private int destPoint;


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
        lastKnownPosition = transform.position;


        if(agent != null)
        {
            agentSpeed = agent.speed;
        }

        target = GameObject.FindGameObjectWithTag("target").transform;

    }

    bool CanSeeTarget()
    {
        bool CanSee = false;
        Ray ray = new Ray(eye.position, target.transform.position - eye.position);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if(hit.transform != target)
            {
                CanSee = false;
            }
            else
            {
                lastKnownPosition = target.transform.position;
                CanSee = true;
            }
        }
        return CanSee;
    }




    // Update is called once per frame
    void Update()
    {
        if (agent.pathPending)
        {
            return;
        }
        if (patrolling)
        {
            if(agent.remainingDistance < agent.stoppingDistance)
            {
                if (!arrived)
                {
                    arrived = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
            else
            {
                arrived = false;
            }
        }
        if (CanSeeTarget())
        {
            agent.SetDestination(target.transform.position);
            patrolling = false;

            if(agent.remainingDistance <  agent.stoppingDistance)
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
            }
        }
        else
        {
            anim.SetBool("Attack", false);

            if (!patrolling)
            {
                agent.SetDestination(lastKnownPosition);
                if(agent.remainingDistance < agent.stoppingDistance)
                {
                    patrolling = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
            
        }
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);

        transform.LookAt(target);
    }
    IEnumerator GoToNextPoint()
    {
        if(patrolTargets.Length == 0)
        {
            yield break;
        }
        patrolling = true;
        yield return new WaitForSeconds(2f);
        arrived = false;
        agent.destination = patrolTargets[destPoint].position;
        destPoint = (destPoint + 1) % patrolTargets.Length;

        agent.speed = agentSpeed / 2;


        if(target != null && Vector3.Distance(transform.position, target.position) < enemyRange)
        {
            agent.destination = target.position;
            agent.speed = agentSpeed;
        }
    }



}
