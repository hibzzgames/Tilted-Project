using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField]
    private Transform PongTransform = null;

    [SerializeField]
    private TMP_Text GameOverText = null;

    [SerializeField]
    private float YKillZone = 6;

    private void Start()
    {
        Time.timeScale = 1; // TODO: REMOVE THIS AS WELL
    }

    private void OnEnable()
    {
        PaddleLife.OnPaddleHealthZero += TriggerGameOver;
    }

    private void OnDisable()
    {
        PaddleLife.OnPaddleHealthZero -= TriggerGameOver;
    }

    private void Update()
    {
        if(PongTransform.position.y > YKillZone || PongTransform.position.y < -YKillZone )
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        GameOverText.text = "GAME OVER";
        Time.timeScale = 0; // TODO: NEED TO DO SOMETHING BETTER THAN THIS
    }
}
