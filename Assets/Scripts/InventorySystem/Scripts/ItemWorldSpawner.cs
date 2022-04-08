using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemWorldSpawner : MonoBehaviour
{
    [LabelText("使用ItemType创建默认Item")]
    public bool IsUseItemTypeCreate = false;
    [HideIf("IsUseItemTypeCreate")]
    public Item item;
    [ShowIf("IsUseItemTypeCreate")]
    public ItemType ItemType;
    [ShowIf("IsUseItemTypeCreate")]
    [Range(1,99)]
    public int amount = 1;

    private void Start()
    {
        if (!IsUseItemTypeCreate)
        {
            ItemWorld.SpawnItemWorld(transform.position, item);
        }
        else
        {
            ItemWorld.SpawnItemWorld(transform.position, Item_Factory.Instance.CreateItem(ItemType, amount));
        }
        Destroy(gameObject);
    }
}
