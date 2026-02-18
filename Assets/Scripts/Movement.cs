using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    public CharacterController controller;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float crouchSpeed = 3f;

    [Header("Impostazioni Stamina")]
    public Slider staminaSlider;
    public float maxStamina = 100f;
    public float staminaDrain = 15f; 
    public float staminaRegen = 10f;
    public float minStaminaToRun = 20f;

    private float currentStamina;
    private bool canRun = true;

    [Header("Fisica Salto")]
    public float gravity = -19.62f;
    public float jumpHeight = 3f;

    [Header("Impostazioni Crouch")]
    public Transform playerCamera;
    public float normalHeight = 2f;
    public float crouchHeight = 1f;
    public float camStandY = 1.6f; 
    public float camCrouchY = 0.8f;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode runKey = KeyCode.LeftShift;
    public Transform ceilingCheck;

    [Header("Controllo Terreno")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isCrouching = false;
    float currentSpeed;

    void Start()
    {
        currentStamina = maxStamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }

    void Update()
    {
        if (playerCamera == null) return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isMoving = (x != 0 || z != 0);
        Vector3 move = transform.right * x + transform.forward * z;

        if (currentStamina <= 0) canRun = false;
        else if (currentStamina >= minStaminaToRun) canRun = true;

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
            RegenStamina();
        }
        else if (Input.GetKey(runKey) && isMoving && canRun)
        {
            currentSpeed = runSpeed;
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            currentSpeed = walkSpeed;
            RegenStamina();
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        if (staminaSlider != null) staminaSlider.value = currentStamina;

        if (Input.GetKeyDown(crouchKey)) 
            StartCrouch();
        if (Input.GetKeyUp(crouchKey)) 
            StopCrouch();

        if (playerCamera != null)
        {

            float targetY = isCrouching ? camCrouchY : camStandY;

            Vector3 camPos = playerCamera.localPosition;
            camPos.y = Mathf.Lerp(camPos.y, targetY, 10f * Time.deltaTime);
            playerCamera.localPosition = camPos;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RegenStamina()
    {
        if (currentStamina < maxStamina)
            currentStamina += staminaRegen * Time.deltaTime;
    }

    void StartCrouch()
    {
        controller.height = crouchHeight;
        controller.center = new Vector3(0, -0.5f, 0);
        isCrouching = true;
    }

    void StopCrouch()
    {
        if (!Physics.Raycast(ceilingCheck.position, Vector3.up, 1.5f))
        {
            controller.height = normalHeight;
            controller.center = Vector3.zero; 
            isCrouching = false;
        }
    }
}