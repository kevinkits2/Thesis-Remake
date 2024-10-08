using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private PlayerControls playerControls;


    private void Awake() {
        playerControls = new PlayerControls();
        playerControls.Enable();

        InputManagerEvents.OnMovementVectorRequested += HandleMovementVectorRequested;
        InputManagerEvents.OnMouseWorldPositionRequested += HandleMouseWorldPositionRequested;
    }

    private void OnDestroy() {
        playerControls.Disable();
        InputManagerEvents.OnMovementVectorRequested -= HandleMovementVectorRequested;
        InputManagerEvents.OnMouseWorldPositionRequested -= HandleMouseWorldPositionRequested;
    }

    private Vector2 HandleMovementVectorRequested() {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    private Vector3 HandleMouseWorldPositionRequested() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = 0f;

        return mousePosition;
    }
}
