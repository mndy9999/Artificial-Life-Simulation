using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyTraits : MonoBehaviour
{
    public GameObject self;
    public string name;
    public enum Gender
    {
        male,
        female
    }
    public Gender gender;
    public int foodLevel;
    public float sigth;
    public int age;

    int initializationTime;

    // Use this for initialization
    void Start()
    {
        if (name != "")
        {
            self.name = name;
        }
        foodLevel = 90;
        age = 0;

        initializationTime = (int)Time.realtimeSinceStartup;
        Debug.Log("TIME: " + initializationTime);
    }

    // Update is called once per frame
    void Update()
    {
        setTag();
        ageUp();
        checkIfDead();
    }

    void ageUp()
    {
        age = (int)Time.realtimeSinceStartup - initializationTime;
    }

    void checkIfDead()
    {
        if(age > 50)
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





}
