using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour {

    public enum SeedType {FingerRoot, MoonWort, RedMushroom, BrownMushroom, Nyteshaid}
    public Seed seedType;
    public GameObject prefabToSpawn;
    public GameObject getPrefabToSpawn()
    {
        return prefabToSpawn;
    }
}
