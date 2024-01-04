using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int maxNumberPlay;
        public int MaxNumberPlay => maxNumberPlay;
    }
}