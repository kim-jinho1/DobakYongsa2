using Code.Entities;
using Code.Players;
using GondrLib.Dependencies;
using UnityEngine;

namespace Code.Managers
{
    [DefaultExecutionOrder(-1)]
    public class PlayerManager : MonoBehaviour
    {
        [Inject, SerializeField] private Player player;
        [SerializeField] private EntityFinderSO playerFinder;

        private void Awake()
        {
            playerFinder.SetTarget(player);
        }
    }
}