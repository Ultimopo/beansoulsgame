using UnityEngine;

public class CameraMovement : MonoBehaviour
{   
    //states of the player
    public Transform orientation;
    public Transform rotation;
    public Transform model;

    //isgrounded check
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    public Rigidbody rb;

    //main movement
    public float rotationSpeed;
    public float movementSpeed;
    public float groundDrag;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;


    private void KeyInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        speedControl();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
}
