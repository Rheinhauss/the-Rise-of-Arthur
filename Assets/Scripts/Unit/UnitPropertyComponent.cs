using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPropertyComponent
{
    #region 更改属性

    /// <summary>
    /// 更改后的新增属性，原代码能修改
    /// </summary>
    //最大HP值 能拥有的最大HP值
    public float MaxHP = 100;
    //最大MP值 能拥有的最大MP值
    public float MaxMP = 100;
    //最大护盾值 能拥有的最大护盾值（最大HP的百分比）
    public float MaxShield = 0.5f;
    //护盾值消散比例  每秒护盾消散比例，间接决定最大护盾
    public float ShieldDissipateRate = 0.25f;
    //移动速度  角色每秒能移动的最大距离
    public float MoveSpeed = 5;
    //攻击力   角色的基本攻击力
    public float AttackPower = 20;
    //攻击速度  角色各类攻击动画的播放速度
    public float AttackSpeed = 1;
    //伤害修正(全能加成)    伤害公式中全能加成项
    public float Versality = 0;
    //元素伤害加成
    public float ElementalDmgModifier = 0;
    // 金元素伤害加成
    public float JinDmgModifier = 0;
    // 木元素伤害加成
    public float MuDmgModifier = 0;
    // 水元素伤害加成
    public float ShuiDmgModifier = 0;
    // 火元素伤害加成
    public float HuoDmgModifier = 0;
    // 土元素伤害加成
    public float TuDmgModifier = 0;
    // 暴击概率
    public float CriticalChance = 0;
    //暴击伤害  发起者为该单位的伤害的暴击伤害
    public float CriticalDamage = 1.5f;
    //HP值   当前的生命值，为0时角色死亡
    public float CurrentHP = 100;
    //MP值   当前的MP值
    public float CurrentMP = 0;
    //护盾值   当前角色拥有的护盾值，当护盾大于0时，受到攻击会优先消耗护盾值（可能会有特判）
    public float CurrentShield = 0;
    /// <summary>
    /// 护盾减伤修正（需与程序讨论）			
    /// </summary>
    //减伤百分比 受伤百分比修正
    public float TakenDmgModifier = 0;
    //元素抗性修正百分比 5+1(无属性)属性抗性分别计算
    public float TakenElementalDmgModifier = 0;
    //击退抗性  受到击退效果时，被击退的距离计算时的减少百分比'
    public float KnockBackResistance = 0;
    //受到硬直效果时，硬直的持续时间计算时的减少百分比
    public float RigidResistance = 0;
    //是否是友方单位
    public bool isOwn = false;
    //是否是飞行单位   无视碰撞体
    public bool isFly = false;

    #endregion
    #region 属性数据的文件读写
    /// <summary>
    /// 写入当前对象的数据
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="path">文件路径</param>
    /// <returns>是否写入成功</returns>
    public bool Save(string fileName, string path)
    {
        return UnityJSon.Saves(this, fileName, path);
    }
    /// <summary>
    /// 读入文件数据到当前对象
    /// </summary>
    /// <param name="fileName">文件名，包括路径</param>
    public void ReadToSelf(string fileName)
    {
        UnitPropertyComponent unit = UnityJSon.Read(typeof(UnitPropertyComponent), fileName) as UnitPropertyComponent;
        this.MaxHP = unit.MaxHP;
        this.MaxMP = unit.MaxMP;
        this.MaxShield = unit.MaxShield;
        this.ShieldDissipateRate = unit.ShieldDissipateRate;
        this.MoveSpeed = unit.MoveSpeed;
        this.AttackPower = unit.AttackPower;
        this.AttackSpeed = unit.AttackSpeed;
        this.Versality = unit.Versality;
        this.ElementalDmgModifier = unit.ElementalDmgModifier;
        this.JinDmgModifier = unit.JinDmgModifier;
        this.MuDmgModifier = unit.MuDmgModifier;
        this.HuoDmgModifier = unit.HuoDmgModifier;
        this.ShuiDmgModifier = unit.ShuiDmgModifier;
        this.TuDmgModifier = unit.TuDmgModifier;
        this.CriticalChance = unit.CriticalChance;
        this.CriticalDamage = unit.CriticalDamage;
        this.CurrentHP = unit.CurrentHP;
        this.CurrentMP = unit.CurrentMP;
        this.CurrentShield = unit.CurrentShield;
        this.TakenDmgModifier = unit.TakenDmgModifier;
        this.TakenElementalDmgModifier = unit.TakenElementalDmgModifier;
        this.KnockBackResistance = unit.KnockBackResistance;
        this.RigidResistance = unit.RigidResistance;
        this.isOwn = unit.isOwn;
        this.isFly = unit.isFly;
    }
    /// <summary>
    /// 读入文件数据并返回读入后的对象
    /// </summary>
    /// <param name="fileName">文件夹，包括路径</param>
    /// <returns></returns>
    public UnitPropertyComponent Read(string fileName)
    {
        return UnityJSon.Read(typeof(UnitPropertyComponent), fileName) as UnitPropertyComponent;
    }
    #endregion
}
