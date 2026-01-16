using UnityEngine;
using UnityEngine.EventSystems; // Necessario per rilevare il mouse sulla UI

public class SkyboxHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Skybox da mostrare")]
    public Material hoverSkybox; // Trascina qui il materiale della Skybox speciale

    private Material defaultSkybox; // Per ricordare quella originale

    void Start()
    {
        // Salva la skybox attuale appena parte il gioco
        defaultSkybox = RenderSettings.skybox;
    }

    // Quando il mouse ENTRA nel bottone
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSkybox != null)
        {
            RenderSettings.skybox = hoverSkybox;
            DynamicGI.UpdateEnvironment(); // Aggiorna le luci ambientali
        }
    }

    // Quando il mouse ESCE dal bottone
    public void OnPointerExit(PointerEventData eventData)
    {
        // Ripristina la skybox originale
        RenderSettings.skybox = defaultSkybox;
        DynamicGI.UpdateEnvironment(); // Aggiorna le luci ambientali
    }
}