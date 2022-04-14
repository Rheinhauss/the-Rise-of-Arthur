using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pf_ItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    private Item item;
    public Text textMeshPro;

    private void Awake()
    {
        textMeshPro = transform.Find("Canvas").Find("Text").GetComponent<Text>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        textMeshPro.text = "½ð±Ò * " + item.amount;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        dropPosition += new Vector3(0, 1.5f, 0);
        Vector3 randomDir = Random.insideUnitSphere;
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir, item);
        return itemWorld;
    }
}
