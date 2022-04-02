using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatusComponent : Entity
{
    #region 单位的各种状态常量
    /// <summary>
    /// 单位的各种状态常量（单属性，5个，双属性，5个）
    /// </summary>
    //眩晕（金）
    public bool isVertigo;
    //缠绕（木）
    public bool isWinding;
    //缓慢（水）
    public bool isSlow;
    //点燃（火）
    public bool isIgnite;
    //中毒（土）
    public bool isPoisoning;
    //焚烧（木生火）
    public bool isIncineration;
    //束缚（水生木）
    public bool isBondage;
    //疫病（火生土）
    public bool isEpidemicDisease;
    //禁制（土生金）
    public bool isProhibition;
    //冰冻（金生水）
    public bool isFreeze;

    #endregion
}
