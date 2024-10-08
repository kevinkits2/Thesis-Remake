using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Player animator parameters
    private const string PLAYER_IS_MOVING = "IsMoving";

    //Player variables
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerDashPower;

    [Tooltip("How long the dash lasts")]
    [SerializeField] private float playerDashingTime;
    [SerializeField] private float playerDashCooldown;

    //Player components
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Animator playerAnimator;
 
    private Rigidbody2D playerRigibody;
    private Collider2D playerCollider;

    //Misc
    private Vector2 moveVector;


    private void Awake() {
        playerRigibody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update() {
        GetMoveVector();
        FlipSprite();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        playerRigibody.velocity = new Vector2(moveVector.x, moveVector.y) * playerMoveSpeed;
    }

    private void GetMoveVector() {
        moveVector = InputManagerEvents.GetMovementVector();

        if (moveVector.sqrMagnitude > 0) {
            playerAnimator.SetBool(PLAYER_IS_MOVING, true);
        }
        else {
            playerAnimator.SetBool(PLAYER_IS_MOVING, false);
        }
    }

    private void FlipSprite() {
        Vector3 mousePosition = InputManagerEvents.GetMouseWorldPosition();

        if (mousePosition.x < transform.position.x) {
            playerSpriteRenderer.flipX = true;
        }
        else {
            playerSpriteRenderer.flipX = false;
        }
    }
}
