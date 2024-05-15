using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks; // Cysharp.Threading.Tasks���g�p���Ĕ񓯊��������T�|�[�g
public class HoleObject : MonoBehaviour
{
    [SerializeField]
    private Transform _moleObject; // ���O����Transform�I�u�W�F�N�g
    [SerializeField]
    private Vector3 _showingPositionOffset, _hiddenPositionOffset; // ���O���̕\���ʒu�Ɣ�\���ʒu�̃I�t�Z�b�g
    [SerializeField]
    private float _magnification; // �g�嗦
    [SerializeField]
    private float _hiddenTime; // ���O������\���ɂȂ鎞��
    [SerializeField]
    private float _minTime, _maxTime; // ���O�����\�������ŏ����Ԃƍő厞��
    private float _randomTime; // �����_���Ɍ��肳��鎞��
    private float _currentTime; // ���݂̎���
    private bool _isCanHit = false; // ���O�����@���邩�ǂ���
    private bool _isTransition = false; // ��ԑJ�ڒ����ǂ���
    private bool _isStop = false; // �Q�[������~���Ă��邩�ǂ���
    private enum State
    {
        Hidden, // ���O������\�����
        Showing // ���O�����\�����
    }
    private State _currentState; // ���݂̏��

    void Start()
    {
        ChangeState(State.Hidden); // ������Ԃ��\���ɐݒ�
    }

    async void Update()
    {
        print(_currentState); // ���݂̏�Ԃ��R���\�[���ɕ\��
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
        _isStop = true; // �Q�[�����~����
    }

    public void Hit()
    {
        if (!_isCanHit) return; // �@���Ȃ��ꍇ�͉������Ȃ�
        if (_currentState == State.Hidden) return; // ���O������\����Ԃ̏ꍇ�͉������Ȃ�
        _isCanHit = false;
        ChangeState(State.Hidden); // ���O���̏�Ԃ��\���ɕύX
    }

    private void ChangeState(State state)
    {
        _currentState = state; // �V������ԂɕύX
        _isTransition = false; // �J�ڏ�Ԃ����Z�b�g
        _currentTime = 0; // ���݂̎��Ԃ����Z�b�g
        _randomTime = Random.Range(_minTime, _maxTime); // �����_���Ȏ��Ԃ�ݒ�
    }
}

public static class Tween
{
    public async static Task LerpTween(this Transform targetTransform, Vector3 targetPosition, float duration) // "during"��"duration"�ɏC��
    {
        float step = 0f;
        Vector3 originPosition = targetTransform.position;
        while (step < 1f) // ���S�Ƀ^�[�Q�b�g�|�W�V�����ɓ��B����܂Ń��[�v
        {
            targetTransform.position = Vector3.Lerp(originPosition, targetPosition, step);
            step += Time.deltaTime / duration;
            await Task.Yield();
        }
        targetTransform.position = targetPosition; // �Ō�ɐ��m�ȃ^�[�Q�b�g�|�W�V�����ɐݒ�
    }
}
