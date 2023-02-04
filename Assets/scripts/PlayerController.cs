using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;
    public float jumpForce = 6;
    public float gravity = -10;
    public bool ableToMakeADoubleJump = true;

    public Animator animator;
    public Transform model;

    public static string trait = "None";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Take the horizontal input to move the player
        float hInput = Input.GetAxis("Horizontal") * (trait == "Dyslexia" ? -1 : 1);
        direction.x = hInput * speed;
        //animator.SetFloat("speed", Mathf.Abs(hInput));

        //Check if the player is on the ground
        bool grounded = controller.isGrounded;
        //animator.SetBool("isGrounded", grounded);
        if (grounded)
        {
            direction.y = -1;
            ableToMakeADoubleJump = true;
            if (Input.GetButton("Jump"))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                //animator.SetTrigger("fireBallAttack");
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("DEBUG KEY!");
                Debug.Log(model.forward);
                Vector3 point = transform.position + model.forward + new Vector3(0.0f, -1.25f, 0.0f);
                Debug.Log(point);
                Collider[] intersecting = Physics.OverlapSphere(point, 0.05f);
                Debug.Log(intersecting.Length);
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;//Add Gravity
            if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }
        }

        /*if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Attack"))
            return;*/

        //Flip the player
        if(hInput != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
            model.rotation = newRotation;
        }

        //Move the player using the character controller
        controller.Move(direction * Time.deltaTime);

        //Reset Z Position
        if (transform.position.z != -1f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);

    }

    private void DoubleJump()
    {
        //Double Jump
        //animator.SetTrigger("doubleJump");
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
    }
    private void Jump()
    {
        //Jump
        direction.y = jumpForce;
    }

}
