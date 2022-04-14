using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TorsoArmor_D_Factory : ItemEquipFactory
{
    public Item_TorsoArmor_D_Factory()
    {
        itemType = ItemType.Torso_Armor_D;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0016_Torso_Armor_D", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
