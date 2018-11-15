using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyTraits : MonoBehaviour
{
    public string name;
    public int age;

    public enum Gender { male, female }
    public Gender gender;

    public int speed = 0;
    public float sigth = 0;  
    public int collect = 0;
    public float foodLevel = 0;
    public float health = 0;

    public float fitness;

    int initializationTime;
    GameObject fittest;
    GameObject bunniesParent;
    // Use this for initialization
    void Start()
    {
        if (name != "")
        {
            transform.name = name;
        }
        fitness = 0;
        age = 0;
        foodLevel = 50;
        collect = 0;
        initializationTime = (int)Time.realtimeSinceStartup;
        genRandomTraits();
               
    }

    // Update is called once per frame
    void Update()
    {
        ageUp();
        checkIfDead();
    }


    void ageUp()
    {
        age = (int)Time.realtimeSinceStartup - initializationTime;
    }

    void checkIfDead()
    {
        if(age > 100 || foodLevel<=0)
        {
            Destroy(gameObject);
        }
    }

    public bool canMate()
    {
        return foodLevel > 50;
    }

    void genRandomTraits()
    {
        int rand = Random.Range(1, 4);
        speed = rand;
        rand = Random.Range(2, 6);
        sigth = rand;
        rand = Random.Range(0, 3);
        health = rand;

    }

}
