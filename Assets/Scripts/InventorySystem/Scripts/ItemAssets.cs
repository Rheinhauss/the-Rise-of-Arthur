using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    [Serializable]
    public struct ItemSpriteDictStruct
    {
        public ItemType ItemType;
        public Sprite Sprite;
    }

    public static ItemAssets Instance { get; private set; }
    public Transform pf_ItemWorld;
    public ItemSpriteDictStruct[] itemSpriteDictStructs;
    public Dictionary<ItemType, Sprite> ItemSpriteDict = new Dictionary<ItemType, Sprite>();

    private void Awake()
    {
        Instance = this;
        foreach (ItemSpriteDictStruct item in itemSpriteDictStructs)
        {
            if (ItemSpriteDict.ContainsKey(item.ItemType))
            {
                ItemSpriteDict[item.ItemType] = item.Sprite;
            }
            else
            {
                ItemSpriteDict.Add(item.ItemType, item.Sprite);
            }
        }
    }
}
