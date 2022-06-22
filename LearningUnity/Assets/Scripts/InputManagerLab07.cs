using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManagerLab07 : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpeedManager.CurrentSpeedState =
                (SpeedManager.CurrentSpeedState == SpeedManager.GameSpeed.Slow) ?
                    SpeedManager.GameSpeed.Fast :
                    SpeedManager.GameSpeed.Slow;
        }
        if (GameManager.currentGameState == GameManager.GameState.Start && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene((int)GameManager.GameState.WalkingLevel);
        }
    }
}
