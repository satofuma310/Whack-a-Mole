using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
public class HoleObject : MonoBehaviour
{
    [SerializeField]
    private Transform _moleObject;
    [SerializeField]
    private Vector3
        _showingPositionOffset,
        _hiddenPositionOffset;
    [SerializeField]
    private float _magnification;
    [SerializeField]
    private float _hiddenTime;
    [SerializeField]
    private float 
        _minTime,
        _maxTime;
    private float _randomTime;
    private float _currentTime;
    private bool _isCanHit = false;
    private bool _isTrandition = false;
    enum State
    {
        Hidden,
        Showing,
    }
    private State _currentState;
    void Start()
    {
        ChangeState(State.Hidden);
    }

    async void Update()
    {
        print(_currentState);
        _currentTime += Time.deltaTime + Time.deltaTime * ( _magnification * TimeManager.instance.GetMagnification());
        switch (_currentState)
        {
            case State.Hidden:
                if (!_isTrandition)
                {
                    _isCanHit = false;
                    await _moleObject.LerpTween(transform.position + _hiddenPositionOffset, _hiddenTime);
                    _isTrandition = true;
                }
                if (_currentTime > _randomTime)
                    ChangeState(State.Showing);
                break;
            case State.Showing:
                if (!_isTrandition)
                {
                    await _moleObject.LerpTween(transform.position + _showingPositionOffset, _hiddenTime);
                    _isTrandition = true;
                    _isCanHit = true;
                }
                if (_currentTime > _randomTime)
                    ChangeState(State.Hidden);
                break;
            default:
                break;
        }
    }
    public void Hit()
    {
        if (!_isCanHit) return;
        if (_currentState == State.Hidden) return;
        _isCanHit = false;
        ChangeState(State.Hidden);
    }
    private void ChangeState(State state)
    {
        _currentState = state;
        _isTrandition = false;
        _currentTime = 0;
        _randomTime = Random.Range(_minTime, _maxTime);
    }

}
public static class Tween
{
    public async static Task LerpTween(this Transform targetTransform, Vector3 targetPosition, float during)
    {
        float step = 0f;
        Vector3 originPosition = targetTransform.position;
        while (step < 0.9f)
        {
            targetTransform.position = Vector3.Lerp(originPosition, targetPosition, step);
            step += Time.deltaTime / during;
            await Task.Yield();
        }
        targetTransform.position = targetPosition;
    }
}