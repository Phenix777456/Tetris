using System;
using UnityEngine;

public sealed class TetrominoSpawner : MonoBehaviour
{
    [SerializeField] private Tetromino[] _prefabs;
    [SerializeField] private Transform _spawnPoint;

    private IGridQuery _grid;
    private ISpeedProvider _speed;
    private IIndexRandomizer _randomizer;

    public event Action<Tetromino> TetrominoLanded;

    public event Action<Tetromino> TetrominoSpawned;

    public void Initialize(IGridQuery grid, ISpeedProvider speed, IIndexRandomizer randomizer)
    {
        _grid = grid;
        _speed = speed;
        _randomizer = randomizer;
    }

    public bool TrySpawn()
    {
        Tetromino prefab = _prefabs[_randomizer.Next(_prefabs.Length)];
        Tetromino instance = Instantiate(prefab, _spawnPoint.position, Quaternion.identity);

        if (AreSpawnCellsFree(instance) == false)
        {
            Destroy(instance.gameObject);
            return false;
        }

        instance.Landed += OnLanded;
        instance.Initialize(_grid, _speed);

        TetrominoSpawned?.Invoke(instance);
        return true;
    }

    private bool AreSpawnCellsFree(Tetromino tetromino)
    {
        foreach (Cube cube in tetromino.Cubes)
        {
            if (_grid.IsCellFree(cube.Cell) == false)
                return false;
        }

        return true;
    }

    private void OnLanded(Tetromino tetromino)
    {
        tetromino.Landed -= OnLanded;

        TetrominoLanded?.Invoke(tetromino);

        Destroy(tetromino.gameObject);
    }
}
