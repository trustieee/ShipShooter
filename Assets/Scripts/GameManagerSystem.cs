using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerSystem : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        GameCountdown,
        SpawnWave,
        GamePlay,
        GameDeath,
        GamePause,
        WaveCleared
    }

    private Color _defaultColor;
    private Vector3 _defaultPlayerPosition;
    private bool _isCountdownRunning;
    private bool _isWaveClearedShowing;
    private PlayerController _playerController;
    private Spawner _spawner;
    private float _waveClearedTimer;

    public float CountdownAmount;
    public Text CountdownText;
    public GameState State;
    public float WaveClearedDuration;
    public Text WaveClearedText;

    // Start is called before the first frame update
    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        WaveClearedText.enabled = false;
        _playerController = FindObjectOfType<PlayerController>();
        _defaultColor = Camera.main.backgroundColor;
        _defaultPlayerPosition = _playerController.transform.position;
        State = GameState.GameCountdown;
    }

    private void Update()
    {
        switch (State)
        {
            case GameState.Menu:
                break;
            case GameState.GameCountdown:
            {
                if (!_isCountdownRunning)
                {
                    _isCountdownRunning = true;
                    CountdownText.enabled = true;
                }
                else
                {
                    _playerController.transform.position = _defaultPlayerPosition;
                    if (CountdownAmount > 0)
                    {
                        CountdownAmount -= Time.deltaTime;
                        CountdownText.text = CountdownAmount.ToString(CultureInfo.InvariantCulture);
                        Camera.main.backgroundColor = Color.grey;
                    }
                    else
                    {
                        CountdownAmount = 5;
                        _isCountdownRunning = false;
                        CountdownText.enabled = false;
                        Camera.main.backgroundColor = _defaultColor;
                        State = GameState.SpawnWave;
                    }
                }

                break;
            }
            case GameState.GamePlay:
            {
                if (!_playerController.IsAlive)
                {
                    State = GameState.GameDeath;
                }

                if (_spawner.Enemies.All(e => e.GetComponent<EnemyBehavior>().IsAlive == false))
                {
                    _spawner.Enemies.Clear();
                    State = GameState.WaveCleared;
                }

                break;
            }
            case GameState.GameDeath:
                break;
            case GameState.GamePause:
                break;
            case GameState.WaveCleared:
                if (!_isWaveClearedShowing)
                {
                    StartCoroutine(ShowWaveCleared());
                }

                break;
            case GameState.SpawnWave:
                _spawner.SpawnWave(1);
                State = GameState.GamePlay;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator ShowWaveCleared()
    {
        _isWaveClearedShowing = true;
        WaveClearedText.enabled = true;
        _waveClearedTimer = 0;
        while (_waveClearedTimer < WaveClearedDuration)
        {
            _waveClearedTimer += Time.deltaTime;
            yield return null;
        }

        WaveClearedText.enabled = false;
        _isWaveClearedShowing = false;
        State = GameState.GameCountdown;
    }
}