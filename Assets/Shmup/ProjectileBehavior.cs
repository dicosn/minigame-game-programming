using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 500f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 mouse = Input.mousePosition;
        //NEW FEATURE POINT : tracking the mouse : used to decide where the projectile will go
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);
        mousePos.z = 0f;
        direction = mousePos - transform.position;
        direction = direction.normalized;
        //direction *= 2f;
        

    }

    // Update is called once per frame
    void Update()
    {
        //FEATURE POINT: Time.deltaTime : used to make projectiles move in their direction
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

}
