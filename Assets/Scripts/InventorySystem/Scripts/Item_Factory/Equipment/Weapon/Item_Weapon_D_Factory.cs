using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Weapon_D_Factory : ItemEquipFactory
{
    public Item_Weapon_D_Factory()
    {
        itemType = ItemType.Weapon_D;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0015_Weapon_D", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
