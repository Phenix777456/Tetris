using System;

public sealed class TetrominoController : IDisposable
{
    private readonly TetrisInputHandler _input;
    private readonly TetrominoSpawner _spawner;

    private Tetromino _current;
    private bool _isFastDrop;

    public TetrominoController(TetrisInputHandler input, TetrominoSpawner spawner)
    {
        _input = input;
        _spawner = spawner;

        _spawner.TetrominoSpawned += OnSpawned;
        _spawner.TetrominoLanded += OnLanded;
        _input.MoveLeftPressed += OnMoveLeft;
        _input.MoveRightPressed += OnMoveRight;
        _input.RotatePressed += OnRotate;
        _input.SoftDropChanged += OnSoftDropChanged;
    }

    public void Dispose()
    {
        _spawner.TetrominoSpawned -= OnSpawned;
        _spawner.TetrominoLanded -= OnLanded;
        _input.MoveLeftPressed -= OnMoveLeft;
        _input.MoveRightPressed -= OnMoveRight;
        _input.RotatePressed -= OnRotate;
        _input.SoftDropChanged -= OnSoftDropChanged;
    }

    private void OnSpawned(Tetromino tetromino)
    {
        _current = tetromino;
        _current.SetFastDrop(_isFastDrop);
    }

    private void OnLanded(Tetromino tetromino) => _current = null;

    private void OnMoveLeft() => _current.MoveLeft();
    private void OnMoveRight() => _current.MoveRight();
    private void OnRotate() => _current.Rotate();

    private void OnSoftDropChanged(bool value)
    {
        _isFastDrop = value;
        _current.SetFastDrop(value);
    }
}
