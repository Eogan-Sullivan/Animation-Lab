using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BruceControl : NetworkBehaviour
{


    Animator anim;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float runleft = 0.0f, runright = 0.0f, timeInAir = 0f, deathTimer = 10f;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject bulletsprefab;
    public float shotforce = 100000f;



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
           

            if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
            {
                anim.SetBool("Running", true);

            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                CmdFireBullet();
            }

            if(Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("WalkBackwards",true);
                anim.SetBool("Idle", false);

            }

            else
            {
                anim.SetBool("WalkBackwards", false);
                anim.SetBool("Idle", true);
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
               
                    if (runright != 1.0f)
                    {
                        runright += 0.2f;
                        anim.SetFloat("Blend", runright);
                    }

                    else
                        anim.SetFloat("Blend", runright);
                
                
            }

            if(Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(transform.position, Vector3.up, 60 * Time.deltaTime);
            }

            else if ((Input.GetKeyUp(KeyCode.D)))
            {
                anim.SetFloat("Blend", 0.5f);
            }


            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetFloat("Blend", runleft);
               

            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(transform.position, Vector3.up, -60 * Time.deltaTime);
            }
            else if ((Input.GetKeyUp(KeyCode.A)))
            {
                anim.SetFloat("Blend", 0.5f);

            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("Kick", true);
                anim.SetBool("Idle", false);

            }

            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("Kick", false);
                anim.SetBool("Idle", true);
            }



        }



    }




    
       [Command] private void CmdFireBullet()
    {
        
       
            GameObject bullets = Instantiate(bulletsprefab, transform.position, Quaternion.Euler(90, 0, 0));
            bullets.GetComponent<Rigidbody>().AddForce(transform.forward * shotforce);
            NetworkServer.Spawn(bullets);
            Destroy(bullets,.9f);
          
       
    }
    public void FootR() { }

    public void FootL() { }


      void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("isHit", true);
    }
}

