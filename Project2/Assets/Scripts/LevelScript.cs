using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    [SerializeField] Text[] objTexts;
    [SerializeField] Text completionText;
    GameObject[] enemies;
    DeadCheck deadCheck;
    bool advanceObj;
    List<bool> objectives = new List<bool>();
    int objIndex;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemies1");
        objIndex = 0;

        for (int i = 0; i < objTexts.Length; i++)
        {
            objectives.Add(false);
            objTexts[i].CrossFadeAlpha(0, 0f, false);
        }

        completionText.CrossFadeAlpha(0, 0f, false);

        advanceObj = false;
    }
    
    //check if enemies are still alive
    void Update()
    {
        //fade in and out text for each objective
        if (!objectives[objIndex])
        {
            StartCoroutine(FadeInOut(objIndex, 2, 3));
            objectives[objIndex] = true;
        }

        //check if obj is complete, set flag to false if not
        enemies = GameObject.FindGameObjectsWithTag("Enemies" + (objIndex + 1));
        for (int i = 0; i < enemies.Length; i++) {
            deadCheck = enemies[i].transform.GetComponent<DeadCheck>();
            if (!deadCheck.dead)
            {
                advanceObj = false;
            }
        }

        //advance obj
        if (advanceObj)
        {
            objIndex++;
            Debug.Log("next objective");
        }

        advanceObj = true;

        if (objIndex == objTexts.Length)
        {
            //prevent overflow error
            objIndex--;

            //end mission
            StartCoroutine(End());
        }


    }

    //fade objective text
    IEnumerator FadeInOut(int objNum, float timeIn, float timeOut)
    {
        Debug.Log("fading");
        objTexts[objNum].CrossFadeAlpha(0, 0f, false);
        yield return new WaitForSeconds(timeIn);
        objTexts[objNum].CrossFadeAlpha(1, 3f, false);
        yield return new WaitForSeconds(timeOut);
        objTexts[objNum].CrossFadeAlpha(0, 5f, false);
    }

    IEnumerator End()
    {
        completionText.CrossFadeAlpha(1, 3f, false);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("End");
    }
}
