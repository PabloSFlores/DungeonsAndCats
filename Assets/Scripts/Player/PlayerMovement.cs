using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    Vector2 direction;

    Rigidbody2D rigidBody;
    Animator animator;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //Asigna la velocidad al rb en funcion de la direction y velocidad
    private void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
    }

    private void Update()
    {
        Movement();
        Animations();
    }

    private void Movement()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void Animations()
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        if (direction.magnitude != 0) animator.Play("Run");
        else animator.Play("Idle");
    }
}
