using UnityEngine;

public class BuildingDatabase : MonoBehaviour
{
    public static BuildingDatabase instance;
    public BuildingData[] buildingDatabase;

    private void Awake()
    {
        instance = this;
    }
}