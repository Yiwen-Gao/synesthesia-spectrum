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
        SceneDisplay.text = "Congratulations, You Won!\n\nYour Score: " +  WallVictoryCondition.score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
}
