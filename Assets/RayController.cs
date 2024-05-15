using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // カメラのスクリーン座標からレイを作成
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // レイをデバッグ用に可視化
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.5f);

            // レイキャストを行い、オブジェクトに当たったかどうかをチェック
            if (Physics.Raycast(ray, out hit))
            {
                // ヒットしたオブジェクトの親にHoleObjectコンポーネントがあるかどうかをチェック
                if (hit.collider.transform.parent.TryGetComponent<HoleObject>(out var holeObject))
                {
                    // HoleObjectが見つかった場合はHitメソッドを呼び出す
                    holeObject.Hit();
                }
            }
        }
    }
}
