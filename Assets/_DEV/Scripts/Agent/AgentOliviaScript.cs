using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentOliviaScript : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointlndex;
    Vector3 target;
    
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		UpdateDestination();
	}

	void Update()
	{
		if (Vector3. Distance(transform. position, target) < 1)
		{
			IterateWaypointIndex();
			UpdateDestination();
		}
	}

    // References Functions
	void UpdateDestination()
	{
		target = waypoints[waypointlndex].position;
		agent.SetDestination(target);
	}
	
	void IterateWaypointIndex()
	{
		waypointlndex++;
		if (waypointlndex == waypoints.Length)
		{
			waypointlndex = 0;
		}
	}
}