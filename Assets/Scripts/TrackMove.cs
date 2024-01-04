using UnityEngine;

public class TrackMove : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Update()
    {
        UpdateTextureOffset();
    }

    private void UpdateTextureOffset()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time * speed);
    }
    
}