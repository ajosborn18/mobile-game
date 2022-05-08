using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public string level_to_load = "LevelX";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && PublicVars.score > 10)
        {
            PublicVars.score = 0;
            SceneManager.LoadScene(level_to_load);
        }

        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    
}
