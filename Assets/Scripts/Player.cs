using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // Player
    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 lastMoveDirection;

    // Animacao
    Animator anim;
    private bool facingLeft = true;

    // Cenas
    Scene scene;

    // Memoria
    public Memoria memoria;

    // Vida
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Moeda
    private int coin;
    public TextMeshProUGUI coinTXT;

    // Upgrade na casa
    private Transform marcenaria;
    public GameObject upgradeCasa;
    public GameObject maxCasa;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        scene = SceneManager.GetActiveScene();

        anim = GetComponent<Animator>();

        marcenaria = GameObject.FindWithTag("marcenaria").transform;
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

        BackToVillage();

        HealthCheck();

        coinTXT.text = memoria.coin.ToString(); 

        CasaUpgrade();
    }


    private void FixedUpdate()
    {
        rb.velocity = input * memoria.playerSpeed;
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


    private void BackToVillage() {
        if (Input.GetKeyDown(KeyCode.B) && scene.name != "QuartoA" && scene.name != "QuartoE")
        {
            memoria.profundo = 1;
            SceneManager.LoadScene(1);
        }
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

        else if(other.gameObject.CompareTag("eskel_bullet"))  {
            memoria.playerLife-=1;
            Destroy(other.gameObject);   
        }

        else if(other.gameObject.CompareTag("coin"))  {
            memoria.coin+=1;
            Destroy(other.gameObject); 
        }

        else if(other.gameObject.CompareTag("portal"))  {
            memoria.profundo = 1;
            SceneManager.LoadScene(1);
        }

        else if(other.gameObject.CompareTag("purpleFire"))  {
            memoria.playerLife-=1;
            Destroy(other.gameObject);   
        }
    }


    private void HealthCheck() {
        if (memoria.playerLife <= 0) {
            memoria.playerLife = 0;
        } else if (memoria.playerLife >= memoria.playerNumOfHearts) {
            memoria.playerLife = memoria.playerNumOfHearts;
        }

        if (memoria.playerLife > memoria.playerNumOfHearts) {
            memoria.playerLife = memoria.playerNumOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++) {
            if (i < memoria.playerLife
            ) {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }

            if (i < memoria.playerNumOfHearts) {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }
    }


    private void CasaUpgrade(){
        if (memoria.casaNivel <= 1) {
            memoria.casaNivel = 1;
        }
        if (Vector2.Distance(marcenaria.position, transform.position) <= 3f){
            if (memoria.casaNivel == 3) {
                maxCasa.SetActive(true);
                upgradeCasa.SetActive(false);
            }
            else {
                upgradeCasa.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E) && memoria.coin>=60){
                if (memoria.casaNivel == 1) {
                    memoria.casaNivel = 2;
                    memoria.coin -= 60;
                } else if (memoria.casaNivel == 2) {
                    memoria.casaNivel = 3;
                    memoria.coin -= 60;
                }
            }
        }
        else {
            upgradeCasa.SetActive(false);
            maxCasa.SetActive(false);
        }
    }

}
