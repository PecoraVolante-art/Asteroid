using UnityEngine;
using UnityEngine.SceneManagement;
public class Impostazione_livello : MonoBehaviour
{
    //Creazione dei bottoni per caricare le scene
    public void Playbutton()// Carica scena livello 1 
    {
        SceneManager.LoadScene("Asteroid");
    }

    public void Goback_Menubutton()// Carica scena Menu
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}