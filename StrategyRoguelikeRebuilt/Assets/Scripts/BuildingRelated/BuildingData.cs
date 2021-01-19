using UnityEngine;

[CreateAssetMenu (fileName = "buildingData", menuName = "new BuildingData")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public Vector2 buildingSize;
    public Sprite buildingSprite;
}