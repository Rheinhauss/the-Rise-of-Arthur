using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_FootArmor_A_Factory : ItemEquipFactory
{
    public Item_FootArmor_A_Factory()
    {
        itemType = ItemType.Foot_Armor_A;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0010_Foot_Armor_A", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
