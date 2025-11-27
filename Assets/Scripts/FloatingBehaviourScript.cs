using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class FloatingBehaviourScript : MonoBehaviour
{
    private Vector2 pointPos;
    private Vector3 mousePoint;
    [SerializeField] private float mouseDist = 3;



    [Header("Flight Deck")]
    [SerializeField] float flySpeed = 5;
    
    [SerializeField] float offset = 5;
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

                //Vector2 targetY = new Vector2(transform.position.x, transform.position.y + offset);

                //transform.position = Vector2.Lerp(transform.position, targetY, lerpTimer);

                //transform.position = new Vector3(transform.position.x, newY, 0);
                //rb.AddForceY(flySpeed * offset);

                
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

    private void FixedUpdate()
    {
       

        if (flyActive && isHolding)
        {
            Flying();
        }

        OrbFollow(flyActive);
        Debug.Log(pointPos);
    }
   
    
    private void Flying()
    {
        Vector2 direction = mousePoint - transform.position;
        rb.AddForce(direction * flySpeed, ForceMode2D.Force);

        Debug.Log($"{rb.totalForce}");
        
        Debug.Log("kakaaaa");
        
    }

    private void OrbFollow(bool isFloating)
    {
        if (!isFloating) return;

        Vector3 orbMove = Vector3.Lerp(transform.position, mousePoint, lerpTimer);

        Vector2 dir = mousePoint - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);

        //rb.MovePosition(orbMove);
        //transform.localRotation = Quaternion.Euler(0, 0,mousePoint.z);
        //transform.Rotate(0, 0, mousePoint.x);

        //Vector2 orbPos = Vector2.Lerp(transform.position, pointPos, lerpTimer);
        //transform.position = Camera.main.ScreenToWorldPoint(mousePoint);
        /*if(orbPos != pointPos)
        {
            rb.MovePosition(pointPos);
        }*/

    }


    //INPUT HANDLING
    public void PointControls(InputAction.CallbackContext ctx)
    {
        pointPos = ctx.ReadValue<Vector2>();
        mousePoint = Camera.main.ScreenToWorldPoint(pointPos);
    }

}
