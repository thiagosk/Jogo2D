using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class init : MonoBehaviour
{
    // Start is called before the first frame update
    public Memoria memoria;
    void Start()
    {
        // Cena
        memoria.profundo = 1; 

        // Player
        memoria.playerSpeed=5f;
        memoria.playerLife=3;
        memoria.playerNumOfHearts=3; // Vida maxima

        // Moeda
        memoria.coin = 0;
        memoria.coinValue = 0;

        memoria.attackDamage = 1;
        memoria.hasBow = false;
        
        // Upgrade Casa
        memoria.casaNivel = 1;

        memoria.numEnemies = 0;
        // Upgrade Armas
        memoria.armaNivel = 0;
        // 0 -> nao comprou nada
        // 1 -> comprou espada de fogo e ainda nao comprou arco
        // 2 -> comprou arco e nao comprou espada de fogo
        // 3 -> comprou espada de fogo e arco\
        
        memoria.bossExist = 0;
    }


}
