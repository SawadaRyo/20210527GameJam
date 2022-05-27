using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //public GameObject damageEffectPrefab; //�e�����������Ƃ��̃G�t�F�N�g�i�[�p
    public float speed = 2.0f;
    public Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        rb.AddForce(Vector3.forward * speed * Time.deltaTime); //�e���O�ɐi��
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy"�@|| other.gameObject.tag == "player") //enemy��player���^�O�Ɏ��I�u�W�F�N�g�ɓ���������A�e��j�󂷂�
        {
            //GameObject damageEffect = Instantiate(damageEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }
    }
}

