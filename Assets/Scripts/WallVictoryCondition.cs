using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallVictoryCondition : MonoBehaviour
{

    //Number of seconds when reloading the scene
    private float restartDelay = 1f;
    public string nextScene;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //CMD line prompt when Player object collides with wall
            Debug.Log("You won!");

            NetworkClient client = FindObjectOfType<NetworkClient>();
            if (client != null)
            {
                client.SendMessageNetwork("victory");
            }

            //Restart the game
            //"Restart" is the method, and restartDelay will delay the method by restartDelay seconds.
            Invoke("Restart", restartDelay);
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
        SceneManager.LoadScene(nextScene);
        
    }

    void Lose()
    {
        Debug.Log("You lose");

        SceneManager.LoadScene("LoserScene");
    }
}
