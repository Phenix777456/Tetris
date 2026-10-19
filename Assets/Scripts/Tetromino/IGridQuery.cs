using UnityEngine;

public interface IGridQuery
{
    bool IsCellFree(Vector2Int cell);
}
