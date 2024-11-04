using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIAnimations : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject newGameMenu;

    public void OpenExitMenu()
    {
        LeanTween.scale(exitMenu.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f)
            .setDelay(0.5f)
            .setEase(LeanTweenType.easeOutBack);
        LeanTween.alpha(overlay.GetComponent<RectTransform>(), 0.75f, 1f);
    }

    public void CloseExitMenu()
    {
        LeanTween.scale(exitMenu.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f)
            .setEase(LeanTweenType.easeInBack);
        LeanTween.alpha(overlay.GetComponent<RectTransform>(), 0f, 1f);
    }
    
    public void OpenOptionsMenu()
    {
        LeanTween.scale(optionsMenu.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f)
            .setDelay(0.5f)
            .setEase(LeanTweenType.easeOutBack);
        LeanTween.alpha(overlay.GetComponent<RectTransform>(), 0.75f, 1f);
    }

    public void CloseOpionsMenu()
    {
        LeanTween.scale(optionsMenu.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f)
            .setEase(LeanTweenType.easeInBack);
        LeanTween.alpha(overlay.GetComponent<RectTransform>(), 0f, 1f);
    }
    
    public void OpenNewGameMenu()
    {
        LeanTween.scale(newGameMenu.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f)
            .setDelay(0.5f)
            .setEase(LeanTweenType.easeOutBack);
        LeanTween.alpha(overlay.GetComponent<RectTransform>(), 0.75f, 1f);
    }

    public void CloseNewGameMenu()
    {
        LeanTween.scale(newGameMenu.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f)
            .setEase(LeanTweenType.easeInBack);
        LeanTween.alpha(overlay.GetComponent<RectTransform>(), 0f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
