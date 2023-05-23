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

    //***Patroling Parameters***//
    private int PatrollingType = 0 ;
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //***GameOver Parameters***//
	private bool isGameOver = false; // Oyunun bitip bitmediği bilgisini tutar

    //***States Parameters***//
    public float sightRange, bustedRange;
    public bool thePlayerInEnemyFOV, playerInSightRange, playerInBustedRange = false;


    //***thePlayerInEnemyFOV Parameters***//
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
        // For thePlayerInEnemyFOV
        playerCollider = player.GetComponent<Collider>();
        theEnemyPovCamera = GetComponentsInChildren<UnityEngine.Camera>()[0];
    }

    private void Update()
    {
        //Check for sight and busted range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInBustedRange = Physics.CheckSphere(transform.position, bustedRange, whatIsPlayer);

        //***Patrolling State***//
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

        //***Chasing State***//
        if (playerInSightRange && !playerInBustedRange && thePlayerInEnemyFOV) 
        {
            ChasePlayer();
            IterateWaypointIndex();
        }
    
        
        //***GameOver State***//
        if (playerInBustedRange && playerInSightRange && thePlayerInEnemyFOV) 
        {
            GameOver();
        }

        PlayerDetection();
    }


    private void Patroling_v1()
    {
		//Debug.Log("Patrolling yapılıyor");        
		//Debug.Log("Alex Agent Konum : " + transform.position + "Target Konum : " + target);
		//Debug.Log("Alex Agent Konum : " + "Target Konum : " + target);
        if (Vector3.Distance(transform.position, target) < 2)
		{
		//  Debug.Log("mesafe farkı birden kücük");
			IterateWaypointIndex();
			UpdateDestination();
		}
    }

    private void Patroling_v2()    // Rastgele lokasyon belirleyerek patrolling yapma
    {
        // Debug.Log("Agent Konum : " + transform.position + "Target Konum : " + walkPoint);
        if (!walkPointSet) 
		{
			SearchWalkPoint();
			//Debug.Log("walkPointSet True");
		}

        if (walkPointSet)
		{
            agent.SetDestination(walkPoint);
			// Debug.Log("SetDestination True");
		}

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
		{
            walkPointSet = false;
			// Debug.Log("Walkpoint reached!");
		}
    }

	void UpdateDestination()
	{
        
		//Debug.Log("UpdateDestination");
		//Debug.Log(target);
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

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            //Debug.Log("walkPointSet = true");
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        PatrollingType = 1;
    }

    private void GameOver()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!isGameOver)
        {
            isGameOver = true;
            // Oyunun bitiş scene gösterilmelidir.
            //Debug.Log("Yakalandın! Oyun bitti!");
        }
        //SceneManager.LoadScene("MenuScene");
        //GameOverUI.SetActive(true);
        // GameOverUI.enabled = true;
        //GameOverScreen.Setup(maxTime);
    }

    private void PlayerDetection()
	{
		planes = GeometryUtility.CalculateFrustumPlanes(theEnemyPovCamera);
		if (GeometryUtility.TestPlanesAABB(planes, playerCollider.bounds))
		{
			//Debug.DrawLine(theEnemyPovCamera.transform.position, playerGameObject.transform.position, Color.blue);
			float distance = Vector3.Distance(theEnemyPovCamera.transform.position, playerGameObject.transform.position);
			RaycastHit hit;
            //Debug.Log("if in icinde");
			if (Physics.Linecast ( theEnemyPovCamera.transform.position, playerGameObject.transform.position, out hit) && ( hit.collider.tag == "Player"))
			{
				//Debug.Log("hit.collider.tag" + hit.collider.tag);
                //Debug.Log("thePlayerInEnemyFOV : " + thePlayerInEnemyFOV);
				thePlayerInEnemyFOV = true;
                //Debug.Log("if in icindeeeeeeeee");
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
