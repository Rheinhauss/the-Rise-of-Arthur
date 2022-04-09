using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类别
/// </summary>
public enum ItemType
{
    //头部
    [HideInInspector]
    Head_Armor,
    [LabelText("头盔A")]
    Head_Armor_A,
    [LabelText("头盔B")]
    Head_Armor_B,
    [HideInInspector]
    Head_Armor_End,

    //躯干
    [HideInInspector]
    Torso_Armor,
    [LabelText("躯干盔甲A")]
    Torso_Armor_A,
    [LabelText("躯干盔甲B")]
    Torso_Armor_B,
    [HideInInspector]
    Torso_Armor_End,

    //腿
    [HideInInspector]
    Leg_Armor,
    [LabelText("腿部盔甲A")]
    Leg_Armor_A,
    [LabelText("腿部盔甲B")]
    Leg_Armor_B,
    [HideInInspector]
    Leg_Armor_End,

    //脚
    [HideInInspector]
    Foot_Armor,
    [LabelText("鞋子A")]
    Foot_Armor_A,
    [LabelText("鞋子B")]
    Foot_Armor_B,
    [HideInInspector]
    Foot_Armor_End,

    //武器
    [HideInInspector]
    Weapon,
    [LabelText("武器A")]
    Weapon_A,
    [LabelText("武器B")]
    Weapon_B,
    [HideInInspector]
    Weapon_End,

    //HP
    [HideInInspector]
    HealthPotion,
    [LabelText("小型HP药剂")]
    HealthPotion_Little,
    [HideInInspector]
    HealthPotion_End,

    //MP
    [HideInInspector]
    ManaPotion,
    [LabelText("小型MP药剂")]
    ManaPotion_Little,
    [HideInInspector]
    ManaPotion_End,

    //Coin
    [HideInInspector]
    Coin,
    [LabelText("货币A")]
    Coin_A,
    [HideInInspector]
    Coin_End,

    None,
}

