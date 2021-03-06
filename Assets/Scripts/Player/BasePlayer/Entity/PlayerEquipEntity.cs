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
    private Transform player => Player.Instance.transform;
    private InputEntity inputEntity;

    public void Init()
    {
        inputEntity = new InputEntity(KeyCode.Alpha1, KeyCodeType.DOWN);
        inputEntity.name = "PlayerEquipEntity";
        inputEntity.BindInputAction(Execute);

        hp_Item = Head_Armor = Torso_Armor = Leg_Armor = Foot_Armor = Weapon = null;
        Item weapon = Item_Factory.Instance.CreateItem(ItemType.Weapon_D, 1);
        Item torso = Item_Factory.Instance.CreateItem(ItemType.Torso_Armor_D, 1);
        weapon = PlayerInventoryEntity.HarvestItem(weapon)[0];
        torso = PlayerInventoryEntity.HarvestItem(torso)[0];
        //使用者->自己，作用者->自己
        PlayerInventoryEntity.inventory.UseItem(weapon, player, player);
        PlayerInventoryEntity.inventory.UseItem(torso, player, player);
    }

    private void Execute()
    {
        if (!Player.Instance.CanUseItem)
            return;
        PlayerInventoryEntity.inventory.UseItem(hp_Item, Owner, Owner);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        inputEntity.UnBindInputAction(Execute);
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
        Head_Armor = item;
        PlayerUI.UI_Equip.HeadArmor.sprite = item.GetSprite();
        return true;
    }
    public bool SetTorsoArmor(Item item)
    {
        if (item.GetItemType() != ItemType.Torso_Armor)
            return false;
        Torso_Armor = item;
        PlayerUI.UI_Equip.TorsoArmor.sprite = item.GetSprite();
        return true;
    }
    public bool SetLegArmor(Item item)
    {
        if (item.GetItemType() != ItemType.Leg_Armor)
            return false;
        Leg_Armor = item;
        PlayerUI.UI_Equip.LegArmor.sprite = item.GetSprite();
        return true;
    }
    public bool SetFootArmor(Item item)
    {
        if (item.GetItemType() != ItemType.Foot_Armor)
            return false;
        Foot_Armor = item;
        PlayerUI.UI_Equip.FootArmor.sprite = item.GetSprite();
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

    public Item GetEquip(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Head_Armor:
                return Head_Armor;
            case ItemType.Torso_Armor:
                return Torso_Armor;
            case ItemType.Leg_Armor:
                return Leg_Armor;
            case ItemType.Foot_Armor:
                return Foot_Armor;
            case ItemType.Weapon:
                return Weapon;
            default:
                return null;
        }
    }
}
