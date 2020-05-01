using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WallVictoryCondition : MonoBehaviour
{
    public static int score = 0;
    //Number of seconds when reloading the scene
    private float restartDelay = 2.5f;
    public string nextScene;
    public TextMeshProUGUI playerStatusDisplay;

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

        playerStatusDisplay.enabled = false;
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
            score++;
            StartCoroutine(LoadNextScene(restartDelay, "You win!"));
        }
    }

    void Lose()
    {
        StartCoroutine(LoadNextScene(restartDelay, "You lose!"));
    }

    IEnumerator LoadNextScene(float delay, string text) {
        playerStatusDisplay.enabled = true;
        playerStatusDisplay.text = text;

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);
    }
}
