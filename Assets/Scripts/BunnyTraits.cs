using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyTraits : MonoBehaviour
{
    public GameObject self;
    public string name;
    public enum Gender { male, female }
    public Gender gender;
    public int foodLevel;
    public float sigth;
    public int age;
    public float fitness;
    public int speed;
    public int collectScore;

    int initializationTime;
    GameObject fittest;
    GameObject bunniesParent;
    // Use this for initialization
    void Start()
    {
        if (name != "")
        {
            self.name = name;
        }
        foodLevel = 90;
        age = 0;
        speed = 3;
        sigth = 10;
        collectScore = 0;

        initializationTime = (int)Time.realtimeSinceStartup;
        bunniesParent = GameObject.Find("Bunnies");
    }

    // Update is called once per frame
    void Update()
    {
        setTag();
        ageUp();
        checkIfDead();
    }

    void calculateFitness()
    {
        fitness = (foodLevel + sigth + speed) / 3;
    }

    float getFitness()
    {
        return fitness;
    }

    void ageUp()
    {
        age = (int)Time.realtimeSinceStartup - initializationTime;
    }

    void checkIfDead()
    {
        if(age > 70)
        {
            Destroy(gameObject);
        }
    }

    public bool canMate()
    {
        return foodLevel > 70;
    }

    void setTag()
    {
        if (foodLevel > 70)
        {
            if (gender == Gender.male)
            {
                self.gameObject.tag = "male";
            }
            else
            {
                self.gameObject.tag = "female";
            }
        }
        else
        {
            self.gameObject.tag = "Untagged";
        }
    }

    GameObject getFittest()
    {
        float highestFitness = 0;
        for(int i = 0; i < bunniesParent.transform.childCount; i++)
        {
            if(bunniesParent.transform.GetChild(i).GetComponent<BunnyTraits>().getFitness() > highestFitness)//fittest.GetComponent<BunnyTraits>().getFitness())
            {
                highestFitness = bunniesParent.transform.GetChild(i).GetComponent<BunnyTraits>().getFitness();
                fittest = bunniesParent.transform.GetChild(i).gameObject;
            }
        }
        return fittest;
    }

}
