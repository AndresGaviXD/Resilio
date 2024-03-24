using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AddOffset : MonoBehaviour
{
    bool isItBattle = false;

    // Update is called once per frame
    void Update()
    {
        if(!isItBattle)
        {
            addOffsetToObjects();
        }
    }
    void addOffsetToObjects()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        string objectName = gameObject.name;


        if (sceneName == "Battle")
        {
            transform.position = new Vector3(transform.position.x - 3.0f, transform.position.y - 0.27f, transform.position.z);
            isItBattle = true;
        }
    }
}
