using UnityEngine;

public sealed class Cube : MonoBehaviour
{
    public Vector2Int Cell => Vector2Int.RoundToInt(transform.position);

    public void DetachFromParent() => transform.SetParent(null, worldPositionStays: true);

    public void MoveDown(int cells) => transform.position += Vector3.down * cells;

    public void Remove() => Destroy(gameObject);
}
