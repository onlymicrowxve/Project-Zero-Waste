using UnityEngine;

public class ColorWeapon : MonoBehaviour
{
    [Header("Stato")]
    public bool isHeld = false; // SE VERO = SPARA. SE FALSO = NON SPARA.

    [Header("Statistiche")]
    public int maxAmmo = 10;
    public int currentAmmo;
    public float fireRate = 0.2f;
    public float bulletSpeed = 40f;

    [Header("Riferimenti")]
    public Camera fpsCamera;       
    public Transform firePoint;     
    public GameObject bulletPrefab; 
    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isHeld == false) return;
        else if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            if (currentAmmo > 0)
            {
                nextTimeToFire = Time.time + fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        if (muzzleFlash != null) muzzleFlash.Play();
        RaycastHit hit;
        Vector3 targetPoint;
        if (fpsCamera != null && Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 100f))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = firePoint.position + firePoint.forward * 100f;
        }

        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
    }
}