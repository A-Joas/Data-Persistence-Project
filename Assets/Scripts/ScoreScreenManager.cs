using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreScreenManager : MonoBehaviour
{
    public TMP_Text nameList;
    public TMP_Text scoreList;
    private string[] nameData;
    private int[] scoreData;
    // Start is called before the first frame update
    void Start()
    {
        nameData = DataManager.Instance.GetSavedNames();
        scoreData = DataManager.Instance.GetSavedScores();
        nameList.text = nameData[0]+":";
        scoreList.text = scoreData[0].ToString();
        for(int i=1;i<10;i++){
            nameList.text += "\n"+nameData[i]+":";
            scoreList.text += "\n"+scoreData[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }
}
