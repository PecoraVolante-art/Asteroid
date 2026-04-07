using UnityEngine;
using UnityEngine.UI;

public class UIbuttonSFX : MonoBehaviour
{
    public AudioClip clickClip;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        if (GestioneSFX.Instance != null && clickClip != null)
        {
            // Aggiunge il suono al pulsante senza cancellare altri suoni
            GestioneSFX.Instance.AddSFXToButton(button, clickClip);
        }
    }
}
