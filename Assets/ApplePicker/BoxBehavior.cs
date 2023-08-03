using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction = Vector3.left;
    public float speed = 1f;
    public GameObject dropped;
    float directionChangeChance = 0.3f;
    float spawnChance = 0.6f;
    void Start()
    {
        transform.position = new Vector2(0f, 2.37f);
        Invoke("ChangeDirectionRandomly", Random.Range(1f, 3f)); //starting call
    }
    void ChangeDirectionRandomly()
    {
        Invoke("ChangeDirectionRandomly", Random.Range(1f, 3f)); //call again
    }
    // Update is called once per frame, randomly changes the speed so the enemy goes the opposite direction, and potentially instantiates dropped toast
    void Update()
    {
        //FEATURE POINT : Enforcing boundaries : the enemy is prevented from moving beyond 7.57 and -7.57
        //it is at the right edge of the screen
        if (transform.position.x >= 7.57f && speed < 0f)
        {
            speed *= -1;
        }
        //it is at the left edge of the screen
        else if (transform.position.x <= -7.57f && speed > 0f)
        {
            speed *= -1;
        }
        else
        {  
            //NEW FEATURE POINT : Random value : used to as a threshhold to randomly spawn a piece of toast
            //                                                          It is moving right and not too near the left  or it is moving left and not too near the right
            if (Random.value <= directionChangeChance * Time.deltaTime && ((transform.position.x >= -4f && speed < 0f) || (transform.position.x <= 4f && speed > 0f)))
            {
                speed *= -1;
            }
        }
        //FEATURE POINT: Time.deltaTime : this is used to make sure the enemy moves at a constant speed, rather than juttering around depending on framerate
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        if (Random.value <= spawnChance * Time.deltaTime)
        {
            //FEATURE POINT: instantiating prefabs : dropped objects are instanted prefabs, as well as the player's "baskets"
            GameObject clone = Instantiate(dropped, transform.position, Quaternion.identity);
            //FEATURE POINT: AddForce : each toast that spawns will have an initial upward force before falling down.
            clone.GetComponent<Rigidbody2D>().AddForce(transform.up * 300f);
            //FEATURE POINT: Layers : the instantiated dropped objects are on their own layer to avoid collisions with each other set up in the collision matrix
            //FEATURE POINT: Triggered Sounds : the enemy plays a "ding" sound effect when spawning a dropped toast
            GameObject sound = GameObject.Find("ding");
            sound.GetComponent<AudioSource>().Play();
        }

    }

    //this function is called whenever a score is earned by the player
    public void increaseSpeed()
    {
        //slightly increases the speed of the enemy until it is around 10 times as fast as its starting speed
        if ((speed > 0 && speed < 10f) || (speed < 0 && speed > -10f))
        {
            speed *= 1.05f;
        }
        //once it has reached the max potential speed, start increasing the spawn rate
        else
        {
            spawnChance += 0.01f;
        }
        //halfway throuh the max potential speed, start increasing chances toast will spawn and the chance that the enemy will change direction
        if((speed > 5f && speed < 10f) || (speed < -5f && speed > -10f))
        {
            spawnChance += 0.01f;
            directionChangeChance += 0.05f;
        }
    }
}
