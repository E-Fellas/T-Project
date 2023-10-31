using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InteractPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private Animator _resultAnimation;
    [SerializeField] private string _result = "Interaction_ResultText_Animation";
    // Start is called before the first frame update
    void Start()
    {
        _uiPanel.SetActive(false);
    }

    public bool IsDisplayed = false;
    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        _uiPanel.SetActive(false);
        IsDisplayed = false;
    }
    
    public void Result (string resultText)
    {
        _resultText.text = resultText;
        _resultAnimation.Play(_result, 0, 0.0f);
    }
}
