using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveProvider : MonoBehaviour
{
    public bool Changed;
    public abstract void Initialize();

    // Методы для работы с громкостью
    public abstract float[] LoadVolume();
    public abstract void SaveVolume(float musicVolume, float soundVolume);

    // Методы для работы со счётом
    public abstract void SaveScore(float score, int levelId);
    public abstract float LoadScore(int levelId);
    public abstract bool GetTutorialProgress();
    public abstract void SaveTutorialProgress(bool endTutorial);
    public abstract void SaveQuestProgress(int step);
    public abstract int LoadQuestProgress();
    // Прочие методы (например, сохранение статуса уровней)

    public abstract void SaveGems(int amount);
    public abstract int LoadGems();

    public abstract void SaveCoins(int amount);
    public abstract int LoadCoins();

    public abstract void SaveAchievementProgress(AchievementType id, int progress);
    public abstract int LoadAchievementProgress(AchievementType id);

    public abstract void SaveAchievementStatus(string id, bool progress);
    public abstract bool LoadAchievementStatus(string id);

    public abstract List<string> LoadInventory();
    public abstract void SaveRouletteDate(DateTime date);

    public abstract DateTime LoadRouletteDate();

    public abstract bool CheckProgress();

    public abstract void ResetProgress();
    public abstract void SaveProgress();

}
