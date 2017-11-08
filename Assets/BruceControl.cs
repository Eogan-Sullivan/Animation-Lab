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
    public float runleft = 0.0f, runright = 1.0f, timeInAir = 0f, deathTimer = 10f;
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
            CmdFireBullet();

            if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
            {
                anim.SetBool("Running", true);

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
                anim.SetFloat("Blend", runright);
            }

            else if ((Input.GetKeyUp(KeyCode.D)))
            {
                anim.SetFloat("Blend", 0.5f);
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
                anim.SetBool("Kick", true);

            }

            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("Kick", false);
            }



        }
        //rotating here
        transform.right = Vector3.Slerp(transform.right, Vector3.right * Input.GetAxis("Horizontal"), 0.1f);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


    }


    void DeathCondition()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            timeInAir = 0f;
        }
        if (!controller.isGrounded)
        {
            timeInAir += Time.deltaTime;

        }
        if (timeInAir >= deathTimer)
        {
           


        }
    }

    [Client] private void CmdFireBullet()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject bullets = Instantiate(bulletsprefab, transform.position, Quaternion.Euler(90, 0, 0));
            bullets.GetComponent<Rigidbody>().AddForce(transform.forward * shotforce);
            NetworkServer.SpawnWithClientAuthority(bullets, connectionToClient);
            Destroy(bullets,.9f);
          
        }
    }
}

