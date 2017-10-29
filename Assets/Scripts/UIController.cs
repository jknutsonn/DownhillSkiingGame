using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private Text textTotalScoreOnHUD;
    private Text textLevelScoreOnHUD;
    private Text textForwardOnHUD;
    private Text textMoveOnHUD;
    private Text textScoreOnHUD;
    private Text textTimeBonusOnHUD;

    // Use this for initialization
    void Start () {
        textTotalScoreOnHUD = GameObject.Find("Canvas/TextTotalScore").GetComponent<Text>();
        textLevelScoreOnHUD = GameObject.Find("Canvas/TextLevelScore").GetComponent<Text>();
        textForwardOnHUD = GameObject.Find("Canvas/TextForward").GetComponent<Text>();
        textMoveOnHUD = GameObject.Find("Canvas/TextMove").GetComponent<Text>();
        textScoreOnHUD = GameObject.Find("Canvas/TextScore").GetComponent<Text>();
        textTimeBonusOnHUD = GameObject.Find("Canvas/TextTimeBonus").GetComponent<Text>();

        textTotalScoreOnHUD.text = "Total Score: " + DataDictionary.getDD().gettotalScore();
        textLevelScoreOnHUD.text = "Current Level Score: ";
        textForwardOnHUD.text = "Press the 'w' key to ski";
        textMoveOnHUD.text = "Steer using the mouse and 'a' and 'd' keys";
        textScoreOnHUD.text = "Steer through flags and pick up snowballs to earn points";
        textTimeBonusOnHUD.text = "Time Bonus: ";
        textForwardOnHUD.enabled = false;
        textMoveOnHUD.enabled = false;
        textScoreOnHUD.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        // Game is running
        if (DataDictionary.getDD().getisPlaying())
        {
            textLevelScoreOnHUD.text = "Current Level Score: " + DataDictionary.getDD().getlevel1Score();
            updateTimeBonus();
 
            if (!DataDictionary.getDD().gethasShownInstructions())
            {
                forward();
                DataDictionary.getDD().sethasShownInstructions(true);
            }
        } else
        {
            // Game is not running
            textTotalScoreOnHUD.text = "Total Score: " + DataDictionary.getDD().gettotalScore();
            textTimeBonusOnHUD.enabled = false;
            textLevelScoreOnHUD.enabled = false;

            textMoveOnHUD.text = "Press 'q' to quit";
            textMoveOnHUD.enabled = true;
            
            StartCoroutine("endCoroutine");
        }
    }

    IEnumerator endCoroutine()
    {
        while (!Input.GetKeyDown("q"))
            yield return null;
        Debug.Log("Quitting app");
        Application.Quit();
    }

    private void updateTimeBonus()
    {
        int t = (int) Math.Round(100 - 2*Time.timeSinceLevelLoad);

        textTimeBonusOnHUD.text = "Time Bonus: " + t;
        DataDictionary.getDD().settimeBonus(t);
    }

    /// <summary>
    /// Displays information on how to move forward
    /// </summary>
    private void forward()
    {
        textForwardOnHUD.enabled = true;
        StartCoroutine("forwardCoroutine");
    }

    IEnumerator forwardCoroutine()
    {
        while (!Input.GetKeyDown("w"))
            yield return null;
        textForwardOnHUD.enabled = false;
        StartCoroutine("moveCoroutine");
    }

    /// <summary>
    /// Displays moving directions for 5 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator moveCoroutine()
    {
        textMoveOnHUD.enabled = true;
        yield return new WaitForSeconds(5);
        StartCoroutine("scoreCoroutine");
        textMoveOnHUD.enabled = false;
    }

    /// <summary>
    /// Displays how to score points
    /// </summary>
    /// <returns></returns>
    IEnumerator scoreCoroutine()
    {
        textScoreOnHUD.enabled = true;
        yield return new WaitForSeconds(5);
        textScoreOnHUD.enabled = false;
    }
}
