using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : MonoBehaviour
{
    //public int parts;
    //public bool done;
    // Start is called before the first frame update
    void Start()
    {
        //done = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GetComponentInParent<PlatformerManager>().parts == 3)
        {
            //do win screen
            //FEATURE POINT : LoadScene : Loads the game over screen
            SceneManager.LoadScene("PlatformerGameOver");
        }
    }
}
