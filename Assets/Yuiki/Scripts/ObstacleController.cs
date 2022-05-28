using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] int m_damage = 5;
    //public GameObject damageEffectPrefab; //�G�t�F�N�g�i�[�p


    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerScript.Instance.AddDamage(m_damage); //Player�X�N���v�g��AddDamage���\�b�h���Ăяo��

        if (other.gameObject.tag == "player") //player�ɓ���������A��Q����j�󂷂�
        {
            //GameObject damageEffect = Instantiate(damageEffectPrefab, transform.position, Quaternion.identity); //�j�󎞂̃G�t�F�N�g
            Destroy(gameObject, 0.1f);
        }
    }
}

