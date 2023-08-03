using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : dicosnPlayerController
{
    public GameObject projectile;
    public bool smart;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void onEnable()
    {
        //PanicSwitch.onPanic += myPanic;
    }


    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (!Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(1.5f, 0), .25f, LayerMask.GetMask("ground", "props", "Default")) || 
            Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(1f, -1.25f), .25f, LayerMask.GetMask("ground", "props")))
        {
            inputVector = new Vector2(-1f, 0f);
            if (smart)
            {
                StopCoroutine(blink());
                StartCoroutine(blink());
            }
        }
        if (!Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-1.5f, 0), .25f, LayerMask.GetMask("ground", "props", "Default")) ||
            Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-1f, -1.25f), .25f, LayerMask.GetMask("ground", "props")))
        {
            inputVector = new Vector2(1f, 0f);
            if (smart)
            {
                StopCoroutine(blink());
                StartCoroutine(blink());
            }
        }
    }

    //FEATURE POINT : enum : used to make the frog flash
    //FEATURE POINT : corutines (m) : used to make the frog flash
    IEnumerator blink()
    {
        for(int i = 0; i < 5; i++)
        {
            GetComponent<SpriteRenderer>().enabled ^= true;
            yield return new WaitForSeconds(0.25f);
        }
        GetComponent<SpriteRenderer>().enabled = true;
        //PanicSwitch.onPanic += MyPanic;
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Pizza")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //FEATURE POINT : Inheritance : Inheritin the jumpAdd variable from playercontroller to make the frog
            //jump when hit
            rb.velocity += new Vector2(0f, jumpAdd);
        }
    }

    private void OnDrawGizmos()
    {
        //base.OnDrawGizmos();
        //FEATURE POINT : gizmos : gizmos used to see where the overlap circles are so I can see where it thinks edges are
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(1f, -1.25f), .25f);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(-1f, -1.25f), .25f);
    }
}
