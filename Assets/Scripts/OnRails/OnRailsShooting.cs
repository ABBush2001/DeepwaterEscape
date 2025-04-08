using UnityEngine;

public class OnRailsShooting : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;

    public float bulletSpeed;

    public AudioClip gunShotSound;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = gunShotSound;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bulletInst = Instantiate(bullet, new Vector3(bulletSpawn.position.x, bulletSpawn.position.y + 2, bulletSpawn.position.z), bulletSpawn.rotation);
            bulletInst.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
            audioSource.PlayOneShot(gunShotSound);
        }
    }

}
