using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy AI Settings")] 
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Patrol Settings")] 
    public Vector3 walkPoint;
    bool _walkPointSet;
    public float walkPointRange;

    [Header("Attack Settings")] 
    public float timeBetweenAttacks;
    bool _alreadyAttacked;
    [Tooltip("The projectile being shot out of the weapon.")]
    public GameObject projectile;
    [Tooltip("The point where the projectile is shot out of.")]
    public Transform attackPoint;
    
    [Header("Health Settings")]
    public HealthManager healthManager;

    [Header("States")] 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var position = transform.position;
        playerInSightRange = Physics.CheckSphere(position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(position, attackRange, whatIsPlayer);
        
        if (!playerInSightRange && !playerInAttackRange)
        { Patrolling(); }
        
        if (playerInSightRange && !playerInAttackRange)
        { ChasePlayer(); }
        
        if (playerInSightRange && playerInAttackRange)
        { AttackPlayer(); }
    }
    
    /// <summary>
    /// This method is used to make the enemy patrol around randomly.
    /// </summary>
    private void Patrolling()
    {
        if (!_walkPointSet)
        { SearchWalkPoint(); }

        if (_walkPointSet)
        { agent.SetDestination(walkPoint); }

        Vector3 distToWalkPoint = transform.position - walkPoint;

        if (distToWalkPoint.magnitude < 1f)
        { _walkPointSet = false; }
    }
    
    /// <summary>
    /// This method is used to make the enemy search for a random walk point.
    /// </summary>
    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            _walkPointSet = true;
        }
    }
    
    /// <summary>
    /// This method is used to make the enemy chase the player.
    /// </summary>
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }
    
    /// <summary>
    /// This method is used to make the enemy attack the player.
    /// </summary>
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!_alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    /// <summary>
    /// This method is used to reset the attack.
    /// </summary>
    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }
    
    // Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnDrawGizmos()
    {
        if (playerInSightRange)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, player.position);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(walkPoint, transform.position);
        }
    }
}