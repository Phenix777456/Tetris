using UnityEngine;
using UnityEngine.Windows;

public sealed class GameBootstrap : MonoBehaviour
{
    [SerializeField] private TetrisInputHandler _input;

    private TetrominoController _controller;

    [SerializeField] private TetrominoSpawner _spawner;
    [SerializeField] private LevelSpeed _speed;
    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 20;

    private GameSession _session;

    private void Start()
    {
        var grid = new GameGrid(_width, _height);
        var randomizer = new UnityIndexRandomizer();

        _speed.SetLevel(0);
        _spawner.Initialize(grid, _speed, randomizer);
        _controller = new TetrominoController(_input, _spawner);

        _session = new GameSession(_spawner, grid);
        _session.GameOver += OnGameOver;
        _session.Start();
    }

    private void OnDestroy()
    {
        if (_session == null)
            return;

        _session.GameOver -= OnGameOver;
        _session.Dispose();
        _controller.Dispose();
    }

    private void OnGameOver() => Debug.Log("Game over");
}
