using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : EnemyScript
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLenght = 7f;
    [SerializeField] private float jumpHeight = 7f;
    [SerializeField] private LayerMask ground;

    private bool facingLeft = true;

    private Collider2D coll;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        //Jumping Left
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if (coll.IsTouchingLayers(ground) == true)
                {
                    rb.velocity = new Vector2(-jumpLenght, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }

            else
            {
                facingLeft = false;
            }
        }

        //Jumping right
        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if (coll.IsTouchingLayers(ground) == true)
                {
                    rb.velocity = new Vector2(jumpLenght, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

}