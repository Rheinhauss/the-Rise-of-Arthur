using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_FootArmor_B_Factory : ItemEquipFactory
{
    public Item_FootArmor_B_Factory()
    {
        itemType = ItemType.Foot_Armor_B;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0011_Foot_Armor_B", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
