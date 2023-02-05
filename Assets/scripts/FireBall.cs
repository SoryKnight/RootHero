using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject damageEffect;
    public int damageAmount = 40;
    public float ttl = 3.0f;

    public void Update()
    {
        if(ttl <= 0)
        {
            Destroy(gameObject, 3);
        }
        ttl -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            other.GetComponent<EnemyController>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
        if(other.tag == "Player")
        {
            //Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            other.GetComponent<PlayerController>().TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
