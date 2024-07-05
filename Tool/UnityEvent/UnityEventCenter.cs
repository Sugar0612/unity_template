/*
author: ShiyiTang
time: 2024/07/05-17:02:08
describe: 事件分发器，维护了一个 Dictionary<EnumDefine.EventKey, OnNotification>
key: Enum
value：delegate
*/

using System.Collections.Generic;
using UnityEngine;

public class UnityEventCenter
{
    private static UnityEventCenter m_instance;

    public static UnityEventCenter Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new UnityEventCenter();
            }
            return m_instance;
        }
    }

    public delegate void OnNotification(IMessage msg);

    public static Dictionary<EnumDefine.EventKey, OnNotification> eventListeners = new Dictionary<EnumDefine.EventKey, OnNotification>();

    public static void AddListener(EnumDefine.EventKey eventKey, OnNotification notify)
    {
        if (notify == null)
        {
            Debug.LogError("������ȫ!!!");
            return;
        }

        if (!eventListeners.ContainsKey(eventKey))
        {
            eventListeners.Add(eventKey, notify);
        }
        else
        {
            eventListeners[eventKey] += notify;
        }
    }

    /// <summary>
    /// �÷������� �¼��ķַ�����
    /// </summary>
    /// <param name="eventKey"></param>
    /// <param name="msg"></param>
    public static void DistributeEvent(EnumDefine.EventKey eventKey, IMessage msg)
    {
        if (!eventListeners.ContainsKey(eventKey))
        {
            Debug.Log("keyֵ����������룡����= " + eventKey);
            return;
        }

        eventListeners[eventKey](msg);
    }

    /// <summary>
    /// �Ƴ�ĳһ�¼���ȫ������
    /// </summary>
    /// <param name="eventKey"></param>
    public static void RemoveEventLister(EnumDefine.EventKey eventKey)
    {
        if (!eventListeners.ContainsKey(eventKey))
        {
            return;
        }
        eventListeners.Remove(eventKey);
    }

    /// <summary>
    /// �Ƴ�ĳһ�¼���ĳһ������
    /// </summary>
    /// <param name="eventKey"></param>
    /// <param name="notify"></param>
    public static void RemoveLister(EnumDefine.EventKey eventKey, OnNotification notify)
    {
        if (!eventListeners.ContainsKey(eventKey))
        {
            return;
        }
        eventListeners[eventKey] -= notify;

        if (eventListeners[eventKey] == null)
        {
            eventListeners.Remove(eventKey);
        }
    }
}
