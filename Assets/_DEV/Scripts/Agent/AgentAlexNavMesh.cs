using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AgentAlexNavMesh : MonoBehaviour
{
	public NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointlndex;
    Vector3 target;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    private int PatrollingType = 0 ;
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private bool isGameOver = false;
	public float sightRange, bustedRange;
    public bool thePlayerInEnemyFOV, playerInSightRange, playerInBustedRange = false;
    public GameObject playerGameObject;
	private Camera theEnemyPovCamera;
	Collider playerCollider;
	Plane[] planes;

    private void Awake()
    {
		UpdateDestination();
        playerInSightRange = false;
        playerInBustedRange = false;
        thePlayerInEnemyFOV = false;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        playerCollider = player.GetComponent<Collider>();
        theEnemyPovCamera = GetComponentsInChildren<UnityEngine.Camera>()[0];
    }

    private void Update()
    {
        playerInSightRange = thePlayerInEnemyFOV && Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInBustedRange = thePlayerInEnemyFOV && Physics.CheckSphere(transform.position, bustedRange, whatIsPlayer);

        if (PatrollingType == 1)
        {
            if (!thePlayerInEnemyFOV) 
            {
                Patroling_v1();
		        UpdateDestination();
            }
        }
        else
        {
            if (!thePlayerInEnemyFOV) 
            {
                Patroling_v1();
            }
        }
        
        if (playerInSightRange && !playerInBustedRange && thePlayerInEnemyFOV) 
        {
            ChasePlayer();
            IterateWaypointIndex();
        }

        if (playerInBustedRange && playerInSightRange && thePlayerInEnemyFOV) 
        {
            GameOver();
        }

        PlayerDetection();
    }


    private void Patroling_v1()
    {
	    if (Vector3.Distance(transform.position, target) < 2)
		{
			IterateWaypointIndex();
			UpdateDestination();
		}
    }

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

	private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        PatrollingType = 1;
    }

    private void GameOver()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        
        if (!isGameOver)
        {
            isGameOver = true;
        }
    }

    private void PlayerDetection()
	{
		planes = GeometryUtility.CalculateFrustumPlanes(theEnemyPovCamera);
		if (GeometryUtility.TestPlanesAABB(planes, playerCollider.bounds))
		{
			float distance = Vector3.Distance(theEnemyPovCamera.transform.position, playerGameObject.transform.position);
			RaycastHit hit;
			if (Physics.Linecast ( theEnemyPovCamera.transform.position, playerGameObject.transform.position, out hit) && ( hit.collider.tag == "Player"))
			{
				thePlayerInEnemyFOV = true;
			}
			else
			{
				thePlayerInEnemyFOV = false;
			}
		}
		else
		{
			thePlayerInEnemyFOV = false;
		}
	}
}
