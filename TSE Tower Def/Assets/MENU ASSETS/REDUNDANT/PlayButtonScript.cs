using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    public void SwitchToGame(int input)
    {
        SceneManager.LoadScene(input);
    }
}
