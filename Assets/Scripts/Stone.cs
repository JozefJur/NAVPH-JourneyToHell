using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stone : MonoBehaviour
{
    public Sprite LevelSprite;
    public Canvas LevelCanvas;
    public GameObject NextStartPoint;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    
    }
    void Awake() 
    {
        var SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = LevelSprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player = collision.gameObject;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            LevelCanvas.transform.gameObject.SetActive(true);
        }
    }

    public void NextLevel()
    {
        //player.transform.position = NextStartPoint.transform.position;
        //gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelControllerScript>().InstantiateLevel(1, player);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }
    public void ExitGame(){
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
