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
        FactoryDict.Add(ItemType.HealthPotion_Big, new ItemHP_Big_Factory());
        FactoryDict.Add(ItemType.Weapon_A, new Item_Weapon_A_Factory());
        FactoryDict.Add(ItemType.Weapon_B, new Item_Weapon_B_Factory());
        FactoryDict.Add(ItemType.Weapon_C, new Item_Weapon_C_Factory());
        FactoryDict.Add(ItemType.Weapon_D, new Item_Weapon_D_Factory());
        FactoryDict.Add(ItemType.Head_Armor_A, new Item_HeadArmor_A_Factory());
        FactoryDict.Add(ItemType.Head_Armor_B, new Item_HeadArmor_B_Factory());
        FactoryDict.Add(ItemType.Torso_Armor_A, new Item_TorsoArmor_A_Factory());
        FactoryDict.Add(ItemType.Torso_Armor_B, new Item_TorsoArmor_B_Factory());
        FactoryDict.Add(ItemType.Torso_Armor_C, new Item_TorsoArmor_C_Factory());
        FactoryDict.Add(ItemType.Torso_Armor_D, new Item_TorsoArmor_D_Factory());
        FactoryDict.Add(ItemType.Leg_Armor_A, new Item_LegArmor_A_Factory());
        FactoryDict.Add(ItemType.Leg_Armor_B, new Item_LegArmor_B_Factory());
        FactoryDict.Add(ItemType.Foot_Armor_A, new Item_FootArmor_A_Factory());
        FactoryDict.Add(ItemType.Foot_Armor_B, new Item_FootArmor_B_Factory());
        FactoryDict.Add(ItemType.Coin_A, new ItemCoin_Factory(ItemType.Coin_A));
        FactoryDict.Add(ItemType.RedFlower, new Item_RedFlower_Factory());
        FactoryDict.Add(ItemType.Book, new Item_Book_Factory());
        FactoryDict.Add(ItemType.Torso_Armor_E, new Item_TorsoArmor_E_Factory());
        FactoryDict.Add(ItemType.Weapon_E, new Item_Weapon_E_Factory());
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
