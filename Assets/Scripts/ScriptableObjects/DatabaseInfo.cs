using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class DatabaseInfo : ScriptableObject
    {
        [SerializeField] private string host;
        [SerializeField] private string database;
        [SerializeField] private string user;
        [SerializeField] private string password;
        [SerializeField] private int port;
        public string ConnectionString => $"Server={host};Database={database};User ID={user};Password={password};Port={port};";
    }
}