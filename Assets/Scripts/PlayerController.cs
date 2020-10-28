using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public bool isGrounded;

    public float counterMovementStrength = 0.175f;
    private float threshold = 0.01f;

    float xMov, yMov;

    private float desiredX;

    public AudioClip dashAudio;

    // Reference to player scripts
    DashController dashController;
    JumpController jumpController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dashController = GetComponent<DashController>();
        jumpController = GetComponent<JumpController>();
        dashAudio = GetComponent<AudioSource>().clip;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        GetPlayerInput();
        MoveCamera();
    }

    private void GetPlayerInput()
    {
        xMov = Input.GetAxisRaw("Horizontal");
        yMov = Input.GetAxisRaw("Vertical");
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

    private void OnCollisionEnter(Collision other)
    {

        // Is Grounded
        if (other.gameObject.CompareTag("Ground"))
        {
            dashController.SendMessage("ResetDash");
            dashController.SendMessage("ResetGrounded");
            jumpController.SendMessage("ResetGrounded");
            jumpController.SendMessage("ResetJump");
            isGrounded = true;

        }
        else
        {
            Debug.Log(other.gameObject.layer.ToString());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // Player has left the grounded state
            // TODO: definitally a better way to do this... maybe with subclassing or an interface?
            isGrounded = false;
            jumpController.SendMessage("LeftGround");
            dashController.SendMessage("LeftGround");
        }
    }
}
