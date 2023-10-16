using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleFire : MonoBehaviour
{
    private float lifeTime = 8f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);        
    }

}
