using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_LegArmor_A_Factory : ItemEquipFactory
{
    public Item_LegArmor_A_Factory()
    {
        itemType = ItemType.Leg_Armor_A;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0008_Leg_Armor_A", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
