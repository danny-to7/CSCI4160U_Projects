using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] private Transform camera = null;

    [SerializeField] private ParticleSystem terrainImpactEffect = null;
    [SerializeField] private ParticleSystem enemyImpactEffect = null;
    [SerializeField] private Text score = null;

    private float range = 200f;
    private int hitCount = 0;

    void Update() {
        /*
        if (Input.GetButtonDown("Shoot")) {
            RaycastHit hit;

            LayerMask enemyMask = LayerMask.GetMask("Enemies");
            LayerMask wallsMask = LayerMask.GetMask("GameArea");

            if (Physics.Raycast(camera.position, camera.forward, out hit, range, enemyMask)) {
                Debug.Log("Shot enemy: " + hit.collider.name + " at " + hit.point);
                ParticleSystem sparks = Instantiate(enemyImpactEffect, hit.point, Quaternion.identity);
                hit.transform.SendMessageUpwards("ReceiveHit");
                hitCount += 1;
                score.text = hitCount.ToString();

            } else if (Physics.Raycast(camera.position, camera.forward, out hit, range, wallsMask)) {
                Debug.Log("Shot the wall: " + hit.collider.name);
                ParticleSystem dirtKick = Instantiate(terrainImpactEffect, hit.point, Quaternion.identity);
            }
        }
        */
    }
}
