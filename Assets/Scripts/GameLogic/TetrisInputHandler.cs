using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public sealed class TetrisInputHandler : MonoBehaviour
{
    public event Action MoveLeftPressed;
    public event Action MoveRightPressed;
    public event Action RotatePressed;
    public event Action<bool> SoftDropChanged;

    private Vector2 _previous;

    private void OnMovement(InputValue value)
    {
        Vector2 current = value.Get<Vector2>();

        if (current.x < 0 && _previous.x >= 0)
            MoveLeftPressed?.Invoke();

        if (current.x > 0 && _previous.x <= 0)
            MoveRightPressed?.Invoke();

        if (current.y > 0 && _previous.y <= 0)
            RotatePressed?.Invoke();

        if (current.y < 0 && _previous.y >= 0)
            SoftDropChanged?.Invoke(true);

        if (current.y >= 0 && _previous.y < 0)
            SoftDropChanged?.Invoke(false);

        _previous = current;
    }
}