using UnityEngine;
using System.Collections;

public class LurePotion : MonoBehaviour {

    public enum LureType { level1, level2 }
    public LureType lureType;
    public GameObject prefabToSpawn0;
    public GameObject prefabToSpawn1;
    public GameObject prefabToSpawn2;
    public GameObject prefabToSpawn3;
    public GameObject getPrefabToSpawn()
    {
        int random = Mathf.RoundToInt(Random.Range(0f, 3f));
        switch (random){
            case 0:
                return prefabToSpawn0;
            case 1:
                return prefabToSpawn1;
            case 2:
                return prefabToSpawn2;
            default:
                return prefabToSpawn3;
            
        }
    }
}
