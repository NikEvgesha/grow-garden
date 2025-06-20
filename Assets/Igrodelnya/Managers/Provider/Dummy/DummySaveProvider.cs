using System;
using System.Collections.Generic;
using UnityEngine;

public class DummySaveProvider : SaveProvider
{
    public override void Initialize() { Debug.Log("DummySaveProvider initialized"); }
    public override float[] LoadVolume() {
        float[] volumes = new float[] { 0.5f, 0.5f };
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            volumes[0] = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            volumes[1] = PlayerPrefs.GetFloat("SoundVolume");
        }
        return volumes;
    }
    public override void SaveGems(int amount) {
        PlayerPrefs.SetInt("Gems", amount);
    }

    public override int LoadGems()
    {
        int gems = 0;
        if (PlayerPrefs.HasKey("Gems"))
        {
            gems = PlayerPrefs.GetInt("Gems");
        }
        return gems;
    }


    public override void SaveCoins(int amount)
    {
        PlayerPrefs.SetInt("Coins", amount);
    }

    public override int LoadCoins()
    {
        int gems = 0;
        if (PlayerPrefs.HasKey("Coins"))
        {
            gems = PlayerPrefs.GetInt("Coins");
        }
        return gems;
    }
    public override void SaveVolume(float musicVolume, float soundVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
    }
    public override void SaveScore(float score, int levelId) { }
    public override float LoadScore(int levelId) => 0;
    public override bool GetTutorialProgress() => false;
    public override void SaveTutorialProgress(bool endTutorial) { }
    public override void SaveQuestProgress(int step) { }
    public override int LoadQuestProgress() => 0;

    public override void SaveAchievementProgress(AchievementType id, int progress)
    {
        PlayerPrefs.SetInt(id.ToString(), progress);
    }

    public override int LoadAchievementProgress(AchievementType id)
    {
        int progress = 0;
        if (PlayerPrefs.HasKey(id.ToString()))
        {
            progress = PlayerPrefs.GetInt(id.ToString());
        }
        return progress;
    }


    public override void SaveAchievementStatus(string id, bool progress)
    {
        PlayerPrefs.SetInt(id, progress ? 1: 0);
    }

    public override bool LoadAchievementStatus(string id)
    {
        int progress = 0;
        if (PlayerPrefs.HasKey(id))
        {
            progress = PlayerPrefs.GetInt(id);
        }
        return progress == 1 ? true : false;
    }


    public override List<string> LoadInventory() {
        return new List<string>();
    }
    public override void SaveRouletteDate(DateTime date)
    {
    }

    public override DateTime LoadRouletteDate()
    {
        return DateTime.Today.AddDays(-1);
    }


    public override bool CheckProgress()
    {
        return false;
    }

    public override void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }

    public override void SaveProgress()
    {

    }
}
