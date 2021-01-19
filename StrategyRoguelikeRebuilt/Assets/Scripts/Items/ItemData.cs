using UnityEngine;

[CreateAssetMenu(fileName = "itemData", menuName = "new itemData")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite itemSprite;
}
