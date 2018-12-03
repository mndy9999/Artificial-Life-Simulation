using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour {

    char direction;

    Vector3 playerPos;
    Vector3 targetPos;

    GameObject foodTarget;
    GameObject breedTarget;

    Tilemap map;

    GA_Population population;
    GA_Traits traits;

    float time;
    float speed;

    private void Start()
    {
        map = Component.FindObjectOfType<Tilemap>();
        population = FindObjectOfType<GA_Population>();
        traits = gameObject.GetComponent<GA_Traits>();
    }

    private void Update()
    {     
        targetPos = transform.position;
        time -= Time.deltaTime;
        if(time <= 0) { chooseDirection(); time = 2; }
        checkForInput();
        if(!foodTarget && !breedTarget) { moveRandomly(); seek(); }
        else if (breedTarget) { moveTowards(breedTarget.transform.position); breed(); }
        else if(foodTarget){ moveTowards(foodTarget.transform.position); eat(); }
    }

    void moveRandomly()
    {
        if (direction == 'n' && transform.position.y < (map.size.y / 2) - 1) { targetPos.y++; }
        else if (direction == 's' && transform.position.y > -map.size.y / 2) { targetPos.y--; }
        else if (direction == 'e' && transform.position.x < (map.size.x / 2) - 1) { targetPos.x++; }
        else if (direction == 'w' && transform.position.x > -map.size.x / 2) { targetPos.x--; }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, traits.speed * Time.deltaTime);
    }

    void chooseDirection()
    {
        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0: direction = 'n'; break;
            case 1: direction = 's'; break;
            case 2: direction = 'e'; break;
            case 3: direction = 'w'; break;
        }
    }

    void checkForInput()
    {
        if (Input.GetKeyDown("w")) { direction = 'n'; }
        else if (Input.GetKeyDown("s")) { direction = 's'; }
        else if (Input.GetKeyDown("d")) { direction = 'e'; }
        else if (Input.GetKeyDown("a")) { direction = 'w'; }
    }

    void seek()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale * traits.sight, 360);
        if (foodTarget == null && breedTarget == null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == gameObject.tag && colliders[i].gameObject.GetComponent<GA_Traits>().gender != traits.gender && colliders[i].gameObject.GetComponent<GA_Traits>().canMate() && traits.canMate()) { breedTarget = colliders[i].gameObject; break; }
                if (colliders[i].tag == traits.foodSource) { foodTarget = colliders[i].gameObject; break; }                
            }
        }
    }

    void moveTowards(Vector3 tp)
    {
        transform.position = Vector3.MoveTowards(transform.position, tp, traits.speed * Time.deltaTime);
    }

    void eat()
    {
        if (transform.position == foodTarget.transform.position)
        {
            Destroy(foodTarget.gameObject);
            traits.foodLevel++;
            traits.collect++;
        }
    }

    void breed()
    {
        if (transform.position == breedTarget.transform.position)
        {
            traits.foodLevel -= 10;
            population.genIndividual(gameObject.tag, "random");
            breedTarget = null;
        }
    }

    void OnDrawGizmos()
    {
        if (gameObject.GetComponent<GA_Traits>().canMate()) { Gizmos.color = Color.red; }
        else { Gizmos.color = Color.blue; }

        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (Time.realtimeSinceStartup > 0)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale * gameObject.GetComponent<GA_Traits>().sight);
    }

}
