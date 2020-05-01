using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public string singlePlayerScene;
	public string multiPlayerScene;

    public void StartSinglePlayer() 
    {
    	SceneManager.LoadScene(singlePlayerScene);
    }

    public void StartMultiPlayer()
    {
    	SceneManager.LoadScene(multiPlayerScene);
    }
}
