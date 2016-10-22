using UnityEngine;
using System.Collections;

public class EncounterArea : MonoBehaviour {

    public GameObject prefabToSpawn;
    public float minimumSpawnTime;
    public void SetTrap(GameObject lure)
    {
        prefabToSpawn = lure.GetComponent<LurePotion>;
        Destroy(lure);
        //Invoke("Grow", minimumSpawnTime + Random.Range(minimumSpawnTime / 2, minimumSpawnTime * 1.5f));
        StartCoroutine(delayedGrow(minimumSpawnTime + Random.Range(minimumSpawnTime / 2, minimumSpawnTime * 1.5f), prefabToSpawn));
    }
    IEnumerator delayedGrow(float delay, GameObject toSpawn)
    {
        yield return new WaitForSeconds(delay);
        Grow();
    }
    void Grow()
    {
        int randomNumToSpawn = Random.Range(1, 5);
        for (int i = 0; i < randomNumToSpawn; i++)
        {
            Vector3 random = new Vector3(Random.value * 2 - 1, 0.01f, Random.value * 2 - 1);
            random *= growRadius;
            Instantiate(prefabToSpawn, transform.position + random, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If not a seed, return
        if (other.GetComponent<Seed>() == null)
        {
            return;
        }
        PlantSeed(other.gameObject);
    }
}
