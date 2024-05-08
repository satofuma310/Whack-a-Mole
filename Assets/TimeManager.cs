using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimeManager : MonoBehaviour
{
    private static TimeManager _ins;
    public static TimeManager instance
    {
        get
        {
            if (_ins == null)
                _ins = FindAnyObjectByType<TimeManager>();
            return _ins;
        }
    }
    private const float TIME_LIMIT = 30f;
    private float _timer;
    private Action<float> _onTimerChange;
    public event Action<float> OnTimerChange
    {
        add
        {
            _onTimerChange += value;
        }
        remove
        {
            _onTimerChange -= value;
        }
    } 
    void Start()
    {

    }

    void Update()
    {
        if (_timer >= TIME_LIMIT) return;
        _timer += Time.deltaTime;
        _onTimerChange(TIME_LIMIT - _timer);
    }
    public float GetMagnification()
    {
        return _timer / TIME_LIMIT;
    }
}
