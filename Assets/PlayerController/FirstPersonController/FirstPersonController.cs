using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
    using System.Net;
#endif

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;

    #region Camera Movement Variables

    public Camera playerCamera;

    public float fov = 60f;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 1f;
    public float maxLookAngle = 50f;

    // Crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private PlayerControls controls;


    private Vector2 lookInput;
    private Vector2 moveInput;

    #endregion

    #region Movement Variables

    public bool playerCanMove = true;
    public float walkSpeed = 4.2f;
    public float maxVelocityChange = 10f;

    // Internal Variables
    private bool isWalking = false;

    #region Jump

    public bool enableJump = true;
    public float jumpPower = 5f;

    // Internal Variables
    public bool isGrounded = false;

    #endregion

    #region Crouch

    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchHeight = .75f;
    public float speedReduction = .5f;

    // Internal Variables
    private bool isCrouched = false;
    private Vector3 originalScale;

    #endregion
    #endregion


    // Internal Variables
    private float timer = 0;

    

    private void Awake()
    {
        controls = new PlayerControls();

        rb = GetComponent<Rigidbody>();

        // Set internal variables
        playerCamera.fieldOfView = fov;
        originalScale = transform.localScale;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    float camRotation;

    private void Update()
    {
        #region Camera

        // Control camera movement
        if(cameraCanMove)
        {
            yaw += lookInput.x * mouseSensitivity * Time.deltaTime;
            pitch -= lookInput.y * mouseSensitivity * Time.deltaTime;

            // Clamp pitch
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            // Rotate player and camera
            transform.localEulerAngles = new Vector3(0, yaw, 0);
            Camera.main.transform.localEulerAngles = new Vector3(pitch, 0, 0);

        }
        #endregion

        CheckGround();

    }

    void FixedUpdate()
    {
        #region Movement

        if (playerCanMove)
        {
            Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y);

            // Checks if player is moving and is grounded
            isWalking = (targetVelocity.magnitude > 0.1f) && isGrounded;

            targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

            // Apply velocity change
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        #endregion
    }

    // Sets isGrounded based on a raycast sent straigth down from the player object
    private void CheckGround()
    {
        float checkDistance = 0.95f; // Slightly more than skin width
        Vector3 direction = Vector3.down;

        Debug.DrawRay(transform.position, direction * checkDistance, Color.red); // Debug visualization

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, checkDistance))
        {
            isGrounded = true;
            Debug.DrawRay(transform.position, direction * checkDistance, Color.green); // Turn green when grounded
            Debug.Log("Ground detected: " + hit.collider.gameObject.name);
        }
        else
        {
            isGrounded = false;
            Debug.Log("Not grounded!");
        }
    }



private void Jump()
    {
        Debug.Log("Jumping!");
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity to prevent stacking forces
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false; // Prevents double jumps
    }


    private void Crouch()
    {
        // Stands player up to full height
        // Brings walkSpeed back up to original speed
        if(isCrouched)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            walkSpeed /= speedReduction;

            isCrouched = false;
        }
        // Crouches player down to set height
        // Reduces walkSpeed
        else
        {
            transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
            walkSpeed *= speedReduction;

            isCrouched = true;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouched = false;
            Crouch();
        }
        else if (context.canceled) // When key is released
        {
            isCrouched = true;
            Crouch();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Grounded: " + isGrounded);
        if (isGrounded)
        {
            Jump();
        }
    }


    private void OnEnable()
    {
        controls.Enable();

        // Bind input actions
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;

        controls.Player.Look.performed += OnLook;
        controls.Player.Look.canceled += OnLook;

        controls.Player.Crouch.performed += OnCrouch;
        controls.Player.Crouch.canceled += OnCrouch;

        controls.Player.Jump.performed += OnJump;
        controls.Player.Jump.canceled += OnJump;
    }

    private void OnDisable()
    {
        controls.Disable();

        // Unbind input actions
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;

        controls.Player.Look.performed -= OnLook;
        controls.Player.Look.canceled -= OnLook;

        controls.Player.Crouch.performed -= OnCrouch;
        controls.Player.Crouch.canceled -= OnCrouch;

        controls.Player.Jump.performed -= OnJump;
        controls.Player.Jump.canceled -= OnJump;
    }
}



