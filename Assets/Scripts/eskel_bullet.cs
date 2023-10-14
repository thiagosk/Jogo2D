using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eskel_bullet : MonoBehaviour
{
    private float speed = 2f;

    // private float lifeTime = 10f;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Destroy(gameObject, lifeTime);        
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    // void OnCollisionEnter2D(Collision2D other) {
    //     if(other.gameObject.CompareTag("Player"))  {
    //         Destroy(gameObject);   
    //     }
    // }


}
