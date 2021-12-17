using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{

    public float damage = 20;
    //public Vector3 boxSize = new Vector3(5,10,1);
    public Vector2 top_right_corner;
    public Vector2 bottom_left_corner;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {

        top_right_corner = new Vector2(gameObject.transform.position.x + top_right_corner.x, gameObject.transform.position.y + top_right_corner.y);
        bottom_left_corner = new Vector2(gameObject.transform.position.x - bottom_left_corner.x, gameObject.transform.position.y - bottom_left_corner.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DealDamage()
    {

        Collider2D[] enemyToHit = Physics2D.OverlapAreaAll(top_right_corner, bottom_left_corner, enemyLayers);
        if (enemyToHit != null && enemyToHit.Length > 0)
        {
            enemyToHit[0].gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }

    }

    void Delete()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {

            //Vector2 top_right_corner_copy = new Vector2(top_right_corner.x + 1943.99f , top_right_corner.y + -973.79f);
           // Vector2 bottom_left_corner_copy = new Vector2( 1943.99f - bottom_left_corner.x,  -973.79f - bottom_left_corner.y);

/*            top_right_corner.x += 1943.99f;
            top_right_corner.y += -973.79f;
            bottom_left_corner.x -= 1943.99f;
            bottom_left_corner.y -= -973.79f;*/

            Vector2 center_offset = (top_right_corner + bottom_left_corner) * 0.5f;
            Vector2 displ = top_right_corner - bottom_left_corner;
            float x_p = Vector2.Dot(displ, Vector2.right);
            float y_p = Vector2.Dot(displ, Vector2.up);

            Vector2 top_left_corner = new Vector2(-x_p * 0.5f, y_p * 0.5f) + center_offset;
            Vector2 bottom_r_c = new Vector2(x_p * 0.5f, -y_p * 0.5f) + center_offset;


            Gizmos.DrawLine(top_right_corner, top_left_corner);
            Gizmos.DrawLine(top_left_corner, bottom_left_corner);
            Gizmos.DrawLine(bottom_left_corner, bottom_r_c);
            Gizmos.DrawLine(bottom_r_c, top_right_corner);
        

    }


}
