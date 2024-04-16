using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(1); 
            // Instead of giving the death message and reloading the scene,
            // we just reload the scene. While the player should not be able to get out of bounds,
            // except for one room where it is fairly easy to, I expect that the player will not
            // jump out of bounds on purpose, so we can just reload the scene.
        }
    }
}
