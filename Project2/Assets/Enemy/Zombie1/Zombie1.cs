using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : MonoBehaviour
{
    [SerializeField] private float hp = 100f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private int damage = 10;

    private bool deliveredHit;
    private bool playerDiscovered;
    private float distanceToPlayer;

    private float maxHitTimer = 2f;
    private float currentHitTimer = 0f;
    private float attackCooldown = 2f;
    private float cooldownTimer = 0f;
    private int currentControlPointIndex = 0;

    private bool hit = false;

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
        //take damage and aggro player
        playerDiscovered = true;
        hp -= damage;
        Debug.Log(hp);

        //start stun timer
        currentHitTimer = maxHitTimer;

        if (hp <= 0)
        {
            animator.SetBool("Dead", true);
            deadCheck.dead = true;
            this.enabled = false;
            //Destroy(gameObject);
        }
    }
    void Attack()
    {
        //begin attack anim
        deliveredHit = false;
        animator.SetBool("Attacking", true);
    }

    //move to next point
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

        //attack if in range and not cooling down
        if (distanceToPlayer <= 2.5f && cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = attackCooldown;
        }
        //deal damage to player within certain frames of animation
        else if (distanceToPlayer <= 2f && cooldownTimer > 0.75f && cooldownTimer <= 1f && !deliveredHit)
        {
            deliveredHit = true;
            player.transform.GetComponent<PlayerHealth>().ReceiveHit(damage);
            cooldownTimer -= Time.deltaTime;
        }
        //decrement cooldown
        else if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        } else
        {
            animator.SetBool("Attacking", false);
        }

        if (distanceToPlayer <= 20.0f)
        {
            playerDiscovered = true;
        }

        //stun zombie if recently hit
        if (currentHitTimer > 0f)
        {
            hit = true;
            currentHitTimer -= Time.deltaTime;
            enemyNav.destination = gameObject.transform.position;
        } else
        {
            hit = false;
        }

        //move to player
        if (playerDiscovered && hit == false && cooldownTimer <=0f)
        {
            enemyNav.destination = player.transform.position;
        } else if (!playerDiscovered)
        {
            if (!enemyNav.pathPending && enemyNav.remainingDistance < 0.5f)
            {
                MoveToNextPatrolPoint();
            }
        }

        animator.SetBool("Hit", hit);
    }
}
