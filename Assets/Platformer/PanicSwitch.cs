using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicSwitch : MonoBehaviour
{
    
    bool spriteFile = false;

    public Sprite[] sprite = new Sprite[2];

    public delegate void PanicType();

    public static event PanicType onPanic;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprite[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteFile ^= true;
        GetComponent<SpriteRenderer>().sprite = sprite[spriteFile ? 0 : 1];

        onPanic?.Invoke();
    }
    

}
