using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private float delayTime;

    private float _timer;
    private int _selectedCarIndex;

    private void Update()
    {
        if (GameManager.Instance.gameStatus == GameManager.State.PrepareStartGame) return;
        UpdateTimer();
        TrySpawnCar();
    }

    private void UpdateTimer()
    {
        _timer -= Time.deltaTime;
    }

    private void TrySpawnCar()
    {
        if (!(_timer <= 0f)) return;
        SpawnCar();
        ResetTimer();
    }

    private void SpawnCar()
    {
        var spawnPosition = CalculateSpawnPosition();
        _selectedCarIndex = Random.Range(0, cars.Length);
        Instantiate(cars[_selectedCarIndex], spawnPosition, Quaternion.Euler(0f,0f,180f), transform);
    }

    private Vector3 CalculateSpawnPosition()
    {
        var spawnX = Random.Range(-GameManager.Range, GameManager.Range);
        return new Vector3(spawnX, transform.position.y, 0);
    }

    private void ResetTimer()
    {
        _timer = delayTime;
    }
}