using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentOliviaScript : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointlndex;
    Vector3 target;
    
    // Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		UpdateDestination();
	}


    // Update is called once per frame
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
