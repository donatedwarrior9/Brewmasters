using UnityEngine;
using System.Collections;

public class EncounterArea : MonoBehaviour {

    public GameObject prefabToSpawn;
    public float minimumSpawnTime;
    public Animator anni;
	public Transform spawnPoint;
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
		Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
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

	public void EnteredTrap(Skeleton enemy)
	{
		anni.SetTrigger ("closeTrigger");
		Instantiate(enemy.myRewardPrefab, transform.position, Quaternion.identity);
	}

}
