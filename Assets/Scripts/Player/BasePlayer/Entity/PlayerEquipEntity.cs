using EGamePlay;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEquipEntity : Entity
{
    /// <summary>
    /// HP道具
    /// </summary>
    public Item hp_Item { get; private set; }
    /// <summary>
    /// 头部
    /// </summary>
    public Item Head_Armor { get; private set; }
    /// <summary>
    /// 躯干
    /// </summary>
    public Item Torso_Armor { get; private set; }
    /// <summary>
    /// 腿
    /// </summary>
    public Item Leg_Armor { get; private set; }
    /// <summary>
    /// 脚
    /// </summary>
    public Item Foot_Armor { get; private set; }
    /// <summary>
    /// 武器
    /// </summary>
    public Item Weapon { get; private set; }

    private CombatEntity combatEntity => GetParent<CombatEntity>();
    private Transform Owner => combatEntity.ModelObject.transform;
    private PlayerInventoryEntity PlayerInventoryEntity => Player.Instance.PlayerInventoryEntity;
    private PlayerUIController PlayerUI => Player.Instance.PlayerUIController;

    public void Init()
    {
        hp_Item = Head_Armor = Torso_Armor = Leg_Armor = Foot_Armor = Weapon = null;
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.Alpha1, () =>
        {
            PlayerInventoryEntity.inventory.UseItem(hp_Item, Owner, Owner);
            //if (PlayerInventoryEntity.inventory.UseItem(hp_Item, Owner, Owner))
            //{
            //    Debug.Log("使用HP成功");
            //}
            //else
            //{
            //    Debug.Log("使用HP失败");
            //}
        }, KeyCodeType.DOWN);
    }

    public void UpdateEquip()
    {
        hp_Item = null;
        foreach (Item item in PlayerInventoryEntity.inventory.GetItemList())
        {
            //只有一种HP药剂
            if (item.GetItemType() == ItemType.HealthPotion)
            {
                hp_Item = item;
            }
        }
    }

    public bool SetHeadArmor(Item item)
    {
        if (item.GetItemType() != ItemType.Head_Armor)
            return false;
        return true;
    }
    public bool SetTorsoArmor(Item item)
    {
        if (item.GetItemType() != ItemType.Torso_Armor)
            return false;
        return true;
    }
    public bool SetLegArmor(Item item)
    {
        if (item.GetItemType() != ItemType.Leg_Armor)
            return false;
        return true;
    }
    public bool SetFootArmor(Item item)
    {
        if (item.GetItemType() != ItemType.Foot_Armor)
            return false;
        return true;
    }
    public bool SetWeapon(Item item)
    {
        if (item.GetItemType() != ItemType.Weapon)
            return false;
        Weapon = item;
        PlayerUI.UI_Equip.Weapon.sprite = item.GetSprite();
        return true;
    }

    public bool SetEquip(Item item)
    {
        switch (item.GetItemType())
        {
            case ItemType.Head_Armor:
                return SetHeadArmor(item);
            case ItemType.Torso_Armor:
                return SetTorsoArmor(item);
            case ItemType.Leg_Armor:
                return SetLegArmor(item);
            case ItemType.Foot_Armor:
                return SetFootArmor(item);
            case ItemType.Weapon:
                return SetWeapon(item);
            default:
                return false;
        }
    }
}
