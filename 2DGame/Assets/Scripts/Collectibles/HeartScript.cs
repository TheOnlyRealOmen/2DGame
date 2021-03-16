using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : CollectibleScript
{
    public void Collected()
    {
        anim.SetTrigger("Collected");
    }

    private void Destroyed()
    {
        Destroy(this.gameObject);
    }
}
