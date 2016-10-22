using UnityEngine;
using System.Collections;

public class Patch : MonoBehaviour {

	public GameObject prefabToSpawn;
	public float minimumSpawnTime;
	public float growRadius = 0.1f;
	public void PlantSeed(GameObject seed)
	{
		Destroy (seed);
		Invoke("Grow", minimumSpawnTime + Random.Range(minimumSpawnTime / 2, minimumSpawnTime * 1.5f));
	}

	void Grow()
	{
		int randomNumToSpawn = Random.Range (1, 5);
		for (int i = 0; i < randomNumToSpawn; i++)
		{
			Vector3 random = new Vector3 (Random.value * 2 - 1, 0.01f, Random.value * 2 - 1);
			random *= growRadius;
			Instantiate(prefabToSpawn, transform.position + random, Quaternion.identity);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// If not a seed, return
		if ("other" != "seed") {
			return;
		}
		PlantSeed (other.gameObject);
	}
}
