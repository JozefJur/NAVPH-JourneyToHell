using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script is used for spell casted by boss entity
public class SpellController : MonoBehaviour
{

    public float Damage = 20; // 30
    //public Vector3 boxSize = new Vector3(5,10,1);
    public Vector2 Top_right_corner; // 3 10
    public Vector2 Bottom_left_corner; // 4 1
    public LayerMask EnemyLayers;



    void Start()
    {

        Top_right_corner = new Vector2(gameObject.transform.position.x + Top_right_corner.x, gameObject.transform.position.y + Top_right_corner.y);
        Bottom_left_corner = new Vector2(gameObject.transform.position.x - Bottom_left_corner.x, gameObject.transform.position.y - Bottom_left_corner.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method is called from animation and deals damage to player if present in collider area
    void DealDamage()
    {

        Collider2D[] enemyToHit = Physics2D.OverlapAreaAll(Top_right_corner, Bottom_left_corner, EnemyLayers);
        if (enemyToHit != null && enemyToHit.Length > 0)
        {
            enemyToHit[0].gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
        }

    }

    void Delete()
    {
        Destroy(gameObject);
    }

    // Utility function used to visualize collision area in editor
    void OnDrawGizmos()
    {
            Vector2 center_offset = (Top_right_corner + Bottom_left_corner) * 0.5f;
            Vector2 displ = Top_right_corner - Bottom_left_corner;
            float x_p = Vector2.Dot(displ, Vector2.right);
            float y_p = Vector2.Dot(displ, Vector2.up);

            Vector2 top_left_corner = new Vector2(-x_p * 0.5f, y_p * 0.5f) + center_offset;
            Vector2 bottom_r_c = new Vector2(x_p * 0.5f, -y_p * 0.5f) + center_offset;


            Gizmos.DrawLine(Top_right_corner, top_left_corner);
            Gizmos.DrawLine(top_left_corner, Bottom_left_corner);
            Gizmos.DrawLine(Bottom_left_corner, bottom_r_c);
            Gizmos.DrawLine(bottom_r_c, Top_right_corner);

    }


}
