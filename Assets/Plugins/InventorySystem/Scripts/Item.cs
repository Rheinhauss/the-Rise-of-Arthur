using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类别
/// </summary>
public enum ItemType
{
    Sword,
    HealthPotion,
    ManaPotion,
    Coin,
    Medkit
}
/// <summary>
/// 物品
/// </summary>
[System.Serializable]
public class Item
{
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        return ItemAssets.Instance.ItemSpriteDict[itemType];
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
                return true;
            case ItemType.Sword:
            case ItemType.Medkit:
                return false;
        }
    }

}
