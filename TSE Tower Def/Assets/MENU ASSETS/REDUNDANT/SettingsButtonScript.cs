using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButtonScript : MonoBehaviour
{
    public void SwitchToSettings()
    {
        SceneManager.LoadScene("Settings Menu");
    }
}
