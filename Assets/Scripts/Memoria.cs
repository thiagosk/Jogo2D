using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Memoria : ScriptableObject
{
    // Cena
    public int profundo = 1; 

    // Player
    public float playerSpeed;
    public int playerLife;
    public int playerNumOfHearts; // Vida maxima

    // Moeda
    public int coin = 0;
    public int coinValue = 0;

    public int attackDamage = 1;
    public bool hasBow = true;
    
    // Upgrade Casa
    public int casaNivel = 1;

    public int numEnemies;
    // Upgrade Armas
    public int armaNivel = 0;
    // 0 -> nao comprou nada
    // 1 -> comprou espada de fogo e ainda nao comprou arco
    // 2 -> comprou arco e nao comprou espada de fogo
    // 3 -> comprou espada de fogo e arco

    public int bossExist = 0;
}
