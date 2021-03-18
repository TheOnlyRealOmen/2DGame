using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private Animator anim;
    
    private enum State { FourHearts, ThreeHearts, TwoHearts, OneHeart }
    private State state = State.FourHearts;

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
        if (PermaUIScript.perm.hearts == 4)
        {
            state = State.FourHearts;
        }

        if (PermaUIScript.perm.hearts == 3)
        {
            state = State.ThreeHearts;
        }

        if (PermaUIScript.perm.hearts == 2)
        {
            state = State.TwoHearts;
        }
        if (PermaUIScript.perm.hearts == 1)
        {
            state = State.OneHeart;
        }
    }
}
