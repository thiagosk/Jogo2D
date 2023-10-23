using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adept : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeed = 8f;

    private Transform target;

    Vector2 moveDirection;

    public bool facingRight = false;

    public GameObject purpleFire;
    private float fireRate = 1f;
    private float timeToFire;

    public int maxHealth = 6;
    
    int currentHealth;

    public Memoria memoria;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindWithTag("Player").transform;

        currentHealth = maxHealth;

        memoria.numEnemies+=1;

    }


    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            
            CreateFire();
        }

        if (target.transform.position.x < transform.position.x && !facingRight){

        Flip ();
        }
        if (target.transform.position.x > transform.position.x && facingRight){
        Flip ();

        }

    }


    private void Flip(){
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }


    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }


    private void CreateFire() {
        if (timeToFire <= 0f) {
            timeToFire = fireRate;
            Instantiate(purpleFire, transform.position, transform.rotation);
        }
        else {
            timeToFire -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy died");
        memoria.numEnemies-=1;
        Destroy(gameObject);
    }

}
