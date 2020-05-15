using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{

    public TextMeshProUGUI SceneDisplay;

    // Start is called before the first frame update
    void Start()
    {
        SceneDisplay.enabled = true;
        int score = WallVictoryCondition.score;
        if (score < 2)
        {
            SceneDisplay.text = "Oh No, You Lost!\n\nYour Score: " + score.ToString();
        }
        else
        {
            SceneDisplay.text = "Congratulations, You Won!\n\nYour Score: " + WallVictoryCondition.score.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
}
