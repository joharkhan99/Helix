using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState { GamePlay, GamePause, GameOver, GameHome, GameShop, GameSetting}
    public static GameState GMState;
    public GameObject HomeScreen;
    public GameObject GameOver;
    public GameObject GamePause;
    public GameObject GameSetting;
    public GameObject GameShop;
    bool isPaused = false;
    public Sprite Pause, Play;
    public float fadeOutTime;
    Color color1;
    Color color2;
    public Slider MusicSlider;
    public Slider SoundSlider;
    public AudioSource[] allSoundEffects;

    public Button RedSelectButton, PurpleSelectButton, GreenSelectButton, OrangeSelectButton, YellowSelectButton, BombSelectButton,
        EyeSelectButton, WoodenSelectButton, SplitMetalSelectButton, SpikeSelectButton, WheelSelectButton;
    public GameObject ErrorBanner;
    public Button SettingButton;

    private void Start()
    {
        fadeOutTime = 5.0f;
        color1 = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        color2 = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    void Update()
    {
        SettingButton.transform.Rotate(Vector3.forward * (Time.deltaTime-1));

        switch (GMState)
        {
            case GameState.GameHome:
                HomeScreen.SetActive(true);
                GameOver.SetActive(false);
                GamePause.SetActive(false);
                GameShop.SetActive(false);
                StartCoroutine(AnimateImageColor(HomeScreen.GetComponent<Image>()));
                break;
            case GameState.GameOver:
                GameOver.SetActive(true);
                GameShop.SetActive(false);
                HomeScreen.SetActive(false);
                GamePause.SetActive(false);
                this.GetComponent<AudioSource>().Stop();
                break;
            case GameState.GamePlay:
                GameOver.SetActive(false);
                HomeScreen.SetActive(false);
                GamePause.SetActive(false);
                GameShop.SetActive(false);
                this.GetComponent<AudioSource>().Stop();
                break;
            case GameState.GamePause:
                GameOver.SetActive(false);
                HomeScreen.SetActive(false);
                GameShop.SetActive(false);
                GamePause.SetActive(true);
                break;
            case GameState.GameSetting:
                GameOver.SetActive(false);
                HomeScreen.SetActive(false);
                GamePause.SetActive(false);
                GameShop.SetActive(false);
                GameSetting.SetActive(true);
                StartCoroutine(AnimateImageColor(GameSetting.GetComponent<Image>()));
                break;
            case GameState.GameShop:
                GameOver.SetActive(false);
                HomeScreen.SetActive(false);
                GamePause.SetActive(false);
                GameSetting.SetActive(false);
                GameShop.SetActive(true);
                StartCoroutine(AnimateImageColor(GameShop.GetComponent<Image>()));
                break;
        }


        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SoundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        foreach (var AudioSource in allSoundEffects)
        {
            AudioSource.volume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        }

        if (PlayerPrefs.HasKey("RedButton"))
        {
            RedSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("PurpleButton"))
        {
            PurpleSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("GreenButton"))
        {
            GreenSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("OrangeButton"))
        {
            OrangeSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("YellowButton"))
        {
            YellowSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("BombButton"))
        {
            BombSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("EyeButton"))
        {
            EyeSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("WoodenButton"))
        {
            WoodenSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("SplitMetalButton"))
        {
            SplitMetalSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("SpikeButton"))
        {
            SpikeSelectButton.gameObject.SetActive(true);
        }
        if (PlayerPrefs.HasKey("WheelButton"))
        {
            WheelSelectButton.gameObject.SetActive(true);
        }
    }
    public void StartGame()
    {
        PlayerPrefs.SetString("TryAgain", "0");
        GMState = GameState.GamePlay;
    }
    public void ExitGame()
    {
        PlayerPrefs.SetString("TryAgain", "0");
        Application.Quit();
    }
    public void ShowHomeScreen()
    {
        GMState = GameState.GameHome;
        PlayerPrefs.SetString("TryAgain", "0");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowSettingScreen()
    {
        GMState = GameState.GameSetting;
    }
    public void ShowShopScreen()
    {
        GMState = GameState.GameShop;
    }
    public void TryAgain()
    {
        PlayerPrefs.SetString("TryAgain", "1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.GetString("TryAgain").Equals("1"))
        {
            GMState = GameState.GamePlay;
        }
        else
        {
            PlayerPrefs.SetString("TryAgain", "0");
            GMState = GameState.GameHome;
        }
    }
    public void PauseGame()
    {
        GameObject button = GameObject.Find("PauseButton");
        GMState = GameState.GamePause;
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            GMState = GameState.GamePlay;
            button.transform.GetComponent<UnityEngine.UI.Image>().sprite = Pause;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            button.transform.GetComponent<UnityEngine.UI.Image>().sprite = Play;
        }
    }
    public IEnumerator AnimateImageColor(Image ImageToAnimate)
    {
        while (this.enabled)
        {
            for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
            {
                ImageToAnimate.color = Color.Lerp(color1, color2, Mathf.Min(1, t / fadeOutTime));
                yield return null;
            }

            for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
            {
                ImageToAnimate.color = Color.Lerp(color2, color1, Mathf.Min(1, t / fadeOutTime));
                yield return null;
            }
        }
        yield return null;
    }
    public void MusicVolumeSlider()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        this.GetComponent<AudioSource>().volume= MusicSlider.value;
    }
    public void SoundVolumeSlider()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundSlider.value);
        foreach (var AudioSource in allSoundEffects)
        {
            AudioSource.volume = SoundSlider.value;
        }
    }

    public void CLIKKED(Button button)
    {
        int price = int.Parse(button.GetComponentInChildren<Text>().text.ToString());
        int UserGold = PlayerPrefs.GetInt("GOLD", 0);

        if (UserGold < price)
        {
            StartCoroutine(RemoveAfterSeconds(2, ErrorBanner, "Not Enough Gold Coins"));
        }
        else
        {
            PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") - price);
            PlayerPrefs.SetString(button.name.ToString(), "YES");
        }
    }
    public void SELECTBALL(string name)
    {
        PlayerPrefs.SetString("BALL", name);
        StartCoroutine(RemoveAfterSeconds(2, ErrorBanner, "Ball Selected"));
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj, string text)
    {
        ErrorBanner.GetComponentInChildren<Text>().text = text;
        obj.SetActive(true);
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

}
