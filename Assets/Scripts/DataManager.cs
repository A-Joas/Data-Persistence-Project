using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string currentPlayerName;
    public string hiscoreName;
    public int hiscore;

    SaveData data;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        data = new SaveData();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if(Instance==this){
            Instance = null;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string[] HiscorePlayerName;
        public int[] StoredHiscore;

        public SaveData(){
            HiscorePlayerName = new string[10];
            StoredHiscore = new int[10];
        }
    }

    public string[] GetSavedNames(){
        return data.HiscorePlayerName;
    }

    public int[] GetSavedScores(){
        return data.StoredHiscore;
    }

    public void SaveHiscore(){
        //SaveData data = new SaveData();
        /*for(int i=0;i<10;i++){
            if(hiscore>data.StoredHiscore[i]){
                for(int j=9;j>i;j--){
                    data.StoredHiscore[j]=data.StoredHiscore[j-1];
                    data.HiscorePlayerName[j]=data.HiscorePlayerName[j-1];
                }
                data.StoredHiscore[i]=hiscore;
                data.HiscorePlayerName[i]=hiscoreName;
                break;
            }
        }*/
        //data.HiscorePlayerName = hiscoreName;
        //data.StoredHiscore = hiscore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void UpdateHiscore(){
        for(int i=0;i<10;i++){
            if(hiscore>data.StoredHiscore[i]){
                for(int j=9;j>i;j--){
                    data.StoredHiscore[j]=data.StoredHiscore[j-1];
                    data.HiscorePlayerName[j]=data.HiscorePlayerName[j-1];
                }
                data.StoredHiscore[i]=hiscore;
                data.HiscorePlayerName[i]=hiscoreName;
                hiscore=data.StoredHiscore[0];
                hiscoreName=data.HiscorePlayerName[0];
                break;
            }
        }
    }

    public void LoadHiscore(){
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);

            hiscore = data.StoredHiscore[0];
            hiscoreName = data.HiscorePlayerName[0];
        }
        else
        {
            for(int i=0;i<10;i++){
                data.StoredHiscore[i]=0;
                data.HiscorePlayerName[i]="Nil";
            }
            hiscore = 0;
            hiscoreName = "Nil";
        }
    }
}
