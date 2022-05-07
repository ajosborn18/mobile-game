using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    
    public void StartGame()
    {
    	SceneManager.LoadScene("Level1");
    }
    
    public void LoadInstructions()
    {
    	SceneManager.LoadScene("InstructionsMenu");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    
    public void ExitGame()
    {
	Application.Quit();
	Debug.Log("Exit");
        
    }
}
