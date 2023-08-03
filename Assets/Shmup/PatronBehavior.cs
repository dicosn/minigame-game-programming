using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronBehavior : MonoBehaviour
{
    //0 if perpendicular, < 0if "behind", >0 if "in front", magnitude is "how directly"
    public Vector2 targetVec2;
    public Vector3 position1;
    public Vector3 direction;

    public float speed = 2f;
    public bool fed = false;
    // Start is called before the first frame update

    
    void Start()
    {
        
        position1 = transform.position;
        //right side of screen
        if (Random.Range(0, 1) == 0)
        {   
            
            position1.x = -8.377f;
            
        }
        else
        {
            position1.x = 8.377f;
        }
        position1.y = Random.Range(-4.45f, 4.45f);
        transform.position = position1;
        direction = new Vector3(Random.value, Random.value, 0f);
        direction = direction.normalized;
        
        //FEATURE POINT: addForce : gives the patron initial moment as it mumbles about the resturant
        GetComponent<Rigidbody2D>().AddForce(direction * 200f);

    }

    //if (hit.collider?.name == "Player"){
      
   //LayerMask.GetMaask... normal
   //~LayerMask.GetMask ... whatever sin


    // Update is called once per frame
    void Update()
    {
        //normalized means length to one
        //targetVec2 = other.transform.position - transform.position;
        /*if (Vector2.Dot(targetVec2.normalized, transform.up) > 0.8f)
        {
            // I see you!
        }*/


    //transform.position += (Vector3)(direction * speed * Time.deltaTime);
        //position1 = transform.position;
    }

    private void FixedUpdate()
    {
        //Vector2 targetVec2; //may or may not need
        //targetVec2 = other.transform.posiotion - transform.position;
        if (Vector2.Dot(targetVec2.normalized, transform.up) > 0.8f)
        {
            // I see you!
            ///RaycastHit2D hit = Physics2D.Raycast(transform.position, targetVec2, Mathf.Infinity, layerMask);


        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //FEATURE POINT: tags : dropped items are tagged as "dropped" for use in destruction of those prefabs
        if (collision.gameObject.tag == "dropped")
        {
            Destroy(collision.gameObject);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            direction = transform.up;
            GetComponent<Rigidbody2D>().AddForce(direction * 1000f);
            //speed = 60f;
            fed = true;
        }
       /* else
        {//Change direction like pong
            if (position1.y < -4f && direction.y > 0f)
            {   
                //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000f);
                direction.y *= -1f;
                transform.position = new Vector3(0f, 0f, 0f);
            }
            if (position1.y > 4f && direction.y < 0f)
            {
                //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * -1000f);
                direction.y *= -1f;
                transform.position = new Vector3(0f, 0f, 0f);
            }
            if (position1.x > 7.5f && direction.x < 0f)
            {
                //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 1000f);
                direction.x *= -1f;
                transform.position = new Vector3(0f, 0f, 0f);
            }
            if (position1.x < -7.5f && direction.x > 0f)
            {
                //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * -1000f);
                direction.x *= -1f;
                transform.position = new Vector3(0f, 0f, 0f);
            }
                
        }*/
    }

}
