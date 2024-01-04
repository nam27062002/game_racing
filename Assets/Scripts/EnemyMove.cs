using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    

    private void Update () {
        
        transform.Translate (new Vector3 (0, 1,0) * (speed * Time.deltaTime));
        if (transform.position.y < -6.5f || GameManager.Instance.gameStatus == GameManager.State.PrepareStartGame)
        {
            if (GameManager.Instance.gameStatus == GameManager.State.StartGame)
            {
                GameManager.Instance.IncreaseScore();
            }
            Destroy(gameObject);
        }
    }
}
