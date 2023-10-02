using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 input;

    Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;

    Scene scene;
    public Memoria memoria;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scene = SceneManager.GetActiveScene();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
        if (input.x < 0 && !facingLeft || input.x > 0 && facingLeft)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.B) && scene.name != "QuartoA" && scene.name != "QuartoE")
        {
            memoria.profundo = 1;
            SceneManager.LoadScene(1);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = input * speed;
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }

    void Animate(){
        anim.SetFloat("MoveX",input.x);
        anim.SetFloat("MoveY",input.y);
        anim.SetFloat("MoveMagnitude",input.magnitude);
        anim.SetFloat("LastMoveX",lastMoveDirection.x);
        anim.SetFloat("LastMoveY",lastMoveDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Buraco")) 
        {
            if (scene.name == "QuartoA")
            {
                SceneManager.LoadScene(Random.Range(2, 5));
            }
            else if (scene.name == "QuartoB" || scene.name == "QuartoC" || scene.name == "QuartoD")
            {
                memoria.profundo = memoria.profundo+1;
                if (memoria.profundo >= 4)
                {
                    SceneManager.LoadScene(5);
                }
                else 
                {
                    SceneManager.LoadScene(Random.Range(2, 5));
                }
            }
            else if (scene.name == "QuartoE")
            {
                memoria.profundo = 1;
                SceneManager.LoadScene(1);
            }
        }
    }
}
