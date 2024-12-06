using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    /*VARIABLES PARA LA VIDA DEL JUGADOR*/
    public int hp = 300; // Máxima vida del personaje
    public int currentHearts = 3; // Máximo número de corazones

    public Image[] playerHearts; // Array de imágenes de los corazones
    public Sprite[] heartStatus;

    static int minHearts = 3;
    static int maxHearts = 3;

    /*VARIABLES PARA LOS DIALOG - NPC*/
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    public GameObject dialogBoxNpc;
    public TextMeshProUGUI dialogTextNpc;
    public TextMeshProUGUI nameNpc;
    public Image imageNpc;

    private void Awake()
    {
        DataInstance.Instance.LoadData();
        currentHearts = DataInstance.Instance.currentHearts;
        hp = DataInstance.Instance.hp;
    }

    void Start()
    {
        UpdateHearts();
    }

    public bool CanHeal()
    {
        return hp < currentHearts * 4;
    }

    public void IncreaseMaxHP()
    {
        currentHearts++;
        currentHearts = Mathf.Clamp(currentHearts, minHearts, maxHearts);
        hp = currentHearts * 3;
        UpdateHearts();
    }

    public void UpdateCurrentHP(int value)
    {
        // Ajustar la vida del personaje
        hp += value;
        hp = Mathf.Clamp(hp, 0, currentHearts * 100); // Limitar entre 0 y 300

        UpdateHearts(); // Actualizar los corazones visualmente
    }

    private void UpdateHearts()
    {
        int auxHp = hp; // Variable auxiliar para calcular el estado de cada corazón

        for (int i = 0; i < playerHearts.Length; i++)
        {
            if (i < currentHearts) // Solo actualizamos corazones activos
            {
                playerHearts[i].enabled = true; // Hacemos visible el corazón

                if (auxHp >= 100)
                {
                    playerHearts[i].sprite = heartStatus[0]; // Corazón lleno
                }
                else if (auxHp >= 50)
                {
                    playerHearts[i].sprite = heartStatus[1]; // Corazón medio
                }
                else
                {
                    playerHearts[i].sprite = heartStatus[2]; // Corazón vacío
                }

                auxHp -= 100; // Restar 100 de vida para el siguiente corazón
            }
            else
            {
                playerHearts[i].enabled = false; // Desactivar corazones adicionales
            }
        }
    }

    private Sprite GetHeartsStatus(int x)
    {
        if (x >= 3) return heartStatus[0]; // Corazón lleno
        if (x == 2) return heartStatus[1]; // Corazón medio lleno
        return heartStatus[2]; // Corazón vacío
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