using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    
    private Rigidbody2D rb2D;
    [SerializeField]private float shotSpeed;
    private PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(transform.up * shotSpeed, ForceMode2D.Impulse);
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.TryGetComponent(out IDamageble ID))
        {
            ID.AddDamage(player.Player_Attack);
        }
    }
}
