using UnityEngine;
using TMPro;  // Importa TextMeshPro

public class AlphaText : MonoBehaviour
{

    public float speedFade = 1.0f; // Velocidad del fade in-out
    private float count;
    private TextMeshProUGUI textMeshPro; // Referencia al componente TextMeshProUGUI

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente TextMeshProUGUI en este GameObject
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Aumentar el contador para el efecto de fade in-out
        count += speedFade * Time.deltaTime;

        // Modificar el color del texto, ajustando solo el canal alfa
        float alpha = Mathf.Sin(count) * 0.5f + 0.5f; // Alfa oscilante entre 0 y 1
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, alpha);
    }
}
