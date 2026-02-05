using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float heightOffset = 1f;
    public GameObject Salire;   // Trascina qui il testo/UI "Sali"
    public GameObject Scendere; // Trascina qui il testo/UI "Scendi"

    void Update()
    {
        // 1. RESET: Nascondi entrambe le scritte all'inizio di ogni frame.
        // Se non guardi nulla, devono sparire.
        if(Salire != null) Salire.SetActive(false);
        if(Scendere != null) Scendere.SetActive(false);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f))
        {
            // Controlliamo subito se è un teletrasporto
            if (hit.transform.CompareTag("Teleport"))
            {
                TeleportPad pad = hit.transform.GetComponent<TeleportPad>();

                if (pad != null && pad.destination != null)
                {
                    // --- LOGICA SALIRE/SCENDERE ---
                    // Confrontiamo l'altezza (Y) della destinazione con la nostra
                    if (pad.destination.position.y > transform.position.y)
                    {
                        // Se la destinazione è più in alto:
                        if(Salire != null) Salire.SetActive(true);
                    }
                    else
                    {
                        // Se la destinazione è più in basso (o uguale):
                        if(Scendere != null) Scendere.SetActive(true);
                    }
                    // ------------------------------

                    // Input per il teletrasporto
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        CharacterController controller = GetComponent<CharacterController>();

                        if (controller != null) controller.enabled = false;

                        transform.position = pad.destination.position + (Vector3.up * heightOffset);
                        transform.rotation = pad.destination.rotation;

                        if (controller != null) controller.enabled = true;

                        Debug.Log("Teletrasporto ESEGUITO a: " + pad.destination.name);
                    }
                }
            }
        }
    }
}