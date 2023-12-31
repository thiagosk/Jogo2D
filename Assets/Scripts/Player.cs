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

    // Audio
    AudioSource audioSource;

    // Ataque
    public Transform aim;


    public GameObject portal;
    // HUD Espada,flecha,espada de fogo
    public GameObject NormalSwordHUD;
    public GameObject FireSwordHUD;
    public GameObject BowHUD;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        scene = SceneManager.GetActiveScene();

        anim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        memoria.numEnemies = 0;

    }


    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        MoveAnimate();

        if (input.x < 0 && !facingLeft || input.x > 0 && facingLeft)
        {
            Flip();
        }

        BackToVillage();

        HealthCheck();

        coinTXT.text = memoria.coin.ToString(); 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackSound();
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            AttackSound();
        }


        if (scene.name == "Vila")
        {
            memoria.playerLife = memoria.playerNumOfHearts;
            memoria.coinValue = 1;
            memoria.profundo = 1;
            memoria.bossExist = 1;
        }
        else if (scene.name == "QuartoA" || scene.name == "QuartoB" || scene.name == "QuartoC")
        {
            memoria.bossExist = 1;
        }
        else if (scene.name == "QuartoE")
        {
            memoria.coinValue = 20;
        }

        HUDWeapon();

        CoinLogic();

        PortalNumEnemy();

        if (memoria.playerLife <= 0)
        {
            SceneManager.LoadScene(6);
        }
        
    }

    private void PortalNumEnemy()
    {
        if (memoria.numEnemies <= 0 && scene.name != "Vila" && memoria.bossExist == 1)
        {
            portal.SetActive(true);
        }
        else{
            portal.SetActive(false);
        }
    }
    private void HUDWeapon(){
        if(memoria.armaNivel==0){
            NormalSwordHUD.SetActive(true);
            FireSwordHUD.SetActive(false);
            BowHUD.SetActive(false);
        }else if(memoria.armaNivel==1){
            NormalSwordHUD.SetActive(false);
            FireSwordHUD.SetActive(true);
            BowHUD.SetActive(false);
        }else if(memoria.armaNivel==2){
            NormalSwordHUD.SetActive(true);
            FireSwordHUD.SetActive(false);
            BowHUD.SetActive(true);
        }else if(memoria.armaNivel==3){
            NormalSwordHUD.SetActive(false);
            FireSwordHUD.SetActive(true);
            BowHUD.SetActive(true);
        }
    }
    private void CoinLogic()
    {
        if (memoria.profundo == 1)
        {
            memoria.coinValue = 1;
        }
        else if (memoria.profundo == 2)
        {
            memoria.coinValue = 5;
        }
        else if (memoria.profundo == 3)
        {
            memoria.coinValue = 10;
        }
    }

    void AttackSound()
    {
        SoundManagerScript.PlaySound("PlayerAttack");
    }


    private void FixedUpdate()
    {
        rb.velocity = input * memoria.playerSpeed;

        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        Vector3 vectorAim =  Vector3.left*input.x + Vector3.down*input.y;
        aim.rotation = Quaternion.LookRotation(Vector3.forward, vectorAim);
    }


    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 || moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }
        
        Vector3 vectorAim =  Vector3.left*lastMoveDirection.x + Vector3.down*lastMoveDirection.y;
        aim.rotation = Quaternion.LookRotation(Vector3.forward, vectorAim);

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();
    }


    void MoveAnimate()
    { 
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }
    

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }

    private void BackToVillage() {
        if (Input.GetKeyDown(KeyCode.B) && scene.name != "Vila" && scene.name != "QuartoE" && memoria.numEnemies <= 0)
        {
            SoundManagerScript.PlaySound("Portal");
            memoria.profundo = 1;
            SceneManager.LoadScene(1);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("portal")) 
        {
            SoundManagerScript.PlaySound("Portal");

            if (scene.name == "Vila")
            {   
                SceneManager.LoadScene(Random.Range(2, 5));
            }
            else if (scene.name == "QuartoA" || scene.name == "QuartoB" || scene.name == "QuartoC")
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
            SoundManagerScript.PlaySound("PlayerHit");
            memoria.playerLife-=1;
            Destroy(other.gameObject);   
        }

        else if(other.gameObject.CompareTag("grandmasterProjectile"))  {
            SoundManagerScript.PlaySound("PlayerHit");
            memoria.playerLife-=1;
            Destroy(other.gameObject);   
        }

        else if(other.gameObject.CompareTag("coin"))  {
            SoundManagerScript.PlaySound("Coin");
            memoria.coin+=memoria.coinValue;
            Destroy(other.gameObject); 
        }

        else if(other.gameObject.CompareTag("purpleFire"))  {
            SoundManagerScript.PlaySound("PlayerHit");
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
            if (i < memoria.playerLife) {
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

}
