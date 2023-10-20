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

    // Upgrade Casa
    public int casaNivel = 1;
}
