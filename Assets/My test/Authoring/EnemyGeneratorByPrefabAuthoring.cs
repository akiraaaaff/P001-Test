using Unity.Entities;
using UnityEngine;

namespace P001.GameView
{
    struct EnemyGeneratorByPrefab : IComponentData
    {
        public Entity cubeEntityProtoType;
        public int cubeCount;
    }
    
    class EnemyGeneratorByPrefabAuthoring : Singleton<EnemyGeneratorByPrefabAuthoring>
    {
        public GameObject cubePrefab = null;
        [Range(1, 10)] public int CubeCount = 6;

        class CubeBaker : Baker<EnemyGeneratorByPrefabAuthoring>
        {
            public override void Bake(EnemyGeneratorByPrefabAuthoring authoring)
            {
                AddComponent(new EnemyGeneratorByPrefab
                {
                    cubeEntityProtoType = GetEntity(authoring.cubePrefab),
                    cubeCount = authoring.CubeCount
                });
            }
        }
    }
}
