using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
//using System.Net;

public class DataDictionary : MonoBehaviour
{

    public static DataDictionary dd;

    // declare data to save across scenes here
    private bool isPlaying = true;
    private int levelNumber = 1;
    private int totalScore = 0;
    private int level1Score = 0;
    private bool hasShownInstructions = false;
    private int timeBonus = 0;
    private List<string> hitList = new List<string>();

    // Use this for initialization
    void Awake()
    {
        if (dd == null)
        {
            DontDestroyOnLoad(gameObject);
            dd = this;
            Debug.Log("DataDictionary::Awake: Application.persistentDataPath = [" + Application.persistentDataPath + "]");
            init();
        }
        else if (dd != this)
        {
            Destroy(gameObject); // singleton pattern - do not allow more than one instance
        }
    }

    private void init()
    {
        Debug.Log("DataDictionary::init(): Initializing.");

    }

    public static DataDictionary getDD()
    {
        if (dd == null)
        {
            Debug.Log("DataDictionary::getDD(): DataDictionary set to null! Fatal error.");
            // TODO what do we do if DD does not exist?
        }
        return dd;
    }

    // add setters and getters here
    public int gettotalScore()
    {
        return totalScore;
    }
    public void settotalScore(int value)
    {
        totalScore = value;
    }


    public int getlevel1Score()
    {
        return level1Score;
    }
    public void setlevel1Score(int value)
    {
        level1Score = value;
    }

    public bool gethasShownInstructions()
    {
        return hasShownInstructions;
    }
    public void sethasShownInstructions(bool x)
    {
        hasShownInstructions = x;
    }

    public int gettimeBonus()
    {
        return timeBonus;
    }
    public void settimeBonus(int value)
    {
        timeBonus = value;
    }

    public bool getisPlaying()
    {
        return isPlaying;
    }
    public void setisPlaying(bool value)
    {
        isPlaying = value;
    }

    public void addhitList(string value)
    {
        hitList.Add(value);
    }
    public void clearhitList()
    {
        hitList.Clear();
    }
    public bool hitListContains(string value)
    {
        bool ret = hitList.Contains(value);
        return ret;
    }
} // ends DataDictionary class
