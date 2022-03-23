using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarRotator : MonoBehaviour
{
    public float rotationSpeed;

    void Start()
    {
        rotationSpeed = 30f;
    }
    private void FixedUpdate()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) && GameManager.GMState.Equals(GameManager.GameState.GamePlay))
        {
            Vector3 Rotation = -Input.GetTouch(0).deltaPosition;
            transform.Rotate(0, Rotation.x * rotationSpeed * Time.deltaTime, 0);
        }
    }
    private void Update()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && GameManager.GMState.Equals(GameManager.GameState.GamePlay))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * (rotationSpeed+175f) * Time.deltaTime, 0);
        }
    }
}
