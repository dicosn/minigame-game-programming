using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerManager : MonoBehaviour
{
    public int parts;
    public GameObject doorprefab;
    //FEATURE POINT : dictionaries : stores the prefabs by their positions
    public IDictionary<Vector3, GameObject> partsDic;
    // Start is called before the first frame update
    void Start()
    {
        partsDic = new Dictionary<Vector3, GameObject>();
        //FEATURE POINT : Instantiate a prefab : 3 door part prefabs are spawned when added to the dictionary
        partsDic.Add(new Vector3(-2.49000001f, 4.46999979f, 0f), Instantiate(doorprefab,transform));
        partsDic.Add(new Vector3(14.896553f, 11.76507f, 0f), Instantiate(doorprefab, transform));
        partsDic.Add(new Vector3(15.4899998f, 7.03999996f, 0f), Instantiate(doorprefab, transform));
        parts = 0;

        foreach (KeyValuePair<Vector3, GameObject> kvp in partsDic)
        {
            kvp.Value.transform.position = kvp.Key;
        }
        /*
        //FEATURE POINT : Instantiate a prefab : door part prefabs are spawned at their locations
        Instantiate(doorprefab, new Vector3(-2.49000001f, 4.46999979f, 0f), transform.rotation);
        Instantiate(doorprefab, new Vector3(14.896553f, 11.76507f, 0f), transform.rotation);
        Instantiate(doorprefab, new Vector3(15.4899998f, 7.03999996f, 0f), transform.rotation);*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collected(Vector3 position)
    {
        Destroy(partsDic[position]);
        parts++;
    }
}
