using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float jumpSpeed;
    Rigidbody rb;
    public GameObject SplashParticle;
    public AudioClip gameOverAudio;
    public AudioClip splashAudio;
    public AudioClip scoreAudio;
    public AudioSource audio;
    public GameObject ScoreManager;
    public GameObject SplashImage;

    public Material DefaultBall, RedBall, PurpleBall,GreenBall, OrangeBall, YellowBall, BombBall, EyeBall, WoodenBall, SplitMetalBall, SpikeBall, WheelBall;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        jumpSpeed = 25;
        ParticleSystem ps = SplashParticle.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        if (PlayerPrefs.HasKey("BALL"))
        {
            switch (PlayerPrefs.GetString("BALL"))
            {
                case "RedBall":
                    BallAppearance(RedBall, new Color32(239, 0, 0, 255));
                    break;
                case "PurpleBall":
                    BallAppearance(PurpleBall, new Color32(157, 0, 242, 255));
                    break;
                case "GreenBall":
                    BallAppearance(GreenBall, new Color32(4, 170, 109, 255));
                    break;
                case "OrangeBall":
                    BallAppearance(OrangeBall, new Color32(235, 139, 79, 255));
                    break;
                case "YellowBall":
                    BallAppearance(YellowBall, new Color32(231, 221, 39, 255));
                    break;
                case "BombBall":
                    BallAppearance(BombBall, new Color32(15, 16, 19, 255));
                    break;
                case "EyeBall":
                    BallAppearance(EyeBall, new Color32(221, 243, 255, 255));
                    break;
                case "WoodenBall":
                    BallAppearance(WoodenBall, new Color32(139, 93, 55, 255));
                    break;
                case "SplitMetalBall":
                    BallAppearance(SplitMetalBall, new Color32(81, 161, 255, 255));
                    break;
                case "SpikeBall":
                    BallAppearance(SpikeBall, new Color32(121, 131, 147, 255));
                    break;
                case "WheelBall":
                    BallAppearance(WheelBall, new Color32(16, 16, 16, 255));
                    break;
                default:
                    BallAppearance(DefaultBall, new Color32(49, 97, 120, 255));
                    break;
            }
        }
    }
    public void BallAppearance(Material material,Color32 color)
    {
        ParticleSystem ps = SplashParticle.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        GetComponent<MeshRenderer>().material = material;
        GetComponent<TrailRenderer>().material.color = color;
        SplashImage.GetComponent<SpriteRenderer>().color = color;
        ma.startColor = (Color)color;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * (Time.deltaTime - 4));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            GameObject splash = Instantiate(SplashImage, transform.position, Quaternion.Euler(90f, 0f, 0f));
            splash.transform.parent = collision.gameObject.transform;

            if (GameManager.GMState.Equals(GameManager.GameState.GamePlay))
            {
                audio.PlayOneShot(splashAudio);
            }
            StartCoroutine(ScaleBall());
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            Instantiate(SplashParticle, transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            audio.PlayOneShot(gameOverAudio);
            PlayerPrefs.SetString("TryAgain", "0");
            ScoreManager.GetComponent<ScoreManager>().UpdateGold(ScoreManager.GetComponent<ScoreManager>().Score * 2);
            GameManager.GMState = GameManager.GameState.GameOver;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "empty")
        {
            audio.PlayOneShot(scoreAudio);
            ScoreManager.GetComponent<ScoreManager>().Score++;
            ScoreManager.GetComponent<ScoreManager>().UpdateScoreUI();
        }
    }
    public IEnumerator ScaleBall()
    {
        transform.localScale = new Vector3(0.3f, 0.2f, 0.3f);
        yield return new WaitForSeconds(0.15f);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
}
