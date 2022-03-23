using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillarSpawner : MonoBehaviour
{
    public GameObject[] pillars;
    public GameObject player;
    private float ballPosition = -5f;
    private float pillarPosition = 0f;
    public float fadeOutTime;
    public Image ImageToAnimate;
    int pillarRandNum;
    void Update()
    {
        if (player.transform.position.y < ballPosition && GameManager.GMState.Equals(GameManager.GameState.GamePlay))
        {
            ballPosition -= 27.85f;
            pillarPosition -= 27.85f;
            addPipe(pillarPosition);
        }
    }
    void addPipe(float pYPos)
    {
        System.Random random = new System.Random();
        int randNum = random.Next(0, pillars.Length);
        pillarRandNum = randNum;
        Invoke("SetColor", 2f);
        Instantiate(pillars[randNum], new Vector3(0, pYPos, 0), Quaternion.identity);
    }
    public void SetColor()
    {
        if (pillarRandNum == 0)
        {
            ImageToAnimate.color = Color32.Lerp(new Color32(231, 204, 241, 255), new Color32(231, 204, 241, 255), Mathf.Min(1, fadeOutTime));
        }
        else if (pillarRandNum == 1)
        {
            ImageToAnimate.color = Color32.Lerp(new Color32(63, 68, 74, 255), new Color32(63, 68, 74, 255), Mathf.Min(1, fadeOutTime));
        }
        else if (pillarRandNum == 2)
        {
            ImageToAnimate.color = Color32.Lerp(new Color32(62, 167, 222, 255), new Color32(62, 167, 222, 255), Mathf.Min(1, fadeOutTime));
        }
        else if (pillarRandNum == 3)
        {
            ImageToAnimate.color = Color32.Lerp(new Color32(90, 100, 147, 255), new Color32(90, 100, 147, 255), Mathf.Min(1, fadeOutTime));
        }
        else if (pillarRandNum == 4)
        {
            ImageToAnimate.color = Color32.Lerp(new Color32(138, 210, 37, 255), new Color32(138, 210, 37, 255), Mathf.Min(1, fadeOutTime));
        }
        else
        {
            ImageToAnimate.color = Color32.Lerp(new Color32(241, 203, 117, 255), new Color32(241, 203, 117, 255), Mathf.Min(1, fadeOutTime));
        }
    }
}

