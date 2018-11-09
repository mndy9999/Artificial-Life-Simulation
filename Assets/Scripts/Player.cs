using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

    public Sprite grass;
    public GameObject self;
    public float speed;
    public LayerMask m_LayerMask;

    RandomSpawn r;

    float moveTime;
    int direction;
    Vector3 v;

    bool m_started;

    

	// Use this for initialization
	void Start () {
        v = self.transform.position;
        moveTime = 0;
        m_started = true;
	}
	
	// Update is called once per frame
	void Update () {
        //moveRandomly();
        seek();
        moveByKey();
	}


    void moveRandomly()
    {
        int rand = Random.Range(0, 100);
        if(rand > 70)
        {
            direction = Random.Range(0, 4);
        }
        

        if(Time.time > moveTime)
        {
            moveTime += (10-speed);
            switch (direction)
            {
                case 0:
                    v.y += 1;
                    break;
                case 1:
                    v.y -= 1;
                    break;
                case 2:
                    v.x += 1;
                    break;
                case 3:
                    v.x -= 1;
                    break;
            }
            self.transform.SetPositionAndRotation(v, Quaternion.identity);
        }
    }



    void moveByKey()
    {
        if (Input.GetKeyDown("w"))
        {
            v.y += 1;
            self.transform.SetPositionAndRotation(v, Quaternion.identity);
        }
        else if (Input.GetKeyDown("s"))
        {
            v.y -= 1;
            self.transform.SetPositionAndRotation(v, Quaternion.identity);
        }
        else if (Input.GetKeyDown("a"))
        {
            v.x -= 1;
            self.transform.SetPositionAndRotation(v, Quaternion.identity);
        }
        else if (Input.GetKeyDown("d"))
        {
            v.x += 1;
            self.transform.SetPositionAndRotation(v, Quaternion.identity);
        }

    }

    void seek()
    {
        List<Collider2D> hitColliders = new List<Collider2D>();
        hitColliders.Add(Physics2D.OverlapBox(self.transform.position, transform.localScale*4, 360));
        int i = 0;
        while (i < hitColliders.Count)
        {
            //Debug.Log("Hit: " + hitColliders[i].name + i);
            i++;
        }
        Debug.Log(hitColliders.Count);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale*4);
    }
}
