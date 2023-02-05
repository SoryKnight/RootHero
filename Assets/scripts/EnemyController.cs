using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private string currentState = "IdleState";
    private Transform target;
    private CharacterController controller;
    private Vector3 direction;
    private int health;

    public float chaseRange = 5;
    public float attackRange = 2;
    public float speed = 1.5f;
    public float chaseSpeed = 2.5f;
    private float checkerY = -1f;

    public int attackDamage = 25;
    public bool ranged = false;
    public int maxHealth;
    public bool patroller = false;
    public int patrolLength = 4;
    public float cooldown = 2f;
    private float currCooldown = 0f;
    private Vector3 patrolPointMax;
    private Vector3 patrolPointMin;

    public Animator animator;
    public Transform model;

    public GameObject fireBall;
    public Transform shootPoint;
    public float fireBallSpeed = 600;
    public bool staticEnemy = false;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        patrolPointMax = transform.position + new Vector3(patrolLength / 2.0f, 0.0f, 0.0f);
        patrolPointMin = transform.position - new Vector3(patrolLength / 2.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, target.position);

        if(currentState == "IdleState")
        {
            if(!patroller)
            {
                //animator.SetBool("chase", false);
                if (distance < chaseRange)
                    currentState = "ChaseState";
            }
            else
            {
                //patrolPointMax = transform.position + new Vector3(patrolLength / 2.0f, 0.0f, 0.0f);
                //patrolPointMin = transform.position - new Vector3(patrolLength / 2.0f, 0.0f, 0.0f);
                currentState = "PatrolState";
            }
        }
        if(currentState == "PatrolState")
        {
            if (distance < chaseRange)
                currentState = "ChaseState";
            else
                Patrol();
        }
        if(currentState == "ChaseState")
        {
            float diff = target.position.x - transform.position.x;
            //Chase animation, if needed
            //animator.SetBool("chase", Mathf.Abs(diff) > 0.15f);
            if(Mathf.Abs(diff) > 0.15f)
            {

                if(distance < attackRange)
                    currentState = "AttackState";
                else if(distance > chaseRange * 2)
                    currentState = "IdleState";

                if(!Move(diff / Mathf.Abs(diff)))
                    currentState = "IdleState";
            }
        }
        if(currentState == "AttackState")
        {
            if(currCooldown <= 0f)
            {
                currCooldown = cooldown;
                animator.SetTrigger("Attack");
                Attack();
            }
            else
            {
                currCooldown -= Time.deltaTime;
            }
            if (distance > attackRange)
                currentState = "ChaseState";
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        currentState = "ChaseState";

        if(health < 0)
        {
            Die();
        }
    }

    private bool Move(float amount)
    {
        if(staticEnemy)
            return true;
        if(EdgeDetector())
        {
            if(model.forward.x * amount > 0)
                return false;
        }
        animator.SetFloat("speed", Mathf.Abs(amount));
        float bonusSpeed = currentState == "ChaseState" ? chaseSpeed : 1;
        //Move the creature using the character controller
        direction.x = amount * speed * bonusSpeed;
        //Flip the enemy
        if(amount != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(amount, 0, 0));
            model.rotation = newRotation;
        }
        controller.Move(direction * Time.deltaTime);
        return true;
    }

    private bool EdgeDetector()
    {
        Vector3 point = transform.position + model.forward + new Vector3(0.0f, checkerY, 0.0f);
        foreach (Collider col in Physics.OverlapSphere(point, 0.05f))
        {
            if(!col.isTrigger)
                return false;
        }
        return true;
    }

    private void Patrol()
    {
        float dir = model.forward.x;
        if((dir > 0 && transform.position.x - patrolPointMax.x > 0) || (dir < 0 && transform.position.x - patrolPointMin.x < 0))
            dir *= -1;
        if(!Move(dir))
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(-dir, 0, 0));
            model.rotation = newRotation;
        }
    }

    private void Attack()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < attackRange)
            {
                if(ranged)
                {
                    Debug.Log("Shooting");
                    FireBallAttack(player.transform.position);
                }
                else
                {
                    player.GetComponent<PlayerController>().TakeDamage(attackDamage);
                }
            }
        }
    }

    public void FireBallAttack(Vector3 target)
    {
        GameObject ball  = Instantiate(fireBall, shootPoint.position, Quaternion.identity);
        Vector3 f = shootPoint.position;
        Vector3 vector = new Vector3(target.x - f.x, target.y + 2 - f.y, target.z - f.z);
        Debug.Log(f);
        Debug.Log(target);
        Debug.Log(vector);
        ball.GetComponent<Rigidbody>().AddForce( vector * fireBallSpeed);

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
