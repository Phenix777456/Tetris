using System.Collections.Generic;
using UnityEngine;

public sealed class TetrominoMover
{
    private readonly Transform _root;
    private readonly IReadOnlyList<Cube> _cubes;
    private readonly IGridQuery _grid;

    public TetrominoMover(Transform root, IReadOnlyList<Cube> cubes, IGridQuery grid)
    {
        _root = root;
        _cubes = cubes;
        _grid = grid;
    }

    public bool CanMove(Vector2Int direction)
    {
        foreach (Cube cube in _cubes)
        {
            if (_grid.IsCellFree(cube.Cell + direction) == false)
                return false;
        }

        return true;
    }

    public bool TryMove(Vector2Int direction)
    {
        if (CanMove(direction) == false)
            return false;

        _root.position += new Vector3(direction.x, direction.y);
        return true;
    }

    public bool TryRotate()
    {
        Vector2Int pivot = Vector2Int.RoundToInt(_root.position);

        foreach (Cube cube in _cubes)
        {
            Vector2Int offset = cube.Cell - pivot;
            var rotated = new Vector2Int(offset.y, -offset.x);

            if (!_grid.IsCellFree(pivot + rotated))
                return false;
        }

        _root.Rotate(0f, 0f, -90f);
        return true;
    }
}
