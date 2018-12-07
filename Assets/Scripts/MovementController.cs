using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

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
        seek();
        if(!foodTarget && !breedTarget) { moveRandomly(); }
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
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale * traits.sight, 360);     //take all the colliders inside the box

        if (breedTarget && !colliders.Contains(breedTarget.GetComponent<Collider2D>())){ breedTarget = null; }
        if (foodTarget && !colliders.Contains(foodTarget.GetComponent<BoxCollider2D>())) { foodTarget = null; }

        if (foodTarget == null && breedTarget == null)  //if the individual doesn't have a target
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                //if the collider gameobject is from the same species, opposite gender and can mate set breed target to gameobject
                if (colliders[i].tag == gameObject.tag && colliders[i].gameObject.GetComponent<GA_Traits>().gender != traits.gender 
                    && colliders[i].gameObject.GetComponent<GA_Traits>().canMate() && traits.canMate()) { breedTarget = colliders[i].gameObject; break; }
                //if the collider gameobject has the specific food source tag set food target to gameobject
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
        if (Vector3.Distance(transform.position, foodTarget.transform.position) < 0.1f)
        {
            Destroy(foodTarget.gameObject);
            traits.foodLevel += 5;
            traits.collect++;
        }
    }

    void breed()
    {
        if (transform.position == breedTarget.transform.position)
        {
            traits.foodLevel -= 10;
            population.genIndividual(gameObject.tag, "random"); //create new random individual from the same species
            breedTarget = null;
        }
    }

    void OnDrawGizmos()
    {
        if (gameObject.GetComponent<GA_Traits>().canMate()) { Gizmos.color = Color.red; }
        else { Gizmos.color = Color.blue; }

        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(transform.position, transform.localScale * gameObject.GetComponent<GA_Traits>().sight);
    }

}
