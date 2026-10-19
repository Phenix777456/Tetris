using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TetrominoState
{
    Moving,
    Landed
}

public sealed class Tetromino : MonoBehaviour
{
    [SerializeField] private Cube[] _cubes;
    [SerializeField] private float _lockDelay = 0.5f;

    private TetrominoMover _mover;
    private ISpeedProvider _speed;

    public TetrominoState State { get; private set; } = TetrominoState.Moving;
    public IReadOnlyList<Cube> Cubes => _cubes;
    private const float FastDropFactor = 0.1f;
    private bool _isFastDrop;


    public event Action<Tetromino> Landed;

    public void Initialize(IGridQuery grid, ISpeedProvider speed)
    {
        _mover = new TetrominoMover(transform, _cubes, grid);
        _speed = speed;
        StartCoroutine(FallRoutine());
    }

    public void MoveLeft()
    {
        if (State == TetrominoState.Moving)
            _mover.TryMove(Vector2Int.left);
    }

    public void MoveRight() 
    { 
        if (State == TetrominoState.Moving)
            _mover.TryMove(Vector2Int.right);
    }

    public void Rotate() 
    {
        if (State == TetrominoState.Moving)
            _mover.TryRotate();
    }

    public void SetFastDrop(bool value) => _isFastDrop = value;

    private IEnumerator FallRoutine()
    {
        while (State == TetrominoState.Moving)
        {
            float delay = _speed.StepDelay;

            if (_isFastDrop)
                delay *= FastDropFactor;

            yield return new WaitForSeconds(delay);

            if (_mover.TryMove(Vector2Int.down))
                continue;

            yield return new WaitForSeconds(_lockDelay);

            if (_mover.CanMove(Vector2Int.down) == false)
                Land();
        }
    }

    private void Land()
    {
        State = TetrominoState.Landed;

        foreach (Cube cube in _cubes)
            cube.DetachFromParent();

        Landed?.Invoke(this);
    }
}
