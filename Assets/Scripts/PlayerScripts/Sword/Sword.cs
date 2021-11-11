using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerAttack playerA;
    // Start is called before the first frame update
    void Start()
    {
        playerA = transform.parent.gameObject.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag.Equals("Enemy"))
        {
            MonsterHealth MonsterH = collision.gameObject.GetComponent<MonsterHealth>();
            MonsterH.TakeDamage(playerA.currentDmg);
        }
    }
}
