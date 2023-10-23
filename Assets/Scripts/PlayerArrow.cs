using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision){
        //Arrumar isso depois para inimigo especifico
        if(collision.name == "GrandMasterWarlock"){
            collision.GetComponent<GrandmasterWarlock>().TakeDamage(damage);
        }
        else if(collision.name == "Blob"){
            collision.GetComponent<Blob>().TakeDamage(damage);
        }
        else if(collision.name == "Eskel"){
            collision.GetComponent<Eskel>().TakeDamage(damage);
        }
        else if(collision.name == "Adept"){
            collision.GetComponent<Adept>().TakeDamage(damage);
        }
    }
}
