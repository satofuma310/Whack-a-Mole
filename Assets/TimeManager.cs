using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance; // �V���O���g���C���X�^���X

    // �C���X�^���X�ւ̃A�N�Z�X�v���p�e�B
    public static TimeManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<TimeManager>(); // �V�[������TimeManager��T��
            return _instance;
        }
    }

    private const float TIME_LIMIT = 30f; // �������ԁi�b�j
    private float _timer; // �o�ߎ���
    private Action<float> _onTimerChange; // �^�C�}�[���ω��������̃C�x���g

    // �^�C�}�[���ω��������̃C�x���g�v���p�e�B
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

    // �t���[�����Ƃ̍X�V����
    void Update()
    {
        if (_timer >= TIME_LIMIT) return; // �������ԂɒB���Ă���ꍇ�͏������s��Ȃ�
        _timer += Time.deltaTime; // �o�ߎ��Ԃ��X�V
        _onTimerChange?.Invoke(TIME_LIMIT - _timer); // �C�x���g���Ăяo���A�c�莞�Ԃ�n��
    }

    // ���Ԃ̌o�߂Ɋ�Â��{�����擾���郁�\�b�h
    public float GetMagnification()
    {
        return _timer / TIME_LIMIT; // �o�ߎ��Ԃ̊�����Ԃ�
    }
}
