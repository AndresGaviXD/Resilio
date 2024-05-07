using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        // Verificar si se ha cargado la escena "SampleScene"
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            // Destruir este objeto
            Destroy(gameObject);
        }
    }
}
