using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Blacksmith : MonoBehaviour {

    private const string BLACKSMITH_IS_MOVING = "IsMoving";

    [SerializeField] private Transform wanderOrigin;
    [SerializeField] private float wanderingDistanceX = 1f;
    [SerializeField] private float wanderingDistanceY = 1f;
    [SerializeField] private float wanderingCooldown = 7f;
    [SerializeField] private float wanderSpeed = 0.2f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;

    private Vector2 wanderPosition;
    private Vector2 wanderDirection;
    private bool wanderingDisabled;
    private float wanderTimer;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        wanderPosition = GetRandomDestination();
    }

    private void Update() {
        DestinationCheck();
        FlipSprite();
     
        if (!wanderingDisabled) return;

        WanderingTimer();
    }

    private void FixedUpdate() {
        if (wanderingDisabled) return;

        Move();
    }

    #region Movement
    private void Move() {
        wanderDirection = (wanderPosition - (Vector2)transform.position).normalized;

        rb.MovePosition((Vector2)transform.position + Time.fixedDeltaTime * wanderSpeed * wanderDirection);
        animator.SetBool(BLACKSMITH_IS_MOVING, true);
    }

    private Vector2 GetRandomDestination() {
        float randomX = Random.Range(-wanderingDistanceX, wanderingDistanceX);
        float randomY = Random.Range(-wanderingDistanceY, wanderingDistanceY);

        return new Vector2(wanderOrigin.position.x + randomX, wanderOrigin.position.y + randomY);
    }

    private void DestinationCheck() {
        if (Vector3.Distance(transform.position, wanderPosition) < 0.05f) {
            wanderingDisabled = true;
            rb.velocity = Vector2.zero;
            animator.SetBool(BLACKSMITH_IS_MOVING, false);
        }
    }

    private void WanderingTimer() {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderingCooldown) {
            wanderingDisabled = false;
            wanderPosition = GetRandomDestination();
            wanderTimer = 0f;
        }
    }
    #endregion

    private void FlipSprite() {
        if (wanderDirection.x < 0) {
            spriteRenderer.flipX = true;
        }
        else if (wanderDirection.x > 0) {
            spriteRenderer.flipX = false;
        }
    }
}
