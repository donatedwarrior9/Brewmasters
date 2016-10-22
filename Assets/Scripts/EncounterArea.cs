using UnityEngine;
using System.Collections;

public class EncounterArea : MonoBehaviour {

    public GameObject prefabToSpawn;
    public float minimumSpawnTime;
    public Animator anni;

    public void SetTrap(GameObject lure)
    {
        prefabToSpawn = lure.GetComponent<LurePotion>().getPrefabToSpawn();
        Destroy(lure);
        //Invoke("Grow", minimumSpawnTime + Random.Range(minimumSpawnTime / 2, minimumSpawnTime * 1.5f));
        StartCoroutine(delayedSpawn(minimumSpawnTime, prefabToSpawn));
    }
    IEnumerator delayedSpawn(float delay, GameObject toSpawn)
    {
        yield return new WaitForSeconds(delay);
        spawn();
    }
    void spawn()
    {
        Vector3 spawnpoint = new Vector3(33f, .1f, 23f);
        Instantiate(prefabToSpawn, spawnpoint, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        // If not a seed, return
        if (other.GetComponent<LurePotion>() == null)
        {
            return;
        }
        SetTrap(other.gameObject);
    }
}
