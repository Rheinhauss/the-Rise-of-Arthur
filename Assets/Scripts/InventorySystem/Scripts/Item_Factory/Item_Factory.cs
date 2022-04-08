using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemFactoryInterface
{
    public ItemType itemType { get; set; }
    public Item CreateItem(int amount);
}

public class Item_Factory
{
    private Dictionary<ItemType, ItemFactoryInterface> FactoryDict;

    private Item_Factory()
    {
        FactoryDict = new Dictionary<ItemType, ItemFactoryInterface>();
        FactoryDict.Add(ItemType.HealthPotion_Little, new ItemHP_Little_Factory());
        FactoryDict.Add(ItemType.Weapon_A, new Item_Weapon_A_Factory());
        FactoryDict.Add(ItemType.Weapon_B, new Item_Weapon_B_Factory());
    }
    private static Item_Factory _instance;
    public static Item_Factory Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new Item_Factory();
            }
            return _instance;
        }
    }
    public Item CreateItem(ItemType itemType, int amount = 1)
    {
        return FactoryDict[itemType].CreateItem(amount);
    }
}
