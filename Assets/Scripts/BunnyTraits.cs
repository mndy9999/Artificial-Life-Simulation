using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyTraits : MonoBehaviour
{
    public string name;
    public int age;

    public enum Gender { male, female }
    public Gender gender;

    public int speed;
    public float sigth;  
    public int collectScore;
    public float foodLevel;

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
        age = 0;
        foodLevel = 50;
        collectScore = 0;
        genRandomTraits();
        initializationTime = (int)Time.realtimeSinceStartup;
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

    }

}
