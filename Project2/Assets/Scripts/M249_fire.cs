using UnityEngine;

public class M249_fire : MonoBehaviour
{
    public AudioClip soundClips;
    public AudioSource firingSound;
    public float damage = 20f;
    public float range = 100f;
    public int magazineCapacity = 8;
    public int loadedRounds = 8;
    public float fireRate = 10f;
    private float timeToFire = 0f;

    public Camera mainCam;
    [SerializeField] private ParticleSystem terrainImpactEffect = null;
    [SerializeField] private ParticleSystem enemyImpactEffect = null;
    [SerializeField] private ParticleSystem muzzleFlash = null;
    LayerMask enemyMask;
    LayerMask wallsMask;

    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemies");
        wallsMask = LayerMask.GetMask("GameArea");
        firingSound.clip = soundClips;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shoot") && Time.time >= timeToFire)
        {
            if (loadedRounds <= 0)
            {
                loadedRounds = magazineCapacity;
            }
            else
            {
                timeToFire = Time.time + 1f / fireRate;
                loadedRounds--;
                firingSound.PlayOneShot(soundClips);

                //unfortunately muzzle flashes had to be disabled
                //due to a strange bug that caused the particle system to
                //go missing at random, crashing the whole fucking script with it

                //Instantiate(muzzleFlash);
                //muzzleFlash.Play();

                RaycastHit hit;
                if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range, enemyMask))
                {

                    ParticleSystem blood = Instantiate(enemyImpactEffect, hit.point, Quaternion.identity);
                    hit.transform.SendMessageUpwards("ReceiveHit", damage);
                }
                else
                {

                }

            }
        }
    }
}
