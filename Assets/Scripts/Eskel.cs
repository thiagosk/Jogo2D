using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eskel : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeed = 8f;

    private Transform target;

    Vector2 moveDirection;

    Animator animator;

    public Memoria memoria;

    private float distanceToShoot = 6f;
    private float distanceToStop = 6f;

    private float fireRate = 2f;
    private float timeToFire;

    public Transform firingPoint;

    public GameObject bullet;

    private float rotateSpeed = 0.05f;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            RotateTowardsTarget();
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }

        if ((target != null) && (Vector2.Distance(target.position, transform.position) <= distanceToShoot)) {
            Shoot();
        }

    }


    private void FixedUpdate()
    {
        if ((target != null) && (Vector2.Distance(target.position, transform.position) >= distanceToStop)) {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        } else {
            rb.velocity = Vector2.zero;
        }
    }


    private void RotateTowardsTarget() {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }


    private void Shoot() {
        if (timeToFire <= 0f) {
            SoundManagerScript.PlaySound("EskelAttack");
            animator.SetTrigger("hit");
            timeToFire = fireRate;
            Instantiate(bullet, transform.position, transform.rotation);
        }
        else {
            timeToFire -= Time.deltaTime;
        }
    }
}
