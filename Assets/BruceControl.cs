using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruceControl : MonoBehaviour {


        Animator anim;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float runleft = 0.0f, runright = 1.0f;
    private Vector3 moveDirection = Vector3.zero;

    

    void Start()
        {
            anim = GetComponent<Animator>();



    }


        void Update()
        {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            
              
            
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;            
            if (Input.GetKeyDown("up")|| Input.GetKeyDown("w"))
            {
                anim.SetBool("Running",true);
             
            }

            if (Input.GetKeyUp("up") || Input.GetKeyUp("w"))
            {
                anim.SetBool("Running", false);

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetFloat("Blend",runright);
            }

            else if ((Input.GetKeyUp(KeyCode.D)))
            {
                anim.SetFloat("Blend",0.5f);
            }


            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetFloat("Blend", runleft);

            }
            else if ((Input.GetKeyUp(KeyCode.A)))
            {
                anim.SetFloat("Blend", 0.5f);

            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("Crouch", true);

            }

            else if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("Crouch", false);
            }


        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    }

