using System.Collections.Generic;
using UnityEngine;

public sealed class GameGrid : IGridQuery
{
    private readonly Cube[,] _cells;

    public int Width { get; }
    public int Height { get; }

    public GameGrid(int width, int height)
    {
        Width = width;
        Height = height;
        _cells = new Cube[width, height];
    }

    public bool IsCellFree(Vector2Int cell)
    {
        if (cell.x < 0 || cell.x >= Width || cell.y < 0)
            return false;

        if (cell.y >= Height)
            return true;

        return _cells[cell.x, cell.y] == null;
    }

    public void Occupy(IEnumerable<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            Vector2Int cell = cube.Cell;

            if (IsInsideField(cell))
                _cells[cell.x, cell.y] = cube;
        }
    }

    private bool IsInsideField(Vector2Int cell) =>
        cell.x >= 0 && cell.x < Width && cell.y >= 0 && cell.y < Height;

    public int ClearFullLines()
    {
        int writeRow = 0;

        for (int row = 0; row < Height; row++)
        {
            if (IsRowFull(row))
            {
                RemoveRow(row);
                continue;
            }

            if (row != writeRow)
                MoveRow(row, writeRow);

            writeRow++;
        }

        return Height - writeRow;
    }

    private bool IsRowFull(int row)
    {
        for (int x = 0; x < Width; x++)
        {
            if (_cells[x, row] == null)
                return false;
        }

        return true;
    }

    private void RemoveRow(int row)
    {
        for (int x = 0; x < Width; x++)
        {
            _cells[x, row].Remove();
            _cells[x, row] = null;
        }
    }

    private void MoveRow(int from, int to)
    {
        for (int x = 0; x < Width; x++)
        {
            Cube cube = _cells[x, from];

            _cells[x, to] = cube;
            _cells[x, from] = null;

            if (cube != null)
                cube.MoveDown(from - to);
        }
    }
}
