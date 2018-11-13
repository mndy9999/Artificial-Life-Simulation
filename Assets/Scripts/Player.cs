using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    public GameObject self;
    public float speed;
    public Tilemap map;
    public RandomSpawn spawn;

    Collider2D target;

    float moveTime;
    int direction;
    Vector3 playerPos;
    Vector3 targetPos;



    bool m_started;
    bool foundTarget;
    bool coRun;

	// Use this for initialization
	void Start () {
        playerPos = self.transform.position;
        targetPos = playerPos;
        moveTime = 0;
        map = Component.FindObjectOfType<Tilemap>();
        m_started = true;
        foundTarget = false;
        target = null;
        coRun = false;

    }

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
        //moveRandomly();
       // Debug.Log(" UPDATE ----------\n PlayerPos: " + playerPos + " TargetPos: " + targetPos);

    }

    void seek(string tag, string mate)
    {

        Collider2D[] colliders = Physics2D.OverlapBoxAll(self.transform.position, transform.localScale * 4, 360);

        if (colliders.Length > 1)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (mate != null && colliders[i].tag == mate)
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
                if (target.tag == mate) { breed(target.gameObject); }
                else { moveTowards(target); }
            }
            else
            {
                moveRandomly();
            }
            foundTarget = false;
        }
        else
        {
            moveRandomly();
        }

    }

    void moveTowards(Collider2D col)
    {
        if(!coRun)
        {
            if (Mathf.Round(transform.position.x) < col.transform.position.x) { targetPos.x++; }
            else if (Mathf.Round(transform.position.x) > col.transform.position.x) { targetPos.x--; }
            else if (Mathf.Round(transform.position.y) < col.transform.position.y) { targetPos.y++; }
            else if (Mathf.Round(transform.position.y) > col.transform.position.y) { targetPos.y--; }
        }
        
        StartCoroutine(move());
        
    }

    void moveRandomly()
    {       
        if (!coRun)
        {
            if(Time.time > moveTime)
            {
                int rand = Random.Range(0, 100);
                if(rand > 70)
                {
                    genRandomDirection();
                    
                }
                moveTime += Time.deltaTime;
            }
            playerPos = targetPos;
            if (direction == 0 && playerPos.x < (map.size.x / 2)-1) { targetPos.x++; }
            else if(direction == 1 && playerPos.x > (-map.size.x / 2)) { targetPos.x--; }
            else if(direction == 2 && playerPos.y < (map.size.y / 2)-1) { targetPos.y++; }
            else if(direction == 3 && playerPos.y > (-map.size.y / 2)) { targetPos.y--; }
            else { Debug.Log("Wrong direction??" + direction); }
            checkIfOnEdge();
        }

        StartCoroutine(move());

    }

    IEnumerator move()
    {
        coRun = true;
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 1f * Time.deltaTime);
            yield return null;
        }
        StopAllCoroutines();
        coRun = false;
    }

    int genRandomDirection()
    {
       // Debug.Log("DIRECTION: " + direction);
        direction = Random.Range(0, 4);
        return direction;
    }

    void checkIfOnEdge()
    {
        if (playerPos.x == (map.size.x / 2)-1) { direction = 1; }
        else if (playerPos.x == (-map.size.x / 2)) { direction = 0; }
        else if (playerPos.y == (map.size.y / 2)-1) { direction = 3; }
        else if (playerPos.y == (-map.size.y / 2)) { direction = 2; }
    }

    void moveByKey()
    {
        if (Input.GetKeyDown("w")) { playerPos.y += 1; }
        else if (Input.GetKeyDown("s")) { playerPos.y -= 1; }
        else if (Input.GetKeyDown("a")) { playerPos.x -= 1; }
        else if (Input.GetKeyDown("d")) { playerPos.x += 1; }
        self.transform.SetPositionAndRotation(playerPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.gameObject.tag == "bush")
        {
            Object.Destroy(collision.gameObject);
            self.GetComponent<BunnyTraits>().foodLevel += 10;
            self.GetComponent<BunnyTraits>().collectScore++;
        }
    }

    void breed(GameObject partner)
    {
        transform.position = Vector3.Lerp(transform.position, partner.transform.position, 1 * Time.deltaTime);
        if(Vector3.Distance(transform.position, partner.transform.position) < 1f)
        {
            gameObject.GetComponent<BunnyTraits>().foodLevel -= 50;
            partner.GetComponent<BunnyTraits>().foodLevel -= 50;
            spawn.spawnBunny();
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
        if (gameObject.GetComponent<BunnyTraits>().canMate()) { Gizmos.color = Color.red; }
        else { Gizmos.color = Color.blue; }
        
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale * 4);
    }
}
