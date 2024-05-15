using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �J�����̃X�N���[�����W���烌�C���쐬
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ���C���f�o�b�O�p�ɉ���
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.5f);

            // ���C�L���X�g���s���A�I�u�W�F�N�g�ɓ����������ǂ������`�F�b�N
            if (Physics.Raycast(ray, out hit))
            {
                // �q�b�g�����I�u�W�F�N�g�̐e��HoleObject�R���|�[�l���g�����邩�ǂ������`�F�b�N
                if (hit.collider.transform.parent.TryGetComponent<HoleObject>(out var holeObject))
                {
                    // HoleObject�����������ꍇ��Hit���\�b�h���Ăяo��
                    holeObject.Hit();
                }
            }
        }
    }
}
