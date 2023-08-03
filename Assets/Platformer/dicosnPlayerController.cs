using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dicosnPlayerController : MonoBehaviour
{
    protected Vector2 inputVector;
    protected bool jump, dash;
    public bool grounded;
    public float topspeed = 3f;
    public float jumpAdd = 5f;

    int hp;

    public int Health
    {
        //FEATURE POINT : get : fetches the hp
        get => hp;
        //FEATURE POINT : set : sets the hp
        set { hp = Health; print(hp); CheckHealth(hp); }
    }

    void CheckHealth(int health)
    {
        if (health <= 0)
            this.gameObject.SetActive(false);
        else
        {
            Color MyColor = GetComponent<SpriteRenderer>().color;
            MyColor.a = (float)health / 10f;
            GetComponent<SpriteRenderer>().color = MyColor;
            if(health == 0)
            {
                if (health == 0)
                {
                    SceneManager.LoadScene("PlatformerGameOver");
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        Health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector2(Input.GetAxis("Horizontal"), 0f);
        //FEATURE POINT : jumping : when the jump button is pressed, the jump bool gets set to true,
        //which when true, during FixedUpdate, the player will have an upward velocity applied to them
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Patron")
        {
            //hp--;
            Health--;
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        //FEATURE POINT : grounding : the grounded bool gets set to the vale of the bool obtained by
        //the OverlapCircle function overlapping with something on the ground/props/NPC layer
        //FEATURE POINT : layers : the ground, props, and NPC layers are used to determine if the circle beneath the player is on those such objects
        //FEATURE POINT : raycast : raycasting circles to detect if player is on the ground
        if (grounded = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -1.2f), .25f, LayerMask.GetMask("ground", "props","NPC"))){
            if (jump)
            {
                //upward velocity is added
                rb.velocity += new Vector2(0f, jumpAdd);

                jump = false;
            }
        }
        jump = false;
        //used to make the player move back and forth in the air
        rb.velocity = new Vector2(inputVector.x * topspeed, rb.velocity.y);

    }
}
