using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // load scene

public class Menu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("AsteroidsScene");
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
