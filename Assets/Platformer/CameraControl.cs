using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float deltaTime = .02f;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //TriggerExit2D, with a collider where the fox isn't could work too
    //FEATURE POINT : triggers : triggered by player, begins camera movement
    //FEATURE POINT ; script a triggerable camera move : the player activates this trigger, which initiates a camera move
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //FEATURE POINT : StopCoroutine : stops previous camera movement and begins with a new direction
            StopCoroutine(MoveCamera(collision));
            //FEATURE POINT : coroutines : used to move the camera to the player's position gradually over time
            StartCoroutine(MoveCamera(collision));
        }
    }

    
    //FEATURE POINT : enum : used to move camera
    IEnumerator MoveCamera(Collider2D collision)
    {
        Vector2 ogPos, newPos;
        float t;

        newPos = collision.transform.position;

        ogPos = transform.position;

        t = 0;

        while((Vector2)transform.position != (Vector2)collision.transform.position)
        {
            Vector3 temp;
            //FEATURE POINT : lerp : used to adjust the temp vector to gradually reach the new position
            temp = Vector2.Lerp(ogPos, collision.transform.position, (t * 2) - (t * t));
            temp.z = -10f;
            transform.position = temp;

            t += deltaTime * speed;
            t = Mathf.Clamp(t, 0, 1f);
            yield return new WaitForSeconds(deltaTime);
        }
        yield return null;
    }
}
