using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text _scoreText;
    private GameSession _gameSession;

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
        _gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        _scoreText.text = _gameSession.GetScore().ToString();
    }
}
