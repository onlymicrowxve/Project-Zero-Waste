using UnityEngine;

public class Leva : MonoBehaviour
{
    public Transform leverTransform;
    public GameObject muroPortale; 
    public GameObject freccia;

    private bool attivata = false;

   public void AttivaLeva()
{
    if (attivata) return;
    attivata = true;

    leverTransform.localEulerAngles = new Vector3(30f, 0f, 0f);

    if (muroPortale != null) muroPortale.SetActive(true);
    if (freccia != null) freccia.SetActive(true);

    Debug.Log("Leva attivata, portale aperto!");
}
}

