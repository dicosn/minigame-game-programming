using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //NEW FEATURE POINT : Lobby : script used for a lobby
    //FEATURE POINT: reuse a script : this script is reused from the lobby scene for title and game over screens as well
    // Start is called before the first frame update
    //FEATURE POINT: UI Button : This script is triggered by buttons in the lobby scene
    public string scene = "scene";
    void Start()
    {
        //FEATURE POINT: loadscene : loads the inputed scene;
        SceneManager.LoadScene(scene);
    }


}
