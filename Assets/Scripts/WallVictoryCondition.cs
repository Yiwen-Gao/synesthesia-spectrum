using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WallVictoryCondition : MonoBehaviour
{
    public static int score = 0;
    //Number of seconds when reloading the scene
    private float restartDelay = 2.5f;
    public string nextScene;
    public Text playerStatusDisplay;

    private void Start()
    {
        NetworkClient client = FindObjectOfType<NetworkClient>();
        if (client != null)
        {
            client.RegisterCallback("victory", (string name) =>
                {
                    Lose();
                });
        }

        // playerStatusDisplay.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NetworkClient client = FindObjectOfType<NetworkClient>();
            if (client != null)
            {
                client.SendMessageNetwork("victory");
            }

            //Restart the game
            //"Restart" is the method, and restartDelay will delay the method by restartDelay seconds.
            Invoke("Restart", restartDelay);
            score++;
        }
    }

    void Restart()
    {
        /*
         * This line reloads the scene
         * .GetActiveScene().name() gets the current scene level
         * If we have other scenes, we can set this to load those scenes.
         * Reference video https://youtu.be/VbZ9_C4-Qbo?t=481
         * */

        //SceneManager.GetActiveScene().name (restarts current scene)
        playerStatusDisplay.enabled = true;
        playerStatusDisplay.text = "You win!";
        SceneManager.LoadScene(nextScene);
    }

    void Lose()
    {
        playerStatusDisplay.enabled = true;
        playerStatusDisplay.text = "You lose!";
        SceneManager.LoadScene(nextScene);
    }
}
