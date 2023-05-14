using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AgentOliviaScript : MonoBehaviour
{   
    public GameOverScreen GameOverScreen;
    int maxTime = 0;
    public Image GameOverUI;
    public NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointlndex;
    Vector3 target;


    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    private int PatrollingType = 0 ;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //GameOver
    
	private bool isGameOver = false; // Oyunun bitip bitmediği bilgisini tutar
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, bustedRange;
    public bool playerInSightRange, playerInBustedRange = false;

    private void Awake()
    {
        playerInSightRange = false;
        playerInBustedRange = false;
        player = GameObject.Find("Player").transform;
        // GameOverUI = GameObject.Find("GameOverUI").GetComponent<Image>();
        agent = GetComponent<NavMeshAgent>();
		UpdateDestination();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInBustedRange = Physics.CheckSphere(transform.position, bustedRange, whatIsPlayer);

        if (PatrollingType == 1)
        {
            // if (!playerInSightRange && !playerInBustedRange) Patroling_v2();  random yere git demek aslında da
            if (!playerInSightRange && !playerInBustedRange) 
            {
                Patroling_v1();
		        UpdateDestination();
            }
        }
        else
        {
            if (!playerInSightRange && !playerInBustedRange) Patroling_v1();
        }

        if (playerInSightRange && !playerInBustedRange) 
        {
            ChasePlayer();
            IterateWaypointIndex();
        }
        
        if (playerInBustedRange && playerInSightRange) GameOver();
        // if (playerInBustedRange && playerInSightRange) AttackPlayer();


    }



    private void Patroling_v1()
    {
		// Debug.Log("Patrolling yapılıyor");        
		// Debug.Log("Agent Konum : " + transform.position + "Target Konum : " + target);
        if (Vector3.Distance(transform.position, target) < 2)
		{
		//  Debug.Log("mesafe farkı birden kücük");
			IterateWaypointIndex();
			UpdateDestination();
		}
    }

    private void Patroling_v2()
    {
        // Debug.Log("Agent Konum : " + transform.position + "Target Konum : " + walkPoint);
        if (!walkPointSet) 
		{
			SearchWalkPoint();
			Debug.Log("walkPointSet True");
		}

        if (walkPointSet)
		{
            agent.SetDestination(walkPoint);
			Debug.Log("SetDestination True");
		}

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
		{
            walkPointSet = false;
			Debug.Log("Walkpoint reached!");
		}
    }

   
    // References Functions
	void UpdateDestination()
	{
        
		Debug.Log("UpdateDestination");
		Debug.Log(target);
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
            Debug.Log("walkPointSet = true");
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
            Debug.Log("Yakalandın! Oyun bitti!");
        }
        //SceneManager.LoadScene("MenuScene");
        //GameOverUI.SetActive(true);
        // GameOverUI.enabled = true;
        GameOverScreen.Setup(maxTime);

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bustedRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
