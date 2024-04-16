using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerMenu : MonoBehaviour
{
    /// <summary>
    /// This method is called when the player clicks the "Play Again" button. Loads the Main Game scene.
    /// </summary>
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    /// <summary>
    /// This method is called when the player clicks the "Main Menu" button. Loads the Main Menu scene.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
