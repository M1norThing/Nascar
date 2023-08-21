using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text energyText;
    [SerializeField] int maxEnergy;
    [SerializeField] int energyRechargeDuration;
    [SerializeField] Button playButton;
    [SerializeField] AndroidNotifications androidNotifications;

    const string EnergyKey = "Energy";
    const string EnergyReadyKey = "EnergyReady";

    int energy;

    private void Start()
    {
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) return;
        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.highScoreKey, 0);
        highScoreText.text = $"High Score: {highScore}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            if (energyReadyString == string.Empty) return;

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyREchargedInGame),(energyReady - DateTime.Now).Seconds);
            }
        }
        energyText.text = $"Play {(energy)}";
    }

    void EnergyREchargedInGame()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Play {(energy)}";
    }

    public void Play()
    {
        if (energy < 1) return;
        energy--;

        PlayerPrefs.SetInt(EnergyKey, energy);
        if (energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey,energyReady.ToString());

#if UNITY_ANDROID
            androidNotifications.ScheduleNotification(energyReady);
#endif

        }
        SceneManager.LoadScene(1);
    }
}
