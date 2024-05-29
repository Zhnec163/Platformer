using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string _nameSceneGame;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _aboutMenu;
    [SerializeField] private GameObject _settingsMenu;
    
    private void Awake()
    {
        _mainMenu.SetActive(true);
        _aboutMenu.SetActive(false);
    }
    
    public void Play()
    {
        SceneManager.LoadScene(_nameSceneGame);
    }
    
    public void OpenSettingsMenu()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }
    
    public void Back()
    {
        _mainMenu.SetActive(true);
        _aboutMenu.SetActive(false);
        _settingsMenu.SetActive(false);
    }
    
    public void OpenAboutMenu()
    {
        _mainMenu.SetActive(false);
        _aboutMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}