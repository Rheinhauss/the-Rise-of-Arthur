using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_LegArmor_B_Factory : ItemEquipFactory
{
    public Item_LegArmor_B_Factory()
    {
        itemType = ItemType.Leg_Armor_B;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0009_Leg_Armor_B", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
