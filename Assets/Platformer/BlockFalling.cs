using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFalling : MonoBehaviour
{
    public bool falling;
    public float deltaTime = 5f;
    public Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        falling = false;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            transform.position = new Vector2(0f, -50f);
        }
        if (collision.gameObject.tag == "Player")
        {
            //FEATURE POINT : StopCoroutine : Used to stop any previous falling initiation
            StopCoroutine(Falling(collision));
            //FEATURE POINT : coroutines : Used to respawn the block after a short amount of time
            StartCoroutine(Falling(collision));
            
        }
    }

    //FEATURE POINT : enums : used to "destroy" a block (move it out of view) and place it back at the original position
    IEnumerator Falling(Collision2D collision)
    {   
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(deltaTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
        falling = true;
        yield return new WaitForSeconds(deltaTime);
        transform.position = new Vector2(0f, -50f);
        //change to kinetic
        rb.bodyType = RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(deltaTime);
        falling = false;
        transform.position = pos;
        rb.velocity = new Vector2(0f,0f);
        yield return null;
    }
}
