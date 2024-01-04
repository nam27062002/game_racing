using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int maxNumberPlay;
        [SerializeField] private string maxNumberErrorTitle;
        [SerializeField] private string maxNumberErrorDescription;
        public int MaxNumberPlay => maxNumberPlay;
        public string MaxNumberErrorTitle => maxNumberErrorTitle;
        public string MaxNumberErrorDescription => maxNumberErrorDescription;
    }
}