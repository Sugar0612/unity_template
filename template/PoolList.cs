/*
author: ShiyiTang
time: 2024/07/05-16:18:40
describe: 内存池模板
eg: 
    New: public PoolList<T> m_Pool = new PoolList<T>();
    Init: m_Pool.AddListener(Instance);
    Create: T t = m_Pool.Create("Static_T");
    Destroy: m_Pool.Destroy(t);
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolList<T> where T : Component
{
    // ʵ��������
    public Func<T> OnInstance = () => { return default(T); };

    // �����б�
    public Queue<T> m_List = new Queue<T>();

    //��������
    public int m_MaxSize = 1000;

    public void AddListener(Func<T> callback)
    {
        OnInstance = callback;
    }

    /// <summary>
    /// ����ʹ��/�½� obj
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual T Create(string name)
    {
        if (m_List.Count > 0)
        {
            T t = m_List.Dequeue();
            t.gameObject.SetActive(true);
            t.transform.name = name;
            return t;
        }
        else
        {
            T t = OnInstance();
            t.gameObject.SetActive(true);
            t.transform.name = name;
            return t;
        }
    }

    /// <summary>
    /// ����/���� obj
    /// </summary>
    /// <param name="t"></param>
    public void Destroy(T t)
    {      
        if (m_List.Count < m_MaxSize && t != null)
        {
            t.gameObject.SetActive(false);
            t.gameObject.name = "unactive";
            m_List.Enqueue(t);
        }
        else
        {
            GameObject.Destroy(t);
        }
    }
}
