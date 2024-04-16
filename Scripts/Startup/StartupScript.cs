using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupScript : MonoBehaviour
{
    void Start()
    {
        // Reset the timescale to 1. This is necessary because the timescale is set to 0 when the player wins.
        Time.timeScale = 1f;
    }
}
