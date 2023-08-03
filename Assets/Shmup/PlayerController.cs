using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputAxes;
    public float speed = 2f;
    public float forceFactor = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //FEATURE POINT: input.get : used to control the location of the player
        inputAxes.x = Input.GetAxis("Horizontal");
        inputAxes.y = Input.GetAxis("Vertical");

    }
    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = inputAxes * speed;


        //turn towards mouse over time

        //FEATURE POINT: mouse aim : gradually moves towards the mouse's position
        Vector3 mouse = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);
        Vector3 targetVec = mousePos - gameObject.transform.position;
        targetVec.z = 0f;
        float step = 2.5f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(gameObject.transform.up, targetVec, step, 0.0f);


        Debug.DrawRay(gameObject.transform.position, newDir, Color.red);

        gameObject.transform.up = newDir;

        //makes the object still have physics applied to it
        if (inputAxes.magnitude > .01)
            gameObject.GetComponent<Rigidbody2D>().velocity = inputAxes * speed;
    }
}
