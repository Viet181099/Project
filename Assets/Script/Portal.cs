using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SaveManager.instance.Save();
            PlayerPrefs.DeleteKey("SceneData");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
