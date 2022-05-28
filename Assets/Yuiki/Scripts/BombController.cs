using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    private int count = 0;
    public float bulletRotate = 2;
    public GameObject explosionPrefab;
    public GameObject bulletPrefab;


    void Update()
    {
        count++;

        if (count > 150)
        {
            GameObject effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject; //エフェクト生成
            Destroy(effect, 1.0f);
            Destroy(gameObject);

            for(float rotate = 0f; rotate < 360f; rotate = rotate + bulletRotate)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, rotate));

            }


        }
    }
}
