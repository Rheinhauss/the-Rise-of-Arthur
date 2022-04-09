using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TorsoArmor_A_Factory : ItemEquipFactory
{
    public Item_TorsoArmor_A_Factory()
    {
        itemType = ItemType.Torso_Armor_A;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0006_Torso_Armor_A", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
