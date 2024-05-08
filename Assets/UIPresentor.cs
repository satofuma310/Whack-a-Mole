using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPresentor : MonoBehaviour
{
    [SerializeField]
    private UIManager _uIManager;
    void Awake()
    {
        TimeManager.instance.OnTimerChange += i => _uIManager.SetTimerText(i);
    }

    void Update()
    {
        
    }
}
