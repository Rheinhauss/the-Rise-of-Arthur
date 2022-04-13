using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Weapon_C_Factory : ItemEquipFactory
{
    public Item_Weapon_C_Factory()
    {
        itemType = ItemType.Weapon_C;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0012_Weapon_C", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
