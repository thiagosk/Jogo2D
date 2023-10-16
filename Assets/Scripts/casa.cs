using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casa : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    public Memoria memoria;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (memoria.casaNivel == 1) {
            spriteRenderer.sprite = spriteArray[0];
            memoria.playerNumOfHearts = 3;
            memoria.playerSpeed = 3f;
        }
        else if (memoria.casaNivel == 2) {
            spriteRenderer.sprite = spriteArray[1]; 
            memoria.playerNumOfHearts = 5;
            memoria.playerSpeed = 5f;
        }
        else if (memoria.casaNivel == 3) {
            spriteRenderer.sprite = spriteArray[2]; 
            memoria.playerNumOfHearts = 7;
            memoria.playerSpeed = 7f;
        }
    }
}
