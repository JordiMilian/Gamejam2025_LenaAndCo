using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Control : MonoBehaviour
{
    public void Button_Comencar()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Button_Sortir()
    {
        Application.Quit();
    }
}
