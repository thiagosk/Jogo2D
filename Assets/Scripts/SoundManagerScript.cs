using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip EnemyHit, Interaction, PlayerHit, PlayerAttack, BlobAttack, EskelAttack, Portal, Pot, Coin, No;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHit = Resources.Load<AudioClip> ("EnemyHit");
        Interaction = Resources.Load<AudioClip> ("Interaction");
        PlayerHit = Resources.Load<AudioClip> ("PlayerHit");
        PlayerAttack = Resources.Load<AudioClip> ("PlayerAttack");
        BlobAttack = Resources.Load<AudioClip> ("BlobAttack");
        EskelAttack = Resources.Load<AudioClip> ("EskelAttack");
        Portal = Resources.Load<AudioClip> ("Portal");
        Pot = Resources.Load<AudioClip> ("Pot");
        Coin = Resources.Load<AudioClip> ("Coin");
        No = Resources.Load<AudioClip> ("No");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "EnemyHit":
                audioSrc.PlayOneShot (EnemyHit);
                break;
            case "Interaction":
                audioSrc.PlayOneShot (Interaction);
                break;
            case "PlayerHit":
                audioSrc.PlayOneShot (PlayerHit);
                break;
            case "PlayerAttack":
                audioSrc.PlayOneShot (PlayerAttack);
                break;
            case "BlobAttack":
                audioSrc.PlayOneShot (BlobAttack);
                break;
            case "EskelAttack":
                audioSrc.PlayOneShot (EskelAttack);
                break;
            case "Pot":
                audioSrc.PlayOneShot (Pot);
                break;
            case "Portal":
                audioSrc.PlayOneShot (Portal);
                break;
            case "Coin":
                audioSrc.PlayOneShot (Coin);
                break;
            case "No":
                audioSrc.PlayOneShot (No);
                break;
        }
    }

}
