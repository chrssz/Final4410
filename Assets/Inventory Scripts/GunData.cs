using UnityEngine;

[CreateAssetMenu(fileName = "GunData.asset", menuName = "Inventory/Gun Data")]
public class GunData : ScriptableObject
{
    public string GunName; // Descriptive name for display
    public Sprite GunImage; // Image for inventory display
    public GameObject GunPrefab; // Reference to the actual gun prefab
}
