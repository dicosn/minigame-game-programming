using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //change to dynamic
            // rb.bodyType = RigidbodyType2D.Dynamic;

            //FEATURE POINT : GetComponentInParent : tells platformermanager to increase the amount of doors
            GetComponentInParent<PlatformerManager>().Collected(transform.position);
            //Destroy(this);
        }
    }
}
