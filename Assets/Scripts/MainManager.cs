using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HiScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int hs_points;
    private string hs_name;
    private string playerName;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        hs_name = DataManager.Instance.hiscoreName;
        hs_points = DataManager.Instance.hiscore;
        HiScoreText.text = "Best Score : "+hs_name+" : "+hs_points+"";
        playerName = DataManager.Instance.currentPlayerName;
        ScoreText.text = $"{playerName} : {m_Points}";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Escape)){
                DataManager.Instance.SaveHiscore();
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{playerName} : {m_Points}";
    }

    public void GameOver()
    {
        if(m_Points>hs_points){
            hs_name = playerName;
            hs_points = m_Points;
        }
        DataManager.Instance.hiscoreName = playerName;
        DataManager.Instance.hiscore = m_Points;
        DataManager.Instance.UpdateHiscore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void Exit(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
        //Application.OpenURL(webplayerQuitURL);
        #else
        Application.Quit();
        #endif
    }

    private void OnApplicationQuit(){
        DataManager.Instance.SaveHiscore();
    }
}
