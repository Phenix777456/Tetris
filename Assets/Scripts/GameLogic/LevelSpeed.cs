using UnityEngine;

[CreateAssetMenu(menuName = "Tetris/Level Speed")]
public sealed class LevelSpeed : ScriptableObject, ISpeedProvider
{
    [SerializeField] private float[] _delaysByLevel = { 0.8f, 0.7f, 0.6f, 0.5f, 0.4f };

    public int Level { get; private set; }

    public float StepDelay =>
        _delaysByLevel[Mathf.Min(Level, _delaysByLevel.Length - 1)];

    public void SetLevel(int level) => Level = Mathf.Max(0, level);
}
