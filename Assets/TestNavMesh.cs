using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavMesh : MonoBehaviour
{


    [SerializeField]
    private Transform goal;

    private NavMeshAgent agent; 

    // Use this for initialization
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = moveTo(agent);
    }

    public Vector3 moveTo (UnityEngine.AI.NavMeshAgent navMeshAgent)
    {
        // Use Unity A* to move 
        if (navMeshAgent != null)
        {
            //navMeshAgent.acceleration = 1; 
            //navMeshAgent.speed = agentAttr.MoveSpd; 
            navMeshAgent.destination = new Vector3(goal.position.x, 0f, goal.position.y);
            navMeshAgent.updatePosition = false;

            UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
            navMeshAgent.CalculatePath(this.transform.position, path);

            // move towards next corner 
            return this.transform.position + Time.deltaTime * 3 * (path.corners[0] - this.transform.position).normalized;
        }
        return this.transform.position;
    }
}
