using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Rigidbody2D rb;

    Animator animator;

    public GameObject coin;

    public Memoria memoria;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }


    private void Dissappear(){
        gameObject.SetActive(false);
    }
    

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("Pot");
            animator.SetTrigger("break");
            Instantiate(coin, transform.position, transform.rotation);
            Invoke("Dissappear", 0.5f);
        }
    }
}
