using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TorsoArmor_C_Factory : ItemEquipFactory
{
    public Item_TorsoArmor_C_Factory()
    {
        itemType = ItemType.Torso_Armor_C;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0013_Torso_Armor_C", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
