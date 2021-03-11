using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Start varables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private bool TouchingGround = false;
    private AudioSource footsteps;

    //FSM
    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;
    
    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 6.5f;
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] private float glideSpeed = 1f;
    [SerializeField] private float hurtForce = 10f;

   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        footsteps = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int)state);

        if (coll.IsTouchingLayers(ground))
        {
            TouchingGround = true;
        }

        else
        {
            TouchingGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GemScript
        Gem(collision);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy(other);

    }



    private void Gem(Collider2D collision)
    {
        
        
        if (collision.tag == "Collectible")
        {
            GemScript gem = collision.gameObject.GetComponent<GemScript>();
            gem.Collected();
            PermaUIScript.perm.gems += 1;
            PermaUIScript.perm.gemAmount.text = PermaUIScript.perm.gems.ToString();
        }
    }

    private void Enemy(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();

            //KillingEnemy
            if (state == State.falling)
            {
                Jump();
                enemy.JumpedOn();
                
            }

            //Knockback
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x < transform.position.x)
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //Moving Left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }

        //Moving Right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }

        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.3f, ground);
            if (hit.collider != null)
            Jump();
        }

        //Gliding (still in work)
        if (state == State.falling)
        {
            if (Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, glideSpeed);
            }

            
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void AnimationState()
    {
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }

        else
        {
            state = State.idle;
        }

        if(state == State.running)
        {
            if( TouchingGround == false)
            {
                state = State.falling;
            }
        }


        if (state == State.idle)
        {
            if (TouchingGround == false)
            {
                state = State.falling;
            }
        }
    }


    private void Footsteps()
    {
        footsteps.Play();
    }

    //private IEnumerator ...()
    //{yield return new WaitForSeconds(x)
    //do something}

    //StartCoroutine(...());
}
