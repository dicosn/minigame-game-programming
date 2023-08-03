using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{

    public GameObject walker;
    public GameObject projectile;

    public Transform tip;

    private Vector3 walkerPos;
    //public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        walkerPos = walker.transform.position;
        transform.position = walkerPos;

        Vector3 mouse = Input.mousePosition;
        //FEATURE POINT: mouse aim : used to direct launching of pizzas
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);

        //Vector3 targetVec = mousePos - walkerPos;

        Vector2 newDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        //float step = Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards(transform.up, targetVec, step, 0.0f);

        //Debug.DrawRay(launcher.transform.position, newDir, Color.red);

        transform.up = newDir;
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    //FEATURE POINT: shooting : the PizzaWalker shoots a pizza
    void Fire()
    {
        //FEATURE POINT: prefab instantiating : used to make the pizzas
        Instantiate(projectile, tip.position, Quaternion.identity);
    }
}
