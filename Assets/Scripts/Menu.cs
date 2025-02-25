using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TMP_InputField playerNameField;
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.LoadHiscore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew(){
        DataManager.Instance.currentPlayerName = playerNameField.text;
        //DataManager.Instance.LoadHiscore();
        SceneManager.LoadScene(1);
    }

    public void ShowHiscoreList(){
        SceneManager.LoadScene(2);
    }

    public void Exit(){
        //DataManager.Instance.SaveHiscore();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
        //Application.OpenURL(webplayerQuitURL);
        #else
        Application.Quit();
        #endif
    }
}
