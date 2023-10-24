using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LojaScript : MonoBehaviour
{
    private Transform player;

    public GameObject buySwordandBow;
    public Memoria memoria;

    private GameObject fireSwordandBow;

    private GameObject onlybow;
    private GameObject onlySword;
    private GameObject nothing;

    public int swordPrice = 60;
    public int bowPrice = 80;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        fireSwordandBow = buySwordandBow.transform.GetChild(0).gameObject;
        onlybow = buySwordandBow.transform.GetChild(1).gameObject;
        onlySword = buySwordandBow.transform.GetChild(2).gameObject;
        nothing = buySwordandBow.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        armaUpgrade();
    }

    private void armaUpgrade(){
        if (memoria.armaNivel <= 0){
            memoria.armaNivel = 0;
            memoria.attackDamage = 1;
            memoria.hasBow = false;
        }
        if(Vector2.Distance(player.position,transform.position) <= 3){
            buySwordandBow.SetActive(true);
            if(memoria.armaNivel == 0){
                fireSwordandBow.SetActive(true);
                onlybow.SetActive(false);
                onlySword.SetActive(false);
                nothing.SetActive(false);
            }else if(memoria.armaNivel == 1){
                fireSwordandBow.SetActive(false);
                onlybow.SetActive(true);
                onlySword.SetActive(false);
                nothing.SetActive(false);
            }else if(memoria.armaNivel == 2){
                fireSwordandBow.SetActive(false);
                onlybow.SetActive(false);
                onlySword.SetActive(true);
                nothing.SetActive(false);
            }else if(memoria.armaNivel == 3){
                fireSwordandBow.SetActive(false);
                onlybow.SetActive(false);
                onlySword.SetActive(false);
                nothing.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E) && memoria.coin>=swordPrice && memoria.armaNivel < 3){ // Compra de Espada 
                if (memoria.armaNivel == 0) {
                    SoundManagerScript.PlaySound("Interaction");
                    memoria.armaNivel = 1;
                    memoria.attackDamage = 2;
                    memoria.coin -= swordPrice;
                } else if (memoria.armaNivel == 2) {
                    SoundManagerScript.PlaySound("Interaction");
                    memoria.armaNivel = 3;
                    memoria.attackDamage = 2;
                    memoria.coin -= swordPrice;
                } else if(memoria.armaNivel == 1){
                    SoundManagerScript.PlaySound("No");
                }
            } else if(Input.GetKeyDown(KeyCode.R) && memoria.coin>=bowPrice && memoria.armaNivel < 3){ //Compra de arco
                if (memoria.armaNivel == 0) {
                    SoundManagerScript.PlaySound("Interaction");
                    memoria.armaNivel = 2;
                    memoria.hasBow = true;
                    memoria.coin -= bowPrice;
                } else if (memoria.armaNivel == 1) {
                    SoundManagerScript.PlaySound("Interaction");
                    memoria.armaNivel = 3;
                    memoria.hasBow = true;
                    memoria.coin -= bowPrice;
                } else if(memoria.armaNivel == 2){
                    SoundManagerScript.PlaySound("No");
                }
            } else if ((Input.GetKeyDown(KeyCode.E) && memoria.coin<swordPrice) || (Input.GetKeyDown(KeyCode.E) && memoria.armaNivel==3) || (Input.GetKeyDown(KeyCode.R) && memoria.coin<bowPrice) || (Input.GetKeyDown(KeyCode.R) && memoria.armaNivel==3)){
                SoundManagerScript.PlaySound("No");
            }
        }else{
            buySwordandBow.SetActive(false);
        }
    }
}
