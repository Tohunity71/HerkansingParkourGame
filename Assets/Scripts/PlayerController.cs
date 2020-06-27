using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variabelen voor mijn movement
    public float moveSpeed = 3.5f;
    public float jumpHeight = 5f;
    public float vert, hor;
    private Rigidbody rbMove;
    public Rigidbody rbJump;

    private bool isGrounded;
    private int jumps;

    //Variabelen voor mijn gravity Jump
    public float jumpFallMulitplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //Variabelen voor WallRun/jump
    public float wallRunSpeed = 15f;
    public float wallRunTime = 3f;
    private int wallJumps;
    private Vector3 wallJumpNormal;
    public float wallJumpForce = 3;

    private void Awake()
    {
        rbMove = GetComponent<Rigidbody>();
        rbJump = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        vert = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");

        Vector3 moveDir = vert * transform.forward + hor * transform.right;

        rbMove.MovePosition(transform.position + moveDir * Time.deltaTime * moveSpeed);

        //Jump
        if (Input.GetButtonDown("Jump") && jumps < 2)
        {
            rbJump.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            jumps++;
        }

        //Gravity aanpassing op mijn Jump
        if (rbJump.velocity.y < 0)
        {
            rbJump.velocity += Vector3.up * Physics.gravity.y * (jumpFallMulitplier - 1) * Time.deltaTime;
        }
        else if (rbJump.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rbJump.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }       

    }





    // Jump check, Dit checked of mijn Player op de grond zit. 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "blue" || collision.gameObject.tag == "orange")
        {

            jumps = 0;
            wallJumps = 0;
            isGrounded = true;
            //wallJumpNormal = collision.contacts[0].normal;
            for (int i = 0; i < collision.contactCount; i++) wallJumpNormal = collision.contacts[i].normal;
            Debug.Log("WallJump" + wallJumpNormal);
            Debug.Log("contact" +  collision.contacts[0].normal);
           
        }

        if (!isGrounded && wallJumps < 1 && collision.gameObject.tag == "BlueWall" && Input.GetButtonDown("Jump") || !isGrounded && wallJumps < 1 && collision.gameObject.tag == "OrangeWall" && Input.GetButtonDown("Jump"))
        {
            rbJump.AddForce(new Vector3(0, 30, 0), ForceMode.Impulse);
            rbJump.AddForce(wallJumpNormal * -1 * wallJumpForce, ForceMode.Impulse);
            wallJumps++;
            Debug.Log("WallJump");
        }



        //Console tekst test
        if (collision.gameObject.tag == "BlueWall" || collision.gameObject.tag == "OrangeWall")
        {
            //wallJumpNormal = collision.contacts[0].normal;
            Debug.Log("HitwallEnter");
            
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        //Wall jump

          if (!isGrounded && wallJumps <1 && collision.gameObject.tag == "BlueWall" && Input.GetButtonDown("Jump") || !isGrounded && wallJumps < 1 && collision.gameObject.tag == "OrangeWall" && Input.GetButtonDown("Jump"))
        {
            rbJump.AddForce(new Vector3(0, 30, 0), ForceMode.Impulse);
            rbJump.AddForce(wallJumpNormal * -1 * wallJumpForce, ForceMode.Impulse);
            wallJumps++;
            Debug.Log("WallJump");
            
        }

        //Console tekst test
        if (collision.gameObject.tag == "BlueWall")
        {
            Debug.Log("HitWall");
        }
    }


        private void OnCollisionExit(Collision collision) 
    {

        //Collission with ground
        if(collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }

    }




}