using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;

    public static UiManager Instance
    {
        get { return _instance; }
    }
    public Text scoreText;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
    }

    public void SetScoreText(int _score)
    {
        scoreText.text = _score.ToString();
    }
}
