using UnityEngine;
using UnityEngine.UI;

public class GrowProgress : MonoBehaviour
{
    [SerializeField] private Image _progressImg;
    [SerializeField] private Text _progressText;

    public void SetProgress(float progress)
    {
        _progressImg.fillAmount = progress;
        _progressText.text = Mathf.Round(progress * 100).ToString() + "%";
    }
}
