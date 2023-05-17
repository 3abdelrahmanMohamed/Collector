using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class AllSceneManager : MonoBehaviour
{
    public void OpenLogin()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenHome()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenUser()
    {
        SceneManager.LoadScene(2);
    }
}
