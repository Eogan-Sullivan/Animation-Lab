using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruceControl : MonoBehaviour {


        Animator anim;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
        {
            anim = GetComponent<Animator>();
        anim.SetBool("Running", true);

    }


        void Update()
        {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            
              
            
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                anim.SetFloat("Jump",jumpSpeed);
                moveDirection.y = jumpSpeed;
                
            }
            if (Input.GetKeyDown("up"))
            {
                anim.SetBool("Running", false);
                anim.SetFloat("Blend", 0.5f);
            }
            else
            {
                anim.SetBool("Running", true);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    }

