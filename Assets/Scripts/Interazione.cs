using UnityEngine;
using TMPro;

public class Interazione : MonoBehaviour
{
    public float distanzaInterazione = 5f;

    [Header("Trascina qui la UI")]
    public GameObject pannelloDialogo; 
    public TextMeshProUGUI testoSchermo;    

    [Header("Riferimenti")]
    public GameObject hand;      

    [Header("Script da bloccare")]
    public MonoBehaviour scriptMovimento;
    public MonoBehaviour scriptTelecamera;
    public MonoBehaviour scriptRaccogliOggetti;

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
                if (scriptRaccogliOggetti != null) scriptRaccogliOggetti.enabled = true;

                if (hand != null)
                {
                    ColorWeapon arma = hand.GetComponentInChildren<ColorWeapon>();
                    if (arma != null) arma.enabled = true;
                }
            }
            return; 
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, distanzaInterazione))
            {
                Debug.Log("Raycast ha colpito: " + hit.transform.name + " | Tag: " + hit.transform.tag);
                if (hit.transform.CompareTag("NPC"))
                {
                    NPC npcColpito = hit.transform.GetComponent<NPC>();

                    if (npcColpito != null)
                    {
                        testoSchermo.text = npcColpito.fraseDaDire;
                        pannelloDialogo.SetActive(true);
                        stoParlando = true; 

                        if (scriptMovimento != null) scriptMovimento.enabled = false;
                        if (scriptTelecamera != null) scriptTelecamera.enabled = false;
                        if (scriptRaccogliOggetti != null) scriptRaccogliOggetti.enabled = false;

                        if (hand != null)
                        {
                            ColorWeapon arma = hand.GetComponentInChildren<ColorWeapon>();
                            if (arma != null) arma.enabled = false;
                        }
                    }
                }

                if(hit.transform.CompareTag("Leva"))
                    {
                        Debug.Log("Tag Leva trovato!");
                        Leva levaColpita = hit.transform.GetComponentInParent<Leva>();
                        Debug.Log("Script Leva trovato: " + (levaColpita != null));
            
                        if (levaColpita != null)
                            levaColpita.AttivaLeva();
                    }

                }
            }
        }
    }
