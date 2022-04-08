using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Weapon_B_Factory : ItemWeaponFactory
{
    public Item_Weapon_B_Factory()
    {
        itemType = ItemType.Weapon_B;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0003_Weapon_B", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
