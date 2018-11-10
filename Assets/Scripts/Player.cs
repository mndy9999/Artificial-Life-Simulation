using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    public GameObject self;
    public float speed;
    public Tilemap map;

    float moveTime;
    int direction;
    Vector3 playerPos;

    bool m_started;

    

	// Use this for initialization
	void Start () {
        playerPos = self.transform.position;
        moveTime = 0;
        map = Component.FindObjectOfType<Tilemap>();
	}

    // Update is called once per frame
    void Update()
    {
        seek("bush");
        if(self.GetComponent<BunnyTraits>().gender == 0)
        {
            seek("female");
        }
        else
        {
            seek("male");
        }
        
        mate();
    }

    void seek(string tag)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(self.transform.position, transform.localScale * 4, 360);
        if (colliders.Length > 2)
        {
            int rand = Random.Range(0, colliders.Length);

            //***to fix*** the bunnies sometimes ignore the food for one round and move randomly
            //then come back to it the next round
            if (colliders[rand].tag != tag)
            {
                rand = Random.Range(0, colliders.Length);
                moveRandomly();
            }
            
                if (Time.time > moveTime)
                {
                    moveTime += (10 - speed);
                    if (playerPos.x < colliders[rand].transform.position.x)
                    {
                        playerPos.x++;
                    }
                    else if (playerPos.x > colliders[rand].transform.position.x)
                    {
                        playerPos.x--;
                    }
                    else if (playerPos.y < colliders[rand].transform.position.y)
                    {
                        playerPos.y++;
                    }
                    else if (playerPos.y > colliders[rand].transform.position.y)
                    {
                        playerPos.y--;
                    }
 
                    self.transform.SetPositionAndRotation(playerPos, Quaternion.identity);
                }
            
        }
        else
        {
            moveRandomly();
        }
    }

    void moveRandomly()
    {
        int rand = Random.Range(0, 100);
        if (rand > 70)
        {
            direction = Random.Range(0, 4);
        }

        if (Time.time > moveTime)
        {
            moveTime += (10 - speed);
            switch (direction)
            {
                case 0:
                    if (playerPos.x < (map.size.x / 2) - 1) { playerPos.x++; }
                    break;
                case 1:
                    if (playerPos.x > (-map.size.x / 2)) { playerPos.x--; }
                    break;
                case 2:
                    if (playerPos.y < (map.size.y / 2) - 1) { playerPos.y++; }
                    break;
                case 3:
                    if (playerPos.y > (-map.size.y / 2)) { playerPos.y--; }
                    break;
            }
            self.transform.SetPositionAndRotation(playerPos, Quaternion.identity);
        }
    }

    void moveByKey()
    {
        if (Input.GetKeyDown("w"))
        {
            playerPos.y += 1;

        }
        else if (Input.GetKeyDown("s"))
        {
            playerPos.y -= 1;
        }
        else if (Input.GetKeyDown("a"))
        {
            playerPos.x -= 1;
        }
        else if (Input.GetKeyDown("d"))
        {
            playerPos.x += 1;
        }
        self.transform.SetPositionAndRotation(playerPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.gameObject.tag == "bush")
        {
            Object.Destroy(collision.gameObject);
            self.GetComponent<BunnyTraits>().foodLevel += 10;
        }
    }

    void mate()
    {
        if (self.GetComponent<BunnyTraits>().canMate())
        {
            Debug.Log(self.name + " Ready To Mate");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale * 4);
    }
}
