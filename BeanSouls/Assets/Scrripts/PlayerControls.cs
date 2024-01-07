using UnityEngine;

public class PlayerControls : MonoBehaviour
{   
    //states of the player
    public Transform orientation;
    public Transform rotation;
    public Transform model;

    //isgrounded check
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;


    public Rigidbody rb;

    //main movement
    public float rotationSpeed;
    public float movementSpeed;
    public float groundDrag;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    //dashing variables
    public float dashForce;
    public float dashDuration;
    public float dashCooldown;

    public bool canDash;
    public bool isDashing;
    public bool isRunning;



    private void KeyInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Dash();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
    }
    
    void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //prevents speed from going a certain point
        if(flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = 7.5f;
            canDash = false;
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = 5f;
            canDash = true;
            isRunning = false;
        }
    }
    void Dash()
    {
        if (canDash && isRunning == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3 forceToApply = model.forward * dashForce;

                rb.AddForce(forceToApply, ForceMode.Impulse);

                Invoke(nameof(resetDash), dashDuration);
                isDashing = true;
            }
        }
    }
    void resetDash()
    {

    }
    
    void DashCooldown()
    {
        if (isDashing == true)
        {
            canDash = false;
            dashCooldown -= 1 * Time.deltaTime;
        }

        if (dashCooldown < 0)
        {
            canDash = true;
            dashCooldown = 1;
            isDashing = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate orientation
        Vector3 viewDir = model.position - new Vector3(transform.position.x, model.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        //move the player based off the camera
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
            model.forward = Vector3.Slerp(model.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        KeyInputs();

        //ground check
        grounded = Physics.Raycast(model.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        speedControl();
        DashCooldown();

        
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
}
