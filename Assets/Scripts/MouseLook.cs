using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Impostazioni Mouse")]
    public float mouseSensitivity = 400f;
    public Transform playerBody;
    
    float xRotation = 0f;

    void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 400f);

        // Blocca il cursore
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    public void LoadSensitivity()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 400f);
    }
}