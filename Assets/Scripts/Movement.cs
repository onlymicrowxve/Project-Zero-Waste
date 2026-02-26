using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    public CharacterController controller;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public KeyCode runKey = KeyCode.LeftShift;

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

    [Header("Controllo Terreno")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
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

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isMoving = (x != 0 || z != 0);
        Vector3 move = transform.right * x + transform.forward * z;

        if (currentStamina <= 0) canRun = false;
        else if (currentStamina >= minStaminaToRun) canRun = true;

        // Corsa o Camminata
        if (Input.GetKey(runKey) && isMoving && canRun)
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

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
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
}