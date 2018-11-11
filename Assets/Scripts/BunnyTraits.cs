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


    // Use this for initialization
    void Start()
    {
        if (name != "")
        {
            self.name = name;
        }
        foodLevel = 70;
    }

    // Update is called once per frame
    void Update()
    {
        setTag();
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
    }





}
