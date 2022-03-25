using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class UnityJSon : MonoBehaviour
{
    /// <summary>
    /// ��������Ϊjson��ʽ
    /// </summary>
    /// <param name="obj">��Ҫ��������ݶ���</param>
    /// <param name="fileName">�ļ���</param>
    /// <param name="path">����·����</param>
    /// <returns>�ɹ�����true��ʧ�ܷ���false</returns>
    public static bool Saves(object obj, string fileName, string path)
    {
        try
        {
            //�ж�·���Ƿ���ڲ����ھʹ���һ��
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //���ļ�����·���ϲ�
            fileName = Path.Combine(path, fileName);
            //�ж��ļ��Ƿ��Ѿ����ڲ����ھʹ���һ���ļ�
            if (!File.Exists(fileName))
            {
                FileStream fs = File.Create(fileName);
                fs.Close();
            }
            string json = JsonUtility.ToJson(obj);
            //utf8 ������������
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
    /// <param name="type">����</param>
    /// <param name="fileName">�ļ���,�����ļ�·��</param>
    /// <returns>�洢���ݵ�object</returns>
    public static object Read(Type type, string fileName)
    {
        string json = File.ReadAllText(fileName, Encoding.UTF8);
        return JsonUtility.FromJson(json, type);
    }
}