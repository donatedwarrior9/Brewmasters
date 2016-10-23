using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour {

	public GameObject respawnEffectsPrefab;
	public GameObject vanishEffectsPrefab;
	public IngredientRespawnPoint[] ingredientRespawns;
	public NameRespawnPoint[] nameRespawns;
	public Transform defaultSpawnPoint;
	public void Respawn(GameObject toRespawn)
	{
		Instantiate (vanishEffectsPrefab, toRespawn.transform.position, Quaternion.identity);
		Transform spawnAt = GetSpawnPoint (toRespawn);
		if (!spawnAt)
			return;
		Instantiate (vanishEffectsPrefab, spawnAt.transform.position, Quaternion.identity);
		toRespawn.transform.position = spawnAt.transform.position;
		toRespawn.transform.rotation = spawnAt.transform.rotation;
		Rigidbody toRespawnRigidbody = toRespawn.GetComponent<Rigidbody> ();
		if (toRespawnRigidbody)
			toRespawnRigidbody.velocity = Vector3.zero;
	}

	Transform GetSpawnPoint(GameObject toRespawn)
	{
		Ingredient ingredient = toRespawn.GetComponent<Ingredient> ();
		if (ingredient) {
			foreach (IngredientRespawnPoint point in ingredientRespawns) {
				if (point.ingredientType == ingredient.type) {
					return point.respawnPoint;
				}
			}
		} else if (toRespawn.GetComponent<RespawnMe>()) {
			foreach (NameRespawnPoint point in nameRespawns) {
				if (toRespawn.name.Contains(point.nameContains)) {
					return point.respawnPoint;
				}
			}
			return defaultSpawnPoint;
		}
		return null;
	}

	[System.Serializable]
	public struct IngredientRespawnPoint {
		public Ingredient.IngredientType ingredientType;
		public Transform respawnPoint;
	}
	[System.Serializable]
	public struct NameRespawnPoint {
		public string nameContains;
		public Transform respawnPoint;
	}
}
