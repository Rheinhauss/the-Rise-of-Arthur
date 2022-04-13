using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemHP_Little_Factory : ItemFactoryInterface
{
    public ItemType itemType { get; set; }

    private StatusObject StatusObject;
    public ItemHP_Little_Factory()
    {
        itemType = ItemType.HealthPotion_Little;
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/Status_0001_RestoreHP_Little", EGamePlay.Combat.AddSkillEffetTargetType.Self, 100);
    }
    public Item CreateItem(int amount)
    {
        Item item = new Item();
        item.itemType = itemType;
        item.amount = amount;
        UnityEvent<Transform, Transform, Item> e = new UnityEvent<Transform, Transform, Item>();
        item.useEvents.Add(e);
        e.AddListener(SpellAction);
        return item;
    }

    private void SpellAction(Transform Creator, Transform Target, Item item)
    {
        Target.GetComponent<UnitControllerComponent>().combatEntity.unitSpellStatusToSelfComponent.SpellToSelf(StatusObject);
    }

}
