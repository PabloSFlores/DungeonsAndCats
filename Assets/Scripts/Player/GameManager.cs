using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    public GameObject dialogBoxNpc;
    public TextMeshProUGUI dialogTextNpc;
    public TextMeshProUGUI nameNpc;
    public Image imageNpc;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText(string text)
    {
        dialogBox.SetActive(true);
        dialogText.text = text;
        Time.timeScale = 0;
    }

    public void HideText()
    {
        dialogBox.SetActive(false);
        dialogText.text = "";
        Time.timeScale = 1;
    }

    public void ShowTextNpc(string text, string name, Sprite image)
    {
        dialogBoxNpc.SetActive(true);
        dialogTextNpc.text = text;
        nameNpc.text = name;
        imageNpc.sprite = image;
        Time.timeScale = 0;
    }

    public void HideTextNpc()
    {
        dialogBoxNpc.SetActive(false);
        dialogTextNpc.text = "";
        nameNpc.text = "";
        imageNpc.sprite = null;
        Time.timeScale = 1;
    }
}
