using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class UnityJSon : MonoBehaviour
{
    /// <summary>
    /// 保存数据为json格式
    /// </summary>
    /// <param name="obj">需要保存的数据对象</param>
    /// <param name="fileName">文件名</param>
    /// <param name="path">创建路径名</param>
    /// <returns>成功返回true，失败返回false</returns>
    public static bool Saves(object obj, string fileName, string path)
    {
        try
        {
            //判断路径是否存在不存在就创建一个
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //将文件名和路径合并
            fileName = Path.Combine(path, fileName);
            //判断文件是否已经存在不存在就创建一个文件
            if (!File.Exists(fileName))
            {
                FileStream fs = File.Create(fileName);
                fs.Close();
            }
            string json = JsonUtility.ToJson(obj);
            //utf8 万国码避免乱码
            File.WriteAllText(fileName, json, Encoding.UTF8);
            return true;
        }
        catch
        {
            return false;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="fileName">文件名,包括文件路径</param>
    /// <returns>存储数据的object</returns>
    public static object Read(Type type, string fileName)
    {
        string json = File.ReadAllText(fileName, Encoding.UTF8);
        return JsonUtility.FromJson(json, type);
    }
}