using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float carSpeed;
    private Vector3 _position;
    public FixedJoystick fixedJoystick;
    private void Start()
    {
        _position = transform.position;
    }
    private void Update()
    {
        HandleEditorInput();
        ClampPosition();
    }
    private void HandleEditorInput()
    {
        _position.x += fixedJoystick.Horizontal * carSpeed * Time.deltaTime;
    }
    private void ClampPosition()
    {
        _position.x = Mathf.Clamp(_position.x, -GameManager.Range, GameManager.Range);
        transform.position = _position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            GameManager.Instance.gameStatus = GameManager.State.Lose;
            GameManager.Instance.endGame.SetActive(true);
            GameManager.Instance.SetEndGameScore();
            GameManager.Instance.EndGameHandle();
            Destroy(gameObject);
        }
    }
}
