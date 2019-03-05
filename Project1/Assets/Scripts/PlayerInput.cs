using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float runSpeed = 40f;

    private CharacterController2D controller;
    private Animator animator;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    private void Start() {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        animator.SetFloat("Speed", controller.speed);
        animator.SetBool("IsJumping", !controller.isGrounded);
    }

    void FixedUpdate() {
        // move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
