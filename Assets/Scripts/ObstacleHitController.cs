using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleHitController : MonoBehaviour {

    private Text obstacleTextOnHUD;
    private bool first = true;

	// Use this for initialization
	void Start () {
        obstacleTextOnHUD = GameObject.Find("Canvas/TextObstacle").GetComponent<Text>();

        obstacleTextOnHUD.text = "Watch out for obstacles!";
        obstacleTextOnHUD.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!DataDictionary.getDD().getisPlaying() && first == true)
        {
            first = false;
            int sc = DataDictionary.getDD().getlevel1Score() + DataDictionary.getDD().gettimeBonus();
            obstacleTextOnHUD.text = "Level Score: " + sc;
            obstacleTextOnHUD.enabled = true;
            StartCoroutine(FadeTextToFullAlpha(1f, obstacleTextOnHUD));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Help: " + collision.gameObject.name);
        if (collision.gameObject.name.StartsWith("Pine_"))
        {
            StartCoroutine("obstacleCoroutine");
            ResetScore();
        }

    }

    private void ResetScore()
    {
        DataDictionary.getDD().setlevel1Score(0);
        DataDictionary.getDD().clearhitList();
    }
   
    IEnumerator obstacleCoroutine()
    {
        obstacleTextOnHUD.enabled = true;
        StartCoroutine(FadeTextToFullAlpha(1f, obstacleTextOnHUD));
        yield return new WaitForSeconds(5);
        obstacleTextOnHUD.enabled = false;
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

}
