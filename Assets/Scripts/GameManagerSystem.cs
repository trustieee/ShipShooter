using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerSystem : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        GameCountdown,
        GamePlay,
        GameDeath,
        GamePause
    }

    private Color _defaultColor;
    private bool _isCountdownRunning;
    private PlayerController _playerController;

    public float CountdownAmount;
    public Text CountdownText;
    public GameState State;

    // Start is called before the first frame update
    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _defaultColor = Camera.main.backgroundColor;
        State = GameState.GameCountdown;
    }

    private void Update()
    {
        switch (State)
        {
            case GameState.Menu:
                break;
            case GameState.GameCountdown:
                if (!_isCountdownRunning)
                {
                    _isCountdownRunning = true;
                    CountdownText.enabled = true;
                }

                if (_isCountdownRunning)
                {
                    if (CountdownAmount > 0)
                    {
                        CountdownAmount -= Time.deltaTime;
                        CountdownText.text = CountdownAmount.ToString();
                        Camera.main.backgroundColor = Color.grey;
                    }
                    else
                    {
                        CountdownAmount = 0;
                        _isCountdownRunning = false;
                        CountdownText.enabled = false;
                        Camera.main.backgroundColor = _defaultColor;
                        State = GameState.GamePlay;
                    }
                }

                break;
            case GameState.GamePlay:
                if (!_playerController.IsAlive)
                {
                    State = GameState.GameDeath;
                }

                break;
            case GameState.GameDeath:
                break;
            case GameState.GamePause:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}