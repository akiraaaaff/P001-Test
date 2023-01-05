using Unity.Entities;
using UnityEngine;

namespace P001.GameView
{
    struct PlayerGeneratorByPrefab : IComponentData
    {
        public Entity cubeEntityProtoType;
        public int cubeCount;
    }
    
    class PlayerGeneratorByPrefabAuthoring : Singleton<PlayerGeneratorByPrefabAuthoring>
    {
        public GameObject cubePrefab = null;
        [Range(1, 10)] public int CubeCount = 6;

        class CubeBaker : Baker<PlayerGeneratorByPrefabAuthoring>
        {
            public override void Bake(PlayerGeneratorByPrefabAuthoring authoring)
            {
                AddComponent(new PlayerGeneratorByPrefab
                {
                    cubeEntityProtoType = GetEntity(authoring.cubePrefab),
                    cubeCount = authoring.CubeCount
                });
            }
        }
    }
}
