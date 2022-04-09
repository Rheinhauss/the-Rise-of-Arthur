using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʒ���
/// </summary>
public enum ItemType
{
    //ͷ��
    [HideInInspector]
    Head_Armor,
    [LabelText("ͷ��A")]
    Head_Armor_A,
    [LabelText("ͷ��B")]
    Head_Armor_B,
    [HideInInspector]
    Head_Armor_End,

    //����
    [HideInInspector]
    Torso_Armor,
    [LabelText("���ɿ���A")]
    Torso_Armor_A,
    [LabelText("���ɿ���B")]
    Torso_Armor_B,
    [HideInInspector]
    Torso_Armor_End,

    //��
    [HideInInspector]
    Leg_Armor,
    [LabelText("�Ȳ�����A")]
    Leg_Armor_A,
    [LabelText("�Ȳ�����B")]
    Leg_Armor_B,
    [HideInInspector]
    Leg_Armor_End,

    //��
    [HideInInspector]
    Foot_Armor,
    [LabelText("Ь��A")]
    Foot_Armor_A,
    [LabelText("Ь��B")]
    Foot_Armor_B,
    [HideInInspector]
    Foot_Armor_End,

    //����
    [HideInInspector]
    Weapon,
    [LabelText("����A")]
    Weapon_A,
    [LabelText("����B")]
    Weapon_B,
    [HideInInspector]
    Weapon_End,

    //HP
    [HideInInspector]
    HealthPotion,
    [LabelText("С��HPҩ��")]
    HealthPotion_Little,
    [HideInInspector]
    HealthPotion_End,

    //MP
    [HideInInspector]
    ManaPotion,
    [LabelText("С��MPҩ��")]
    ManaPotion_Little,
    [HideInInspector]
    ManaPotion_End,

    //Coin
    [HideInInspector]
    Coin,
    [LabelText("����A")]
    Coin_A,
    [HideInInspector]
    Coin_End,

    None,
}

