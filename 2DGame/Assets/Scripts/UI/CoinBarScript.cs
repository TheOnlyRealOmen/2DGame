using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBarScript : MonoBehaviour
{
    private Animator anim;

    private enum State {ZeroCoins, OneCoin, TwoCoins, ThreeCoins, FourCoins, FiveCoins}
    private State state = State.ZeroCoins;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void AnimationState()
    {
        if (PermaUIScript.perm.gems == 0)
        {
            state = State.ZeroCoins;
        }

        if (PermaUIScript.perm.gems == 1)
        {
            state = State.OneCoin;
        }

        if (PermaUIScript.perm.gems == 2)
        {
            state = State.TwoCoins;
        }

        if (PermaUIScript.perm.gems == 3)
        {
            state = State.ThreeCoins;
        }

        if (PermaUIScript.perm.gems == 4)
        {
            state = State.FourCoins;
        }

        if (PermaUIScript.perm.gems == 5)
        {
            state = State.FiveCoins;
        }
    }
}
