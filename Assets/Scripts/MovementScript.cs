using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public Vector2 moveInput;
    Vector3 movePlayer;

    public float moveSpeed = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        Vector2 moveDir = new Vector2(moveInput.x, 0);
        rb.linearVelocity = moveDir * moveSpeed;
    }


    public void PlayerMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

}
