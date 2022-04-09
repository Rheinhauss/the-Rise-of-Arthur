using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HeadArmor_B_Factory : ItemEquipFactory
{
    public Item_HeadArmor_B_Factory()
    {
        itemType = ItemType.Head_Armor_B;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0005_Head_Armor_B", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
