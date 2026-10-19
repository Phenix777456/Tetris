using System;

public sealed class GameSession : IDisposable
{
    private readonly TetrominoSpawner _spawner;
    private readonly GameGrid _grid;

    public event Action GameOver;
    public event Action<int> LinesCleared;

    public GameSession(TetrominoSpawner spawner, GameGrid grid)
    {
        _spawner = spawner;
        _grid = grid;
    }

    public void Start()
    {
        _spawner.TetrominoLanded += OnTetrominoLanded;
        TrySpawnNext();
    }

    public void Dispose()
    {
        _spawner.TetrominoLanded -= OnTetrominoLanded;
    }

    private void OnTetrominoLanded(Tetromino tetromino)
    {
        _grid.Occupy(tetromino.Cubes);

        int cleared = _grid.ClearFullLines();

        if (cleared > 0)
            LinesCleared?.Invoke(cleared);

        TrySpawnNext();
    }

    private void TrySpawnNext()
    {
        if (_spawner.TrySpawn() == false)
            GameOver?.Invoke();
    }
}
