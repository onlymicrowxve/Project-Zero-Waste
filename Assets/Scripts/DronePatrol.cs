using UnityEngine;

public class DronePatrol : MonoBehaviour
{
    [Header("Impostazioni Area di Volo")]
    public float raggioPattugliamento = 10f;
    public bool usaPosizioneInizialeComeCentro = true;
    public Vector3 centroManuale;

    [Header("Impostazioni Movimento")]
    public float velocitaVolo = 5f;
    public float velocitaRotazione = 2f;
    public float altezzaMinimaDaTerra = 3f;

    [Header("Evitamento Ostacoli")]
    [Tooltip("Lunghezza del 'laser' visivo. Quanto lontano guarda il drone per schivare?")]
    public float distanzaEvitamento = 4f;
    [Tooltip("Quali Layer contengono i palazzi da evitare?")]
    public LayerMask layerOstacoli;

    private Vector3 _centroPattugliamento;
    private Vector3 _destinazioneAttuale;
    private float _distanzaTolleranza = 1.5f;

    void Start()
    {
        if (usaPosizioneInizialeComeCentro) _centroPattugliamento = transform.position;
        else _centroPattugliamento = centroManuale;

        ScegliNuovaDestinazione();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distanzaEvitamento, Color.yellow);

        if (Physics.Raycast(transform.position, transform.forward, distanzaEvitamento, layerOstacoli))
        {

            ScegliNuovaDestinazione();
        }

        Vector3 direzione = _destinazioneAttuale - transform.position;

        if (direzione.magnitude <= _distanzaTolleranza)
        {
            ScegliNuovaDestinazione();
        }
        else
        {
            Quaternion rotazioneDesiderata = Quaternion.LookRotation(direzione);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotazioneDesiderata, velocitaRotazione * Time.deltaTime);
            transform.Translate(Vector3.forward * velocitaVolo * Time.deltaTime);
        }
    }

    void ScegliNuovaDestinazione()
    {
        Vector3 direzioneCasuale = Random.insideUnitSphere * raggioPattugliamento;
        _destinazioneAttuale = _centroPattugliamento + direzioneCasuale;
        
        if (_destinazioneAttuale.y < altezzaMinimaDaTerra)
        {
            _destinazioneAttuale.y = altezzaMinimaDaTerra;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Vector3 centroVisuale = usaPosizioneInizialeComeCentro ? transform.position : centroManuale;
        Gizmos.DrawSphere(centroVisuale, raggioPattugliamento);
    }
}