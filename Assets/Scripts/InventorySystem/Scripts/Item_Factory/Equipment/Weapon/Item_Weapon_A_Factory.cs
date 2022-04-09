using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Weapon_A_Factory : ItemEquipFactory
{
    public Item_Weapon_A_Factory()
    {
        itemType = ItemType.Weapon_A;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0002_Weapon_A", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
