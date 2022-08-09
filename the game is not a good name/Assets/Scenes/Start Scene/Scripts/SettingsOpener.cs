using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsOpener : MonoBehaviour
{
  [SerializeField] private Button _settings;

  private void Start()
  {
     _settings = GetComponent<Button>();
     _settings.onClick.AddListener(()=>ChangeScene(name));
  }

  public void ChangeScene(string name)
   {
      SceneManager.LoadScene(name);
   }

   public void ExitScene()
   {
      Application.Quit();
   }
}
