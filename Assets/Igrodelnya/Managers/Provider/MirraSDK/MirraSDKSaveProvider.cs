using UnityEngine;
using MirraGames.SDK;
using System.Collections.Generic;
using System;  // доступ к MirraSDK.Data

[System.Serializable]
public class ListSaver
{
    public List<string> list = new();
}
[Serializable]
public class SavedItem //Положение, статус, 
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
    public ItemStatus status;
}
[System.Serializable]
public class SavedItems
{
    public List<SavedItem> list = new();
}
public class MirraSDKSaveProvider : SaveProvider
{
    private bool isInitialize;
    public override void Initialize()
    {
        // Дождёмся полной готовности системы сохранений
        MirraSDK.WaitForProviders(() =>
        {
            Debug.Log("MirraSDKSaveProvider initialized");
            isInitialize = true;
        });
    }

    public override float[] LoadVolume()
    {
        if (!isInitialize)
            return new float[] { 0.5f, 0.5f }; ;
        // Достаём значения, с дефолтом 0.5f
        float music = MirraSDK.Data.GetFloat(SaveKey.MusicVolume.ToString(), 0.5f);
        float sound = MirraSDK.Data.GetFloat(SaveKey.SoundVolume.ToString(), 0.5f);
        return new float[] { music, sound };
    }

    public override void SaveVolume(float musicVolume, float soundVolume)
    {
        if (!isInitialize) return;
        MirraSDK.Data.SetFloat(SaveKey.MusicVolume.ToString(), musicVolume);
        MirraSDK.Data.SetFloat(SaveKey.SoundVolume.ToString(), soundVolume);
        Changed = true;
    }

    public override void SaveScore(float score, int levelId)
    {
        if (!isInitialize) return;
        // Ключ «Score_1», «Score_2» и т.д.
        MirraSDK.Data.SetFloat($"{SaveKey.Score_}{levelId}", score);
        Changed = true;
    }

    public override float LoadScore(int levelId)
    {
        if (!isInitialize)
            return 0f;
        return MirraSDK.Data.GetFloat($"{SaveKey.Score_}{levelId}", 0f);
    }

    public override void ResetProgress()
    {
        MirraSDK.Data.DeleteAll();
    }

    public override void SaveTutorialProgress(bool endTutorial)
    {
        if (!isInitialize) return;
        MirraSDK.Data.SetBool(SaveKey.EndTutorial.ToString(), endTutorial);
        Changed = true;
    }
    public override void SaveQuestProgress(int step)
    {
        if (!isInitialize) return;
        MirraSDK.Data.SetInt(SaveKey.QuestProgress.ToString(), step);
        Changed = true;
    }
    public override int LoadQuestProgress()
    {
        if (!isInitialize) return 0;
        return MirraSDK.Data.GetInt(SaveKey.QuestProgress.ToString(), 0);
    }
    public override bool GetTutorialProgress()
    {
        if (!isInitialize) return false;
        return MirraSDK.Data.GetBool(SaveKey.EndTutorial.ToString(), false);
    }

    public override void SaveGems(int amount)
    {
        if (!isInitialize) return;
        Changed = true;
        MirraSDK.Data.SetInt(SaveKey.Gems.ToString(), amount);
    }

    public override int LoadGems()
    {
        if (!isInitialize)
            return 0;
        return MirraSDK.Data.GetInt(SaveKey.Gems.ToString(), 0);
    }


    public override void SaveCoins(int amount)
    {
        if (!isInitialize) return;
        Changed = true;
        MirraSDK.Data.SetInt(SaveKey.Coins.ToString(), amount);
    }

    public override int LoadCoins()
    {
        if (!isInitialize)
            return 0;
        return MirraSDK.Data.GetInt(SaveKey.Coins.ToString(), 0);
    }

    public override void SaveProgress()
    {
        if (!isInitialize) return;
        // Синхронизировать все изменения с провайдером (локальным или облачным)
        if (Changed)
        {
            MirraSDK.Data.Save();
            Changed = false;
        }
    }

    public override bool CheckProgress()
    {
        if (!isInitialize) return false;
        // Есть ли хоть что-то из основных ключей?
        return MirraSDK.Data.GetBool(SaveKey.Save.ToString(), false);
    }


    public override void SaveAchievementProgress(AchievementType id, int progress)
    {
        if (!isInitialize) return;
        Changed = true;
        MirraSDK.Data.SetInt(id.ToString(), progress);
    }

    public override int LoadAchievementProgress(AchievementType id)
    {
        if (!isInitialize)
            return 0;
        return MirraSDK.Data.GetInt(id.ToString(), 0);
    }

    public override void SaveAchievementStatus(string id, bool rewarded)
    {
        if (!isInitialize) return;
        Changed = true;
        MirraSDK.Data.SetBool(id.ToString(), rewarded);
    }

    public override bool LoadAchievementStatus(string id)
    {
        if (!isInitialize)
            return false;
        return MirraSDK.Data.GetBool(id.ToString(), false);
    }

    public override List<string> LoadInventory()
    {
        ListSaver items = new();
        if (isInitialize)
        {
            items = MirraSDK.Data.GetObject<ListSaver>(SaveKey.InventoryList.ToString(), new ListSaver());
        }
        return items.list;
    }
    
    public override void SaveRouletteDate(DateTime date)
    {
        if (!isInitialize) return;
        Changed = true;
        MirraSDK.Data.SetString(SaveKey.RouletteLastDate.ToString(), date.Date.ToString());
        Debug.Log("Date saved: " + date.ToString());
    }

    public override DateTime LoadRouletteDate()
    {
        if (!isInitialize) return DateTime.Today.AddDays(-1);

        string date = MirraSDK.Data.GetString(SaveKey.RouletteLastDate.ToString());
        Debug.Log("Date loaded: " + date);
        if (date.Length == 0)
        {
            return DateTime.Today.AddDays(-1);
        }
        return DateTime.Parse(date);
    }

}
