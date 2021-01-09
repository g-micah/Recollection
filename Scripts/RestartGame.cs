using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    // Start Button on click
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
        GameManager.instance.heal();
        GameManager.instance.experience = 0;
    }

    public void RestartBtn()
    {
        PlayerPrefs.DeleteAll();
        GameManager.instance.heal();
        SceneManager.LoadScene(1);
    }
}
