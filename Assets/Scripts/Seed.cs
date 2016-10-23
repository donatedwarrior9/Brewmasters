using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour {

    public enum SeedType {FingerRoot, MoonWort, Nightshade, Pumpkin}
    public SeedType seedType;
    public GameObject prefabToSpawn;
    public GameObject getPrefabToSpawn()
    {
        return prefabToSpawn;
    }
}
