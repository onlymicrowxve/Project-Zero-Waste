using UnityEngine;

public class PaintBullet : MonoBehaviour
{
    public GameObject paintSplatPrefab;

    void OnCollisionEnter(Collision collision)
    {

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);

        // Crea la macchia
        GameObject splat = Instantiate(paintSplatPrefab, contact.point + contact.normal * 0.01f, Quaternion.LookRotation(contact.normal));
        
        // Pulizia
        Destroy(splat, 10f);
        Destroy(gameObject);
    }
}