using UnityEngine;
using System.Collections;
using System;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance => _instance;

    [SerializeField] private SaveProvider saveProvider; // Ќазначаем в инспекторе нужный провайдер (YG2SaveProvider, DebugSaveProvider и т.д.)
    [SerializeField] private bool _newPlayer;
    public bool IsNewPlayer => saveProvider.CheckProgress() == false;

    private void Awake()
    {

        if (_newPlayer)
        {
            saveProvider.ResetProgress();
        }

        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
            saveProvider.Initialize();
            StartCoroutine(ProgressSavingRoutine());
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private IEnumerator ProgressSavingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            saveProvider.SaveProgress();
        }
    }

    // ѕример методов, которые делегируют работу провайдеру:
    public float[] GetVolume()
    {
        return saveProvider.LoadVolume();
    }
    public void SaveQuestProgress(int step = 0)
    {
        saveProvider.SaveQuestProgress(step);
    }
    public int LoadQuestProgress() 
    {
        return saveProvider.LoadQuestProgress();
    }
    public void SaveMusicVolume(float volume)
    {
        var volumes = saveProvider.LoadVolume();
        saveProvider.SaveVolume(volume, volumes[1]);
    }

    public void SaveSoundVolume(float volume)
    {
        var volumes = saveProvider.LoadVolume();
        saveProvider.SaveVolume(volumes[0], volume);
    }

    public void SaveScore(float score, int levelId)
    {
        saveProvider.SaveScore(score, levelId);
    }

    public float GetLevelScore(int levelId)
    {
        return saveProvider.LoadScore(levelId);
    }

    public bool GetTutorialProgress()
    {
        return saveProvider.GetTutorialProgress();
    }
    public void SaveTutorialProgress(bool endTutorial)
    {
        saveProvider.SaveTutorialProgress(endTutorial);
    }
    public void SaveGems(int amount)
    {
        saveProvider.SaveGems(amount);
        //LeaderboardManager.Instance.SaveScore(LBName.gems.ToString(), amount);
    }

    public int LoadGems()
    {
        return saveProvider.LoadGems();
    }


    public void SaveCoins(int amount)
    {
        saveProvider.SaveCoins(amount);
        //LeaderboardManager.Instance.SaveScore(LBName.gems.ToString(), amount);
    }

    public int LoadCoins()
    {
        return saveProvider.LoadCoins();
    }

    public void SaveAchiementTypeProgress(AchievementType achievementType, int progress)
    {
        saveProvider.SaveAchievementProgress(achievementType, progress);
    }

    public int GetAchievementTypeProgress(AchievementType achievementType)
    {
        return saveProvider.LoadAchievementProgress(achievementType);
    }

    public void SaveAchiementStatus(string achievementID, bool progress)
    {
        saveProvider.SaveAchievementStatus(achievementID, progress);
    }

    public bool GetAchievementStatus(string achievementID)
    {
        return saveProvider.LoadAchievementStatus(achievementID);
    }



    public void SaveGameProgress()
    { 

        Debug.Log("Progress Saved");
       
    }
    
    public void ResetGameProgress() => SaveGameProgress();

    
    public void SaveRouletteDate(DateTime date)
    {
        saveProvider.SaveRouletteDate(date);
    }

    public DateTime LoadRouletteDate()
    {
        return saveProvider.LoadRouletteDate();
    }

    
}
