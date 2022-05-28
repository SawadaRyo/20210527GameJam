using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour //�G����Player�ւ̒e�E���e����o��e

{
    //public GameObject damageEffectPrefab; //�G�t�F�N�g�i�[�p�v���n�u
    public float speed = 2.0f;
    public Rigidbody2D rb;
    [SerializeField] int m_damage = 3;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }


    private void OnColligionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player") //player�ɓ���������A�e��j�󂵃v���C���[��HP�����
        {
            //GameObject damageEffect = Instantiate(damageEffectPrefab, transform.position, Quaternion.identity); //�j�󎞂̃G�t�F�N�g
            Destroy(gameObject, 0.1f);
            PlayerScript.Instance.AddDamage(m_damage);            
        }
    }
}

