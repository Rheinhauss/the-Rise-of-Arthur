using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HeadArmor_A_Factory : ItemEquipFactory
{
    public Item_HeadArmor_A_Factory()
    {
        itemType = ItemType.Head_Armor_A;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0004_Head_Armor_A", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
