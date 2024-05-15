using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks; // Cysharp.Threading.Tasksを使用して非同期処理をサポート
public class HoleObject : MonoBehaviour
{
    [SerializeField]
    private Transform _moleObject; // モグラのTransformオブジェクト
    [SerializeField]
    private Vector3 _showingPositionOffset, _hiddenPositionOffset; // モグラの表示位置と非表示位置のオフセット
    [SerializeField]
    private float _magnification; // 拡大率
    [SerializeField]
    private float _hiddenTime; // モグラが非表示になる時間
    [SerializeField]
    private float _minTime, _maxTime; // モグラが表示される最小時間と最大時間
    private float _randomTime; // ランダムに決定される時間
    private float _currentTime; // 現在の時間
    private bool _isCanHit = false; // モグラが叩けるかどうか
    private bool _isTransition = false; // 状態遷移中かどうか
    private bool _isStop = false; // ゲームが停止しているかどうか
    private enum State
    {
        Hidden, // モグラが非表示状態
        Showing // モグラが表示状態
    }
    private State _currentState; // 現在の状態

    void Start()
    {
        ChangeState(State.Hidden); // 初期状態を非表示に設定
    }

    async void Update()
    {
        print(_currentState); // 現在の状態をコンソールに表示
        _currentTime += Time.deltaTime + Time.deltaTime * (_magnification * TimeManager.instance.GetMagnification());
        switch (_currentState)
        {
            case State.Hidden:
                if (!_isTransition)
                {
                    _isCanHit = false;
                    await _moleObject.LerpTween(transform.position + _hiddenPositionOffset, _hiddenTime);
                    _isTransition = true;
                }
                if (_isStop) return;
                if (_currentTime > _randomTime)
                    ChangeState(State.Showing);
                break;
            case State.Showing:
                if (!_isTransition)
                {
                    await _moleObject.LerpTween(transform.position + _showingPositionOffset, _hiddenTime);
                    _isTransition = true;
                    _isCanHit = true;
                }
                if (_isStop)
                {
                    ChangeState(State.Hidden);
                }
                if (_currentTime > _randomTime)
                    ChangeState(State.Hidden);
                break;
            default:
                break;
        }
    }

    public void StopHole()
    {
        _isStop = true; // ゲームを停止する
    }

    public void Hit()
    {
        if (!_isCanHit) return; // 叩けない場合は何もしない
        if (_currentState == State.Hidden) return; // モグラが非表示状態の場合は何もしない
        _isCanHit = false;
        ChangeState(State.Hidden); // モグラの状態を非表示に変更
    }

    private void ChangeState(State state)
    {
        _currentState = state; // 新しい状態に変更
        _isTransition = false; // 遷移状態をリセット
        _currentTime = 0; // 現在の時間をリセット
        _randomTime = Random.Range(_minTime, _maxTime); // ランダムな時間を設定
    }
}

public static class Tween
{
    public async static Task LerpTween(this Transform targetTransform, Vector3 targetPosition, float duration) // "during"を"duration"に修正
    {
        float step = 0f;
        Vector3 originPosition = targetTransform.position;
        while (step < 1f) // 完全にターゲットポジションに到達するまでループ
        {
            targetTransform.position = Vector3.Lerp(originPosition, targetPosition, step);
            step += Time.deltaTime / duration;
            await Task.Yield();
        }
        targetTransform.position = targetPosition; // 最後に正確なターゲットポジションに設定
    }
}
