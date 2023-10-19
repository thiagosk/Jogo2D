using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marcenaria : MonoBehaviour
{
    private Transform player;

    public GameObject upgradeCasa;
    public GameObject maxCasa;

    public Memoria memoria;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CasaUpgrade();
    }

    private void CasaUpgrade(){
        if (memoria.casaNivel <= 1) {
            memoria.casaNivel = 1;
        }
        if (Vector2.Distance(player.position, transform.position) <= 3f){
            if (memoria.casaNivel == 3) {
                maxCasa.SetActive(true);
                upgradeCasa.SetActive(false);
            }
            else {
                upgradeCasa.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E) && memoria.coin>=60 && memoria.casaNivel!=3){

                SoundManagerScript.PlaySound("Interaction");

                if (memoria.casaNivel == 1) {
                    memoria.casaNivel = 2;
                    memoria.coin -= 60;
                } else if (memoria.casaNivel == 2) {
                    memoria.casaNivel = 3;
                    memoria.coin -= 60;
                }
            }
            else if ((Input.GetKeyDown(KeyCode.E) && memoria.coin<60) || (Input.GetKeyDown(KeyCode.E) && memoria.casaNivel==3)){
                SoundManagerScript.PlaySound("No");
            }
        }
        else {
            upgradeCasa.SetActive(false);
            maxCasa.SetActive(false);
        }
    }
}
