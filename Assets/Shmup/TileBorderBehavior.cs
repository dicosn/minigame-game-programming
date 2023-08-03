using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBorderBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //FEATURE POINT : colliders : used to
    //FEATURE POINT : enforce boundaries
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //FEATURE POINT: tags : dropped items are tagged as "dropped" for use in destruction of those prefabs
        //FEATURE POINT: tilemaps : tiles enforce the border that the player and enemies can roam
        if (collision.gameObject.tag == "dropped" && LayerMask.LayerToName(collision.gameObject.layer) != "ground")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Patron" && collision.gameObject.name != "smart" && collision.gameObject.name != "dumb")
        {
            if (collision.gameObject.GetComponent<PatronBehavior>().fed)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
