using UnityEngine;

public sealed class UnityIndexRandomizer : IIndexRandomizer
{
    public int Next(int count) => Random.Range(0, count);
}
