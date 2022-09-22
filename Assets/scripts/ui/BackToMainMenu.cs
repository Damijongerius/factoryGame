using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    public GameObject Manager;
    private void Start()
    {
        Button button = gameObject.GetComponent<Button>();

        button.onClick.AddListener(MainMenu);
    }

    private void MainMenu()
    {
        ProfileManager profileManager = Manager.GetComponent<ProfileManager>();
        profileManager.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
