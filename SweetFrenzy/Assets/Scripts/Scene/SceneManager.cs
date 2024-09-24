using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() //Cargar la pantalla de selección de niveles
    {
        SceneManager.LoadScene("PlayerModeScene");
    }

    public void ContinueGame() //Continuar desde el último punto de guardado
    {

    }

    public void QuitGame() //Salir del juego
    {
        Application.Quit();
    }

    public void ChangeLevel(string level) //Cambiar de escena por su nombre
    {
        SceneManager.LoadScene(level);
    }

    public void EnableLevel(Button button) //Activar el botón que no está interactuable
    {
        button.interactable = true;
    }
}
