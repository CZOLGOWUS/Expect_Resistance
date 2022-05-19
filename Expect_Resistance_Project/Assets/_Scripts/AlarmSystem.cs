using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    public delegate void OnAlarm();

    public event OnAlarm onAlarm;

    public void Subscribe(OnAlarm deleg)
    {
        onAlarm += deleg;
    }
    public void AlarmAll()
    {
        onAlarm.Invoke();
    }

}
