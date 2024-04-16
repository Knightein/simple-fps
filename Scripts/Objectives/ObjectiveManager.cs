using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [Header("Objective Options")]
    public TextMeshProUGUI objectiveText;
    public bool isObjectiveCompleted;
    
    [Header("Level Settings")]
    public GameObject door;
    
    private int _objectiveCount;
    
    void Start()
    {
        objectiveText.text = "Objective: Destroy Opponents - 0 / 4";
        isObjectiveCompleted = false;
    }
    
    void Update()
    {
        if (_objectiveCount >= 4)
        {
            isObjectiveCompleted = true;
        }
    }
    
    /// <summary>
    /// This method is called when the player has destroyed an enemy. It updates the objective text.
    /// </summary>
    public void UpdateTextOnDestroy()
    {
        _objectiveCount++;
        objectiveText.text = "Objective: Destroy Opponents - " + _objectiveCount + " / 4";
        if (_objectiveCount >= 4)
        {
            CompleteObjective();
        }
    }
    
    /// <summary>
    /// This method is called when the player has destroyed all enemies. It opens the door to finish the level.
    /// </summary>
    private void CompleteObjective()
    {
        Destroy(door); 
    }
    
    /// <summary>
    /// This method is called when the player triggers the game ending. It returns true if the level is finished.
    /// </summary>
    /// <returns>True if the level is finished; otherwise, false.</returns>
    public bool IsLevelFinished()
    {
        return isObjectiveCompleted;
    }
}
