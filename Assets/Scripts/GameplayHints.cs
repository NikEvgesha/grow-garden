using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct UIHint
{
    public UIHintType type;
    public string hintLocalizationKey;
}

public class GameplayHints : MonoBehaviour
{
    [SerializeField] private Text hintText; // UI Text для отображения подсказки
    [SerializeField] private GameObject hintPanel; // Панель с подсказкой
    [SerializeField] private float fadeDuration = 0.5f; // Длительность анимации появления/исчезания
    [SerializeField] private float displayDuration = 2f; // Длительность показа подсказки

    [SerializeField] private List<UIHint> _hintsList;

    private Color _textColor;

    private readonly Dictionary<UIHintType, string> _hints = new Dictionary<UIHintType, string>();


    private void Awake()
    {
        foreach (var hint in _hintsList)
        {
            _hints.Add(hint.type, hint.hintLocalizationKey);
        }

        hintPanel.SetActive(false);
        _textColor = hintText.color;
        _textColor.a = 0f;
        hintText.color = _textColor;
    }

    public void ShowHint(UIHintType type)
    {
        // Останавливаем предыдущую анимацию, если она есть
        StopAllCoroutines();

        // Устанавливаем текст подсказки
        hintText.text = _hints.TryGetValue(type, out string message) ? message : "Подсказка не найдена";

        // Запускаем анимацию
        StartCoroutine(PlayHintAnimationCoroutine());
    }

    private IEnumerator PlayHintAnimationCoroutine()
    {
        hintPanel.SetActive(true);

        // Появление
        float t = 0;
        _textColor = hintText.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            _textColor.a = t / fadeDuration;
            hintText.color = _textColor;
            yield return null;
        }
        _textColor.a = 1f;
        hintText.color = _textColor;

        // Задержка
        yield return new WaitForSeconds(displayDuration);

        // Исчезание
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            _textColor.a = 1f - (t / fadeDuration);
            hintText.color = _textColor;
            yield return null;
        }
        _textColor.a = 0f;
        hintText.color = _textColor;
        hintPanel.SetActive(false);
    }

}
