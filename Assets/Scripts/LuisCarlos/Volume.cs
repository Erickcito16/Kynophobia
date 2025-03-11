using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
   
  private AudioMixer master;
  [SerializeField] private Slider sfxAudio, musicAudio;
  private bool isMute;
  public string musicSavedValue = "musicValue";  // Clave para guardar el volumen de la música en PlayerPrefs
  public string sfxSavedValue = "sfxValue";

  void Start()
  {
    if (PlayerPrefs.HasKey("musicVolume"))
    {
      LoadSoundPreferences();
    }

    else
    {
      MusicVolumeControl();
      SFXVolumeControl();
    }

  }
  // Controla el volumen de la música
  public void MusicVolumeControl()
  {
    float volume = musicAudio.value;
    master.SetFloat("Music Volume", MathF.Log10(volume) * 20);

    PlayerPrefs.SetFloat("musicVolume", volume);
  }

  // Controla el volumen de los efectos de sonido
  public void SFXVolumeControl()
  {
    float volume = sfxAudio.value;
    master.SetFloat("SFX Volume", MathF.Log10(volume) * 20);

    PlayerPrefs.SetFloat("SFXVolume", volume);
  }

  // Silencia el sonido 
  public void MuteAll()
  {
    isMute = !isMute; // Alterna entre silencio y sonido
    if (isMute)
    {
      master.SetFloat("Master", -80f); // Silencia todo el audio
    }
    else
    {
      master.SetFloat("Master", 0f); // Restaura el volumen
    }
  }

  public void SaveSoundPreferences(float levelMusic, float levelSFX)
  {

    PlayerPrefs.SetFloat(musicSavedValue, levelMusic);
    PlayerPrefs.SetFloat(sfxSavedValue, levelSFX);
  }

  // Carga las preferencias de volumen guardadas
  public void LoadSoundPreferences()
  {
    musicAudio.value = PlayerPrefs.GetFloat("musicVolume");
    sfxAudio.value = PlayerPrefs.GetFloat("SFXVolume");

    MusicVolumeControl();
    SFXVolumeControl();
  }
}
