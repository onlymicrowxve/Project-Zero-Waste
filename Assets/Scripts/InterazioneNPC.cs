using UnityEngine;
using TMPro;

public class InterazioneNPC : MonoBehaviour
{
    public float distanzaInterazione = 4f;

    [Header("Trascina qui la UI")]
    public GameObject pannelloDialogo; 
    public TextMeshProUGUI testoSchermo;          

    [Header("Script da bloccare")]
    public MonoBehaviour scriptMovimento;
    public MonoBehaviour scriptTelecamera;

    private bool stoParlando = false;

    void Start()
    {
        if (pannelloDialogo != null) pannelloDialogo.SetActive(false);
    }

    void Update()
    {
        if (stoParlando)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                pannelloDialogo.SetActive(false);
                stoParlando = false;

                if (scriptMovimento != null) scriptMovimento.enabled = true;
                if (scriptTelecamera != null) scriptTelecamera.enabled = true;
            }
            return; 
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, distanzaInterazione))
        {
            if (hit.transform.CompareTag("NPC") && Input.GetKeyDown(KeyCode.E))
            {
                NPC npcColpito = hit.transform.GetComponent<NPC>();

                if (npcColpito != null)
                {
                    testoSchermo.text = npcColpito.fraseDaDire;
                    pannelloDialogo.SetActive(true);
                    stoParlando = true; 

                    if (scriptMovimento != null) scriptMovimento.enabled = false;
                    if (scriptTelecamera != null) scriptTelecamera.enabled = false;
                }
            }
        }
    }
}