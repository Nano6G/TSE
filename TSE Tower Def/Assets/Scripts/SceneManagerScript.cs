using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SwitchToScene(int input)
    {
        SceneManager.LoadScene(input);
    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
