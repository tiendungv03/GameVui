using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("MapDemo");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
