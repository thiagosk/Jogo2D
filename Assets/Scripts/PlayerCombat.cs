using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is only used to animate the attack because i wasn't able to do it in the Player.cs
public class PlayerCombat : MonoBehaviour
{
    public GameObject Melee;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;
    public Animator anim;
    public float attackTime = 0.3f;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public Memoria memoria;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play Animation
        anim.SetTrigger("Attack");
        if(!isAttacking){
            Melee.SetActive(true);
            isAttacking=true;
        }
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Melee.transform.position, attackRange, enemyLayers);
    
        foreach(Collider2D Enemy in hitEnemies){
            Debug.Log("We hit "+ Enemy.name);
            //Arrumar isso depois para inimigo especifico
            if(Enemy.name == "GrandMasterWarlock"){
                Enemy.GetComponent<GrandmasterWarlock>().TakeDamage(memoria.attackDamage);
            }
        } 
    }

    void CheckMeleeTimer(){
        if(isAttacking){
            atkTimer+= Time.deltaTime;
            if(atkTimer >= atkDuration){
                atkTimer=0;
                isAttacking=false;
                Melee.SetActive(false);
            }
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(Melee.transform.position,attackRange);
    }
}
