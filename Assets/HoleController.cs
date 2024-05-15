using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    private HoleObject[] _holeObjects; // HoleObject�̔z��

    void Start()
    {
        // �V�[�����̂��ׂĂ�HoleObject�������Ĕz��Ɋi�[
        _holeObjects = FindObjectsByType<HoleObject>(FindObjectsSortMode.None);
    }

    // ���ׂĂ�HoleObject���~���郁�\�b�h
    public void StopAllHole()
    {
        // ���ׂĂ�HoleObject�ɑ΂���StopHole���\�b�h���Ăяo��
        foreach (var hole in _holeObjects)
        {
            hole.StopHole();
        }
    }
}
