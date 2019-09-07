using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject credits;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    private void Update()
    {
        if (credits.activeSelf && Input.anyKeyDown) credits.SetActive(false);
    }

}
