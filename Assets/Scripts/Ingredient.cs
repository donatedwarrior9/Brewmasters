using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour {

	public enum IngredientType { GoldenApple, RedApple, GreenApple, Worm, Fingerroot, Moonwort, RedMushroom, BrownMushroom, ZombieArm, Femur, Pumpkin, Gold, NightShade, NewtEye, DragonScale, TrollBlood, UnicornTear, CWStir, CCWStir }
	public IngredientType type;

    Ingredient(IngredientType i)
    {
        type = i;
    }
}
