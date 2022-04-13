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
    Head_Armor = 0,
    [LabelText("ͷ��A")]
    Head_Armor_A,
    [LabelText("ͷ��B")]
    Head_Armor_B,
    [HideInInspector]
    Head_Armor_End = 50,

    //����
    [HideInInspector]
    Torso_Armor = 51,
    [LabelText("Ƥ���")]
    Torso_Armor_A,
    [LabelText("���Ӽ�")]
    Torso_Armor_B,
    [LabelText("���")]
    Torso_Armor_C,
    [HideInInspector]
    Torso_Armor_End = 100,

    //��
    [HideInInspector]
    Leg_Armor = 101,
    [LabelText("�Ȳ�����A")]
    Leg_Armor_A,
    [LabelText("�Ȳ�����B")]
    Leg_Armor_B,
    [HideInInspector]
    Leg_Armor_End = 150,

    //��
    [HideInInspector]
    Foot_Armor = 151,
    [LabelText("Ь��A")]
    Foot_Armor_A,
    [LabelText("Ь��B")]
    Foot_Armor_B,
    [HideInInspector]
    Foot_Armor_End = 200,

    //����
    [HideInInspector]
    Weapon = 201,
    [LabelText("�⼣�߰ߵĽ�")]
    Weapon_A,
    [LabelText("����Ʒ��")]
    Weapon_B,
    [LabelText("����Ʒ��")]
    Weapon_C,
    [HideInInspector]
    Weapon_End = 250,

    //HP
    [HideInInspector]
    HealthPotion = 251,
    [LabelText("С��HPҩ��")]
    HealthPotion_Little,
    [LabelText("����HPҩ��")]
    HealthPotion_Big,
    [HideInInspector]
    HealthPotion_End = 300,

    //MP
    [HideInInspector]
    ManaPotion = 301,
    [LabelText("С��MPҩ��")]
    ManaPotion_Little,
    [HideInInspector]
    ManaPotion_End = 350,

    //Coin
    [HideInInspector]
    Coin = 351,
    [LabelText("���")]
    Coin_A,
    [HideInInspector]
    Coin_End = 400,

    //Mission
    [HideInInspector]
    Mission = 401,
    [LabelText("�컨")]
    RedFlower,
    [HideInInspector]
    Mission_End = 450,



    None,
}

