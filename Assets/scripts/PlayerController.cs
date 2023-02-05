using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public int attackDamage = 25;
    public int maxHealth = 200;
    private int health;
    public float speed = 8;
    public float jumpForce = 6;
    public float gravity = -10;
    public bool ableToMakeADoubleJump = true;
    public float checkerY = -1.25f;

    public Animator animator;
    public Transform model;

    public static string trait = "None";
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Take the horizontal input to move the player
        float hInput = Input.GetAxis("Horizontal") * (trait == "Dyslexia" ? -1 : 1);
        direction.x = hInput * speed;
        animator.SetFloat("speed", Mathf.Abs(hInput));

        //Check if the player is on the ground
        bool grounded = controller.isGrounded;
        animator.SetBool("isGrounded", grounded);
        if(grounded)
        {
            direction.y = -1;
            ableToMakeADoubleJump = true;
            if(Input.GetButton("Jump"))
            {
                if(!(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetAnimatorTransitionInfo(0).IsName("Idle -> Attack")))
                    Jump();
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(!(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetAnimatorTransitionInfo(0).IsName("Idle -> Attack")))
                {
                    animator.SetTrigger("Attack");
                    Attack();
                }
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("DEBUG KEY!");
                Debug.Log(model.forward);
                Vector3 point = transform.position + model.forward + new Vector3(0.0f, checkerY, 0.0f);
                Debug.Log(point);
                Collider[] intersecting = Physics.OverlapSphere(point, 0.05f);
                Debug.Log(intersecting.Length);
                Debug.Log("LINECAST!");
                Vector3 end = transform.position + model.forward * 2;
                Debug.Log(Physics.Linecast(transform.position, end));
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("HEALTH KEY!");
                health = 200;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Turn UP!");
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
                model.rotation = newRotation;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Turn DOWN!");
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
                model.rotation = newRotation;
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

        //Flip the player
        if(hInput != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
            model.rotation = newRotation;
        }

        //Move the player using the character controller
        controller.Move(direction * Time.deltaTime);

        //Reset Z Position
        if (transform.position.z != -1.5f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.5f);

    }

    private void Attack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if(Vector3.Distance(enemy.transform.position, transform.position) < 2)
            {
                Debug.Log("Enemy in range");
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
        }
    }
    private void DoubleJump()
    {
        //Double Jump
        animator.SetTrigger("Jump");
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
    }
    private void Jump()
    {
        //Jump
        animator.SetTrigger("Jump");
        direction.y = jumpForce;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);

        if(health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //play a die animation
        animator.SetTrigger("Die");

        //disable the script and the collider
        GetComponent<CharacterController>().enabled = false;
        Destroy(gameObject, 3);
        this.enabled = false;
    }

}
