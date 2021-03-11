using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : CollectibleScript
{
    private AudioSource coinSound;

    private void Update()
    {
        coinSound = GetComponent<AudioSource>();
    }
    
    public void Collected()
    {
        anim.SetTrigger("Collected");
        coinSound.Play();
    }

    private void Destroyed()
    {
        Destroy(this.gameObject);
    }
}
