using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    private HoleObject[] _holeObjects; // HoleObjectの配列

    void Start()
    {
        // シーン内のすべてのHoleObjectを見つけて配列に格納
        _holeObjects = FindObjectsByType<HoleObject>(FindObjectsSortMode.None);
    }

    // すべてのHoleObjectを停止するメソッド
    public void StopAllHole()
    {
        // すべてのHoleObjectに対してStopHoleメソッドを呼び出す
        foreach (var hole in _holeObjects)
        {
            hole.StopHole();
        }
    }
}
