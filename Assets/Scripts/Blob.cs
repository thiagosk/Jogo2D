using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeed = 10f;

    private Transform target;

    Vector2 moveDirection;

    Animator animator;

    public Memoria memoria;

    public int maxHealth = 2;

    int currentHealth;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

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
        }

    }


    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }


    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))  {
            SoundManagerScript.PlaySound("BlobAttack");
            SoundManagerScript.PlaySound("PlayerHit");
            animator.SetTrigger("hit");
            memoria.playerLife-=1;
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
        Destroy(gameObject);
        memoria.numEnemies=-1;
    }
}
