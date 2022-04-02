using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitTools
{
    /// <summary>
    /// 获取鼠标点击屏幕的坐标
    /// </summary>
    /// <returns></returns>
    public static Vector3 InputMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //鼠标在屏幕的位置
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Plane")))
        {
            return hit.point;
        }
        return new Vector3(0, 0, 0);

    }
}
