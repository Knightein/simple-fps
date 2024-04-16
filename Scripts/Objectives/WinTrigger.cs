using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [Header("Win Options")]
    [Tooltip("The panel that appears when the player wins the game.")]
    public GameObject winPanel;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            var objectiveManager = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();
            if (objectiveManager.IsLevelFinished())
            {
                Time.timeScale = 0; // Pause the game, this disables fixed update and physics.
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                winPanel.SetActive(true);
            }
        }
    }
}
