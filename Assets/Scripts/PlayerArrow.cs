using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision){
        //Arrumar isso depois para inimigo especifico
        if(collision.name == "GrandMasterWarlock" || collision.name == "GrandMasterWarlock(Clone)"){
            collision.GetComponent<GrandmasterWarlock>().TakeDamage(damage);
            Destroy(gameObject);  
        }
        else if(collision.name == "Blob" || collision.name == "Blob(Clone)"){
            collision.GetComponent<Blob>().TakeDamage(damage);
            Destroy(gameObject);  
        }
        else if(collision.name == "Eskel" || collision.name == "Eskel(Clone)"){
            collision.GetComponent<Eskel>().TakeDamage(damage);
            Destroy(gameObject);  
        }
        else if(collision.name == "Adept" || collision.name == "Adept(Clone)"){
            collision.GetComponent<Adept>().TakeDamage(damage);
            Destroy(gameObject); 
        }else if(collision.gameObject.CompareTag("wall"))  {
            Destroy(gameObject);   
        }
    }
}
