using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{

    //Number of seconds when reloading the scene
    private float restartDelay = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //CMD line prompt when Player object collides with wall
            Debug.Log("Game Over, you lost!");
            FindObjectOfType<NetworkClient>().SendMessageNetwork("died", "name");

            //Restart the game
            //"Restart" is the method, and restartDelay will delay the method by restartDelay seconds.
            Invoke("DeathRestart", restartDelay);
        }
    }

    void DeathRestart()
    {
        /*
         * This line reloads the scene
         * If we have other scenes, we can set this to load those scenes.
         * Reference video https://youtu.be/VbZ9_C4-Qbo?t=481
         * */

        //Restarts current scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
