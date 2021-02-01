using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemypatrolling: MonoBehaviour
{
    public float patrolTime = 10;
    public float enemyRange = 10;

    public Transform[] waypoints;


    int index;
    float speed, agentSpeed;
    Transform target;


    Animator anim;
    NavMeshAgent agent;



    void Awake ()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if(agent != null)
        {
            agentSpeed = agent.speed;
        }



        target = GameObject.FindGameObjectWithTag("target").transform;
        index = Random.Range(0, waypoints.Length);
    
    
    
    
    }







}
