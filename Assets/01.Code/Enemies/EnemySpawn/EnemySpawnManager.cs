using System.Collections.Generic;
using UnityEngine;

namespace Code.Enemies.EnemySpawn
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemies;

        private Stack<GameObject> _enemies;
    }
}