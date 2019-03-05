using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float runSpeed = 40f;

    private CharacterController2D controller;
    private Animator animator;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool attacking = false;
    private float attackCooldown = 0.2f;
    private float attackTimer = 0f;

    public Collider2D attackTrigger;

    private void Start() {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1") && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;
            attackTrigger.enabled = true;
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                //count down attack animation
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }

        animator.SetFloat("Speed", controller.speed);
        animator.SetBool("IsJumping", !controller.isGrounded);
        animator.SetBool("IsAttacking", attacking);
    }

    void FixedUpdate() {
        // move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
