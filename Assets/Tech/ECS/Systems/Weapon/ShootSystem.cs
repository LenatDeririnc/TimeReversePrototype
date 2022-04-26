using System.Threading.Tasks;
using Addressables;
using Entitas;
using MonoBehsProviders;
using TimelineData;
using UnityEngine.InputSystem;

namespace ECS.Systems.Weapon
{
    public class ShootSystem : IInitializeSystem, ITearDownSystem
    {
        private const string BulletAssetName = "StandardBullet";
    
        private readonly Contexts _contexts;
        private AddressablesAssetLoader _loader;

        public ShootSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        private async Task CreateBulletTask(InputAction.CallbackContext callbackContext)
        {
            var bullet = await _loader.LoadComponent<Bullet>(BulletAssetName);
            var playerTransform = _contexts.game.playerCameraEntity.transform.Value;
            var bulletTransform = bullet.transform;
            
            bulletTransform.position = playerTransform.position + (playerTransform.forward * 1f);
            bulletTransform.rotation = playerTransform.rotation;
        }

        private void CreateBullet(InputAction.CallbackContext callbackContext)
        {
            CreateBulletTask(callbackContext);
        }
    
        public void Initialize()
        {
            _loader = new AddressablesAssetLoader();
            _contexts.input.inputEntity.inputSettings.Value.Game.Fire.started += CreateBullet;
        }

        public void TearDown()
        {
            _contexts.input.inputEntity.inputSettings.Value.Game.Fire.started -= CreateBullet;
        }
    }
}