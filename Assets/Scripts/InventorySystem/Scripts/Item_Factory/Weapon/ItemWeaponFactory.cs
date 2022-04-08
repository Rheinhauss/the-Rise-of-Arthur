using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemWeaponFactory : ItemFactoryInterface
{
    protected StatusObject StatusObject;
    public ItemType itemType { get; set; }

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

    private void SpellAction(Transform Creator, Transform Target, Item self)
    {
        CombatEntity combatEntity = Target.GetComponent<UnitControllerComponent>().combatEntity;
        //û��ʹ��װ����װ��
        if (!self.IsUsing)
        {
            PlayerEquipEntity playerEquip = combatEntity.GetChild<PlayerEquipEntity>();
            //��Ϊ�������֮ǰ��װ���Լ�Ч����ִ��һ�¼���
            if (playerEquip.Weapon != null)
            {
                playerEquip.Weapon.Execute(Creator, Target);
            }
            combatEntity.unitSpellStatusToSelfComponent.SpellToSelf(StatusObject);
            combatEntity.GetChild<PlayerEquipEntity>().SetEquip(self);
            self.IsUsing = true;
        }
        //����ʹ��װ����ж��
        else
        {
            AddStatusEffect addStatus = StatusObject.effect as AddStatusEffect;
            combatEntity.GetStatus(addStatus.AddStatus.ID).EndAbility();
            combatEntity.GetChild<PlayerInventoryEntity>().inventory.AddItem(self);
            self.IsUsing = false;
        }
    }
}
