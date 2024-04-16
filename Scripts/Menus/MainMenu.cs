using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// This method is called when the player clicks the "Play" button on the main menu.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
