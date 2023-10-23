using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is only used to animate the attack because i wasn't able to do it in the Player.cs
public class PlayerCombat : MonoBehaviour
{
    // Memoria
    public Memoria memoria;

    // Melee
    public GameObject Melee;
    public GameObject MeleeFire;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;
    public float attackRange = 0.5f;

    // Ranged
    public GameObject arrow;
    public Transform aim;
    float shootCoolDown = 0.25f;
    float shootTimer = 0.5f;

    // Animation
    public Animator anim;
    
    // Enemies
    public LayerMask enemyLayers;
    Collider2D[] hitEnemies;

    // Update is called once per frame
    void Start(){
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "UpAttack":
                    atkDuration = clip.length;
                    print(atkDuration);
                    break;
            }
        }
    }
    void Update()
    {
        CheckMeleeTimer();
        shootTimer+= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.K) && memoria.hasBow)
        {
            shoot();
        }
    }

    void Attack()
    {
        // Play Animation
        anim.SetTrigger("Attack");
        if(!isAttacking){
            if(memoria.armaNivel==1||memoria.armaNivel==3){
                MeleeFire.SetActive(true);
            }else{
                Melee.SetActive(true);
            }
            isAttacking=true;
        }

        if(memoria.armaNivel==1||memoria.armaNivel==3){
            hitEnemies = Physics2D.OverlapCircleAll(MeleeFire.transform.position, attackRange, enemyLayers);
        }else{
            hitEnemies = Physics2D.OverlapCircleAll(Melee.transform.position, attackRange, enemyLayers);
        }
    
        foreach(Collider2D Enemy in hitEnemies){
            Debug.Log("We hit "+ Enemy.name);
            //Arrumar isso depois para inimigo especifico
            if(Enemy.name == "GrandMasterWarlock" || Enemy.name == "GrandMasterWarlock(Clone)"){
                Enemy.GetComponent<GrandmasterWarlock>().TakeDamage(memoria.attackDamage);
            }
            else if(Enemy.name == "Blob(Clone)" || Enemy.name == "Blob"){
                Enemy.GetComponent<Blob>().TakeDamage(memoria.attackDamage);
            }
            else if(Enemy.name == "Eskel(Clone)" || Enemy.name == "Eskel"){
                Enemy.GetComponent<Eskel>().TakeDamage(memoria.attackDamage);
            }
            else if(Enemy.name == "Adept(Clone)" || Enemy.name == "Adept"){
                Enemy.GetComponent<Adept>().TakeDamage(memoria.attackDamage);
            }
        } 
    }

    void shoot(){
        // Cria instancia de objeto arrow que contem script PlayerArrow.cs
        // Dentro desse arquivo lida com colisao
        if(shootTimer > shootCoolDown)
        {
            shootTimer = 0;
            GameObject intArrow = Instantiate(arrow,aim.position,aim.rotation);
            intArrow.GetComponent<Rigidbody2D>().AddForce(-aim.up*10f, ForceMode2D.Impulse);
        }
    }

    void CheckMeleeTimer(){
        if(isAttacking){
            atkTimer+= Time.deltaTime;
            if(atkTimer >= atkDuration){
                atkTimer=0;
                isAttacking=false;
                if(memoria.armaNivel==1 || memoria.armaNivel==3){
                    MeleeFire.SetActive(false);
                }else{
                    Melee.SetActive(false);
                }
            }
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(Melee.transform.position,attackRange);
    }
}
