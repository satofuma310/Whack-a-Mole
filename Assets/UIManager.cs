using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _timerText;
    public void SetTimerText(float value)
    {
        _timerText.text = "Time:" + value.ToString("F2");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
