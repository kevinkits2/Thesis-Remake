using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManagerEvents {

    public static event Func<Vector2> OnMovementVectorRequested;
    public static Vector2 GetMovementVector() => OnMovementVectorRequested?.Invoke() ?? Vector2.zero;

    public static event Func<Vector3> OnMouseWorldPositionRequested;
    public static Vector3 GetMouseWorldPosition() => OnMouseWorldPositionRequested?.Invoke() ?? Vector3.zero;
}
