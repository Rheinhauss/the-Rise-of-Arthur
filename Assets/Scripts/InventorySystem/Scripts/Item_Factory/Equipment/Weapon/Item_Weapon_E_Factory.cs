using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Weapon_E_Factory : ItemEquipFactory
{
    public Item_Weapon_E_Factory()
    {
        itemType = ItemType.Weapon_E;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0017_Weapon_E", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
