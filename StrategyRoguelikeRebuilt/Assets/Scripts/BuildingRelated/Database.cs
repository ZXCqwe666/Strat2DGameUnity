using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database instance;
    public BuildingData[] buildings;
    public ItemData[] items;

    private void Awake()
    {
        instance = this;
    }
}
