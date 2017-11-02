using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadLevel (string levelName)
    {
        if (NiceSceneTransition.instance != null)
        {
            NiceSceneTransition.instance.LoadScene(levelName);
        }
        else
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }
}
