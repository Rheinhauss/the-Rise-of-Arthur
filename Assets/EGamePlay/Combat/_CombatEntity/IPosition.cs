﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ET;

namespace EGamePlay
{
    /// <summary>
    /// 位置接口
    /// </summary>
    public interface IPosition
    {
        Vector3 Position { get; set; }
    }
}