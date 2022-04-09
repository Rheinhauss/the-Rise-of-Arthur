using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TorsoArmor_B_Factory : ItemEquipFactory
{
    public Item_TorsoArmor_B_Factory()
    {
        itemType = ItemType.Torso_Armor_B;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0007_Torso_Armor_B", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
