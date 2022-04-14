using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TorsoArmor_E_Factory : ItemEquipFactory
{
    public Item_TorsoArmor_E_Factory()
    {
        itemType = ItemType.Torso_Armor_E;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0018_Torso_Armor_E", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
}
