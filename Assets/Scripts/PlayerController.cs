using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Liam Sutton
    
    This character controller is based from Dani's first person character controller tutorial
 */
public class PlayerController : MonoBehaviour
{
    public Transform playerCamera;
    public Transform playerOrientation;

    private Rigidbody rb;
    private float xRotation;
    private float mouseSensitivity = 50f;
    private float sensitivityMultiplier = 1f;

    public float moveSpeed = 4500f;
    public float maxSpeed = 20f;

    public float maxTeleporterDistance = 20f;
    public bool isGrounded;

    private bool isCancellingGrounded;
    public LayerMask whatIsGround;
    public float counterMovementStrength = 0.175f;
    private float threshold = 0.01f;

    private bool isReadyToJump = true;
    public bool isReadyToDash = true;

    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;
    public float dashForce = 550f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 0.5f;
    public float maxSlopeAngle = 45f;
    float xMov, yMov;

    public bool isJumping, isDashing;

    public bool dashExecuting = false;
    private Vector3 normalVector = Vector3.up;

    public Material interactable;

    public AudioClip dashAudio;

    // Reference to player scripts
    DashController dashController;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        dashController = GetComponent<DashController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dashAudio = GetComponent<AudioSource>().clip;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        GetPlayerInput();
        MoveCamera();
        // if (Input.GetKeyDown(KeyCode.LeftShift)) {
        //     if (isReadyToDash) {
        //         StartCoroutine("Dash");
        //     }
        // }
    }

    private void GetPlayerInput()
    {
        xMov = Input.GetAxisRaw("Horizontal");
        yMov = Input.GetAxisRaw("Vertical");
        isJumping = Input.GetButton("Jump");
    }

    private void MovePlayer()
    {
        // Extra gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 10);
        // Get the velocity relative to the players current look direction
        Vector2 magnitude = GetRelativeVelocity();
        float xMagnitude = magnitude.x;
        float yMagnitude = magnitude.y;

        // Apply counter movement to prevent unity from ruining the game
        ApplyCounterMovement(xMov, yMov, magnitude);

        if (isReadyToJump && isJumping) Jump();
        float maxSpeed = this.maxSpeed;

        // If speed on any axis is greater than maxSpeed, cancel the input
        if (xMov > 0 && xMagnitude > maxSpeed) xMov = 0;
        if (xMov < 0 && xMagnitude < -maxSpeed) xMov = 0;
        if (yMov > 0 && yMagnitude > maxSpeed) yMov = 0;
        if (yMov < 0 && yMagnitude < -maxSpeed) yMov = 0;

        float multiplier = 1f;
        float multiplierV = 1f;

        // When in the air the player will move at half speed
        if (!isGrounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        rb.AddForce(playerOrientation.transform.forward * yMov * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(playerOrientation.transform.right * xMov * moveSpeed * Time.deltaTime * multiplier);
    }

    private float desiredX;
    private void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime * sensitivityMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime * sensitivityMultiplier;

        Vector3 lookRotation = playerCamera.transform.localRotation.eulerAngles;
        desiredX = lookRotation.y + mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        playerOrientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void ApplyCounterMovement(float xMov, float yMov, Vector2 magnitude)
    {

        if (!isGrounded) return;

        if (Mathf.Abs(magnitude.x) > threshold && Mathf.Abs(xMov) < 0.05f || (magnitude.x < -threshold && xMov > 0) || (magnitude.x > threshold && xMov < 0))
        {
            rb.AddForce(moveSpeed * playerOrientation.transform.right * Time.deltaTime * -magnitude.x * counterMovementStrength);
        }

        if (Mathf.Abs(magnitude.y) > threshold && Mathf.Abs(yMov) < 0.05f || (magnitude.y < -threshold && yMov > 0) || (magnitude.y > threshold && yMov < 0))
        {
            rb.AddForce(moveSpeed * playerOrientation.transform.forward * Time.deltaTime * -magnitude.y * counterMovementStrength);
        }
    }
    private Vector2 GetRelativeVelocity()
    {
        float lookAngle = playerOrientation.transform.eulerAngles.y;
        float movementAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float shortestAngle = Mathf.DeltaAngle(lookAngle, movementAngle);
        float v = 90f - shortestAngle;
        
        float magnitude = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;

        float yMagnitude = magnitude * Mathf.Cos(shortestAngle * Mathf.Deg2Rad);
        float xMagnitude = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMagnitude, yMagnitude);
    }

    private void Jump()
    {
        if (isGrounded && isReadyToJump)
        {
            isReadyToJump = false;
            

            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            Vector3 velocity = rb.velocity;
            if (rb.velocity.y < 0.5f)
            {
                rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
            }
            else if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector3(velocity.x, velocity.y / 2, velocity.z);
            }

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private bool IsFloor(Vector3 vector)
    {
        float angle = Vector3.Angle(Vector3.up, vector);
        return angle < maxSlopeAngle;
    }

    // TODO: understand some of this shit
    private void OnCollisionStay(Collision other)
    {
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        // Iterate over every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;

            if (IsFloor(normal))
            {
                isGrounded = true;
                dashController.SendMessage("ResetDash");
                isCancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        float delay = 3f;
        if (!isCancellingGrounded)
        {
            isCancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void ResetJump()
    {
        isReadyToJump = true;
    }

    private void ResetDash()
    {
        isReadyToDash = true;
        GameObject.Find("HUD").GetComponent<UIController>().text.enabled = true;
    }
    private void StopGrounded()
    {
        isGrounded = false;
    }
}
