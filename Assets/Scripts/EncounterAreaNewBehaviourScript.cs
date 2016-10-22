using UnityEngine;
using System.Collections;

public class EncounterArea : MonoBehaviour {

    public GameObject prefabToSpawn;
    public float minimumSpawnTime;
    public void SetTrap(GameObject lure)
    {
        prefabToSpawn = lure.GetComponent<LurePotion>().getPrefabToSpawn();
        Destroy(lure);
        //Invoke("Grow", minimumSpawnTime + Random.Range(minimumSpawnTime / 2, minimumSpawnTime * 1.5f));
        StartCoroutine(delayedGrow(minimumSpawnTime, prefabToSpawn));
    }
    IEnumerator delayedGrow(float delay, GameObject toSpawn)
    {
        yield return new WaitForSeconds(delay);
        spawn();
    }
    void spawn()
    {
        int randomNumToSpawn = Random.Range(1, 5);
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
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
