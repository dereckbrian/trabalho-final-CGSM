using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float speed = 5f;
    public float gravity = 9.8f;
    public float rotSpeed = 200f;

    private float rot;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            float moveX = Input.GetAxisRaw("Horizontal"); 
            float moveZ = Input.GetAxisRaw("Vertical");   

            moveDirection = new Vector3(moveX, 0, moveZ).normalized * speed;

            // Ajusta a animação baseado no movimento
            if (moveDirection.magnitude > 0)
            {
                animator.SetInteger("transition", 1);
            }
            else
            {
                animator.SetInteger("transition", 0);
            }

            // Faz o personagem olhar para a direção do movimento
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
            }
        }

        // Aplica gravidade
        moveDirection.y -= gravity * Time.deltaTime;

        // Move o personagem
        controller.Move(moveDirection * Time.deltaTime);
    }
}
