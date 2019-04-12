using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletor : MonoBehaviour
{
    [SerializeField] private float hp = 900f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int damage = 50;

    private bool deliveredHit;
    private bool playerDiscovered;
    private float distanceToPlayer;
    private float attackCooldown = 2.5f;
    private float cooldownTimer = 0f;
    private int currentControlPointIndex = 0;

    public Animator animator;
    UnityEngine.AI.NavMeshAgent enemyNav;
    GameObject player;
    DeadCheck deadCheck;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyNav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        deadCheck = GetComponent<DeadCheck>();
    }
    void ReceiveHit(float damage)
    {
        playerDiscovered = true;
        hp -= damage;
        Debug.Log(hp);

        if (hp <= 0)
        {
            animator.SetBool("Dead", true);
            deadCheck.dead = true;
            enemyNav.destination = gameObject.transform.position;
            this.enabled = false;
            //Destroy(gameObject);
        }
    }

    void Attack()
    {
        deliveredHit = false;
        animator.SetBool("Attacking", true);
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length > 0)
        {
            enemyNav.destination = patrolPoints[currentControlPointIndex].position;

            currentControlPointIndex++;
            currentControlPointIndex %= patrolPoints.Length;
        }
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
        animator.SetFloat("MoveSpeed", enemyNav.velocity.magnitude);

        if (distanceToPlayer <= 3f && cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = attackCooldown;
        }
        else if (distanceToPlayer <= 2.5f && cooldownTimer > 1f && cooldownTimer <= 1.75f && !deliveredHit)
        {
            deliveredHit = true;
            player.transform.GetComponent<PlayerHealth>().ReceiveHit(damage);
            cooldownTimer -= Time.deltaTime;
        }
        else if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Attacking", false);
        }

        if (distanceToPlayer <= 20.0f)
        {
            playerDiscovered = true;
        }

        if (playerDiscovered && cooldownTimer <= 0f)
        {
            enemyNav.destination = player.transform.position;
        }
        else if (!playerDiscovered)
        {
            if (!enemyNav.pathPending && enemyNav.remainingDistance < 0.5f)
            {
                MoveToNextPatrolPoint();
            }
        }
    }
}
