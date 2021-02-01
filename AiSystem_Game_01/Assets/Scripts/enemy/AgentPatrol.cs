using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentPatrol : MonoBehaviour
{
    Animator anim;

    NavMeshAgent  agent;
    bool patrolling;
    bool arrived;

    public Transform[] patrolTargets;
    private int destPoints;




    void start()
    {

        agent = GetComponent<NavMeshAgent>();
        


    }


    void update()
    {

        if (agent.pathPending)
        {
            return;
        }

        if (patrolling)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
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

        else
        {
            StartCoroutine("GoToNextPoint");
        }

    }


    IEnumerator GoToNextPoint()
    {
        if (patrolTargets.Length == 0)
        {
            yield break;

        }


        patrolling = true;

        yield return new WaitForSeconds(2f);

        arrived = false;
        agent.destination = patrolTargets[destPoints].position;
        destPoints = (destPoints + 1) % patrolTargets.Length;





    }














}
























