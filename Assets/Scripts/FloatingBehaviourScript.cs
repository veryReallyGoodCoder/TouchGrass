using System.Buffers.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class FloatingBehaviourScript : MonoBehaviour
{
    [SerializeField] float offset, jumpSmooth = 5;
    float targetY;
    [SerializeField] private float lerpTimer = 1.5f;

    private bool flyActive = false;
    private bool isHolding = false;

    private PlayerInput _input;

    private Rigidbody2D rb;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        if (_input == null)
        {
            Debug.Log("nope");
        }
        else
        {
            Debug.Log("yep");
        }

        var jumpAction = _input.actions["Jump"];

        jumpAction.performed += OnJumpPerformed;
        jumpAction.canceled += OnJumpCancelled;

    }

    

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        if(ctx.interaction is TapInteraction)
        {
            flyActive =! flyActive;

            if (flyActive)
            {
                rb.gravityScale = 0f;

                targetY = transform.position.y + offset;

                

                for(float i =0; i < lerpTimer; i += Time.deltaTime)
                {
                    float newY = Mathf.Lerp(transform.position.y, targetY, i);

                    //transform.position = new Vector3(transform.position.x, newY, 0);
                    rb.MovePosition(new Vector2(transform.position.x, newY));

                    //Debug.Log(i);
                }
            }
            else rb.gravityScale = 10f;

            Debug.Log($"fly mode {flyActive}");

        }

        if (ctx.interaction is HoldInteraction)
        {
            if (flyActive)
            {
                isHolding = true;
                Debug.Log("holding");
            }
        }
    }
    private void OnJumpCancelled(InputAction.CallbackContext ctx)
    {
        isHolding = false;
    }

    private void Update()
    {
       

        if (flyActive && isHolding)
        {
            Flying();
        }
    }

    private void Flying()
    {
        Debug.Log("kakaaaa");
        
    }

}
