using UnityEngine;
public class PickUpandDrop : MonoBehaviour
{
    public float pickupRange = 10f;
    public GameObject hand;
    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
        {
                if (hit.transform.CompareTag("Door"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DoorController door = hit.transform.GetComponent<DoorController>();
                    if (door == null)
                    {
                        door = hit.transform.GetComponentInParent<DoorController>();
                    }

                    if (door != null)
                    {
                        door.ToggleDoor();
                    }
                }
            }
            if (hand.transform.childCount < 1)
            {
            if (hit.transform.CompareTag("PickUp"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.position = hand.transform.position;
                    hit.transform.rotation = hand.transform.rotation;
                    hit.transform.parent = hand.transform;
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                }
            }
            if (hit.transform.CompareTag("ArmaDaTerra"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.position = hand.transform.position;
                    hit.transform.rotation = hand.transform.rotation;
                    hit.transform.parent = hand.transform;
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hand.transform.childCount > 0)
            {
                Transform item = hand.transform.GetChild(0);
                item.parent = null;
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.AddForce(transform.forward * 500f);
                }
            }
        }
    }
}
}

