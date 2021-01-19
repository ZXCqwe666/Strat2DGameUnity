using UnityEngine;

[CreateAssetMenu (fileName = "buildingData", menuName = "new BuildingData")]
public class BuildingData : ScriptableObject
{
    public int id;
    public string buildingName;
    public Vector2 buildingSize;
    public Sprite buildingSprite;

    public float impactRadius;
    public int[] ids;
    public float[] efficiencyImpacts;
    public int[] itemCostId;
    public int[] itemCostAmount;
}
