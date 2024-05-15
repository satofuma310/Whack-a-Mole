using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPresentor : MonoBehaviour
{
    [SerializeField]
    private UIManager _uIManager;
    [SerializeField]
    private HoleController _holeController;
    void Awake()
    {
        TimeManager.instance.OnTimerChange += i =>
        {
            if( i < 0)
            {
                _holeController.StopAllHole();
                return;
            }
            _uIManager.SetTimerText(i);
        };
    }

    void Update()
    {
        
    }
}
