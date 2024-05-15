using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance; // シングルトンインスタンス

    // インスタンスへのアクセスプロパティ
    public static TimeManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<TimeManager>(); // シーン内のTimeManagerを探す
            return _instance;
        }
    }

    private const float TIME_LIMIT = 30f; // 制限時間（秒）
    private float _timer; // 経過時間
    private Action<float> _onTimerChange; // タイマーが変化した時のイベント

    // タイマーが変化した時のイベントプロパティ
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

    // フレームごとの更新処理
    void Update()
    {
        if (_timer >= TIME_LIMIT) return; // 制限時間に達している場合は処理を行わない
        _timer += Time.deltaTime; // 経過時間を更新
        _onTimerChange?.Invoke(TIME_LIMIT - _timer); // イベントを呼び出し、残り時間を渡す
    }

    // 時間の経過に基づく倍率を取得するメソッド
    public float GetMagnification()
    {
        return _timer / TIME_LIMIT; // 経過時間の割合を返す
    }
}
