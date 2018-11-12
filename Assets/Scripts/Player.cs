using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    public GameObject self;
    public float speed;
    public Tilemap map;

    Collider2D target;

    float moveTime;
    int direction;
    Vector3 playerPos;
    Vector3 targetPos;

    bool m_started;
    bool foundTarget;
    

	// Use this for initialization
	void Start () {
        playerPos = self.transform.position;
        moveTime = 0;
        map = Component.FindObjectOfType<Tilemap>();
        m_started = true;
        foundTarget = false;
        target = null;

    }

    [SerializeField]
    float frameSpeed = 0.05f;
    float count = 0.0f;
    float epoch = 1f;

    // Update is called once per frame
    void Update()
    {

        playerPos = transform.position;
        
        if (self.GetComponent<BunnyTraits>().canMate())
            {
                if (self.GetComponent<BunnyTraits>().gender == BunnyTraits.Gender.male) { seek("bush", "female"); }
                else { seek("bush", "male"); }
            }
            else
            {
                seek("bush", "null");
            }
           
        
       
    }

    void seek(string tag, string mate)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(self.transform.position, transform.localScale * 4, 360);

        if (colliders.Length > 1)
        {
            for(int i = 0; i < colliders.Length; i++)
            {
               // Debug.Log(self.gameObject.name + " COLLIDERS: \n" + colliders[i].gameObject.name);
                if(mate != null && colliders[i].tag == mate)
                {
                    foundTarget = true;
                    target = colliders[i];
                    break;
                }
                else if (colliders[i].tag == tag)
                {
                    foundTarget = true;
                    target = colliders[i];
                    break;
                }
            }
            if (foundTarget)
            {
               // Debug.Log(self.gameObject.name + "    Go towards: " + target.gameObject.name);
                moveTowards(target);
                breed(target.gameObject);
            }
            else
            {
                // Debug.Log(self.gameObject.name + "    Move Randomly");
                moveRandomly();
            }
            foundTarget= false;
        }
        else
        {
            moveRandomly();
        }

    }

    void moveTowards(Collider2D col)
    {
        targetPos = col.transform.position;
        if (playerPos.x < col.transform.position.x)
        {
            targetPos.x++;
        }
        else if (playerPos.x > col.transform.position.x)
        {
            targetPos.x--;
        }
        else if (playerPos.y < col.transform.position.y)
        {
            targetPos.y++;
        }
        else if (playerPos.y > col.transform.position.y)
        {
            targetPos.y--;
            
        }
        Debug.Log(gameObject.name + ": MOVE TOWARDS");
        StartCoroutine(move(targetPos));
    }

    void moveRandomly()
    {
        targetPos = playerPos;

        int rand = Random.Range(0, 100);
        if (rand > 70)
        {
            direction = Random.Range(0, 4);
        }
        

        switch (direction)
        {
            case 0:
                if (playerPos.x < (map.size.x / 2) - 1)
                {
                    targetPos.x++;
                }
                break;
            case 1:
                if (playerPos.x > (-map.size.x / 2))
                {
                    targetPos.x--;
                }
                break;
            case 2:
                if (playerPos.y < (map.size.y / 2) - 1)
                {
                    targetPos.y++;
                }
                break;
            case 3:
                if (playerPos.y > (-map.size.y / 2))
                {
                    targetPos.y--;
                }
                break;
        }
        Debug.Log(gameObject.name + ": MOVE RANDOMLY");


        StartCoroutine(move(targetPos));

    }

    IEnumerator move(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(playerPos, targetPos, 1 * Time.deltaTime);
        
        yield return null;
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

    /// <summary>
    /// BUG: food level goes down randomly
    /// </summary>
    /// <param name="partner"></param>
    void breed(GameObject partner)
    {
        if(playerPos == partner.transform.position)
        {        
          //  this.GetComponent<BunnyTraits>().foodLevel -= 50;
          //  partner.GetComponent<BunnyTraits>().foodLevel -= 50;
            
        }
    }

    public string getTarget()
    {
        if(target) { return target.name; }
        else { return "NULL"; }       
    }

    Vector3Int genRandomPos()
    {
        return new Vector3Int(Random.Range(-map.size.x / 2, map.size.x / 2), Random.Range(-map.size.y / 2, map.size.y / 2), 0);
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
