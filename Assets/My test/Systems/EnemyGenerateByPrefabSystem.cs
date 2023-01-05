using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace P001.GameView
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(CreateEntitiesByPrefabSystemGroup))]
    partial struct EnemyGenerateByPrefabSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyGeneratorByPrefab>();
        }
    
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
    
        }
    
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var random = Random.CreateFromIndex(10);
            var generator = SystemAPI.GetSingleton<EnemyGeneratorByPrefab>();
            var cubes = CollectionHelper.CreateNativeArray<Entity>(generator.cubeCount, Allocator.Temp);
            state.EntityManager.Instantiate(generator.cubeEntityProtoType, cubes);
            foreach (var cube in cubes)
            {
                var position = new float3(random.NextFloat(), 0, random.NextFloat());
                state.EntityManager.AddComponentData<EnemyMoveData>(cube, new EnemyMoveData
                {
                    moveSpeed = 2,
                    position = position,
                });
                var transform = SystemAPI.GetAspectRW<TransformAspect>(cube);
                transform.LocalPosition = position;
            }
    
            cubes.Dispose();
            // 此System只在启动时运行一次，所以在第一次更新后关闭它。
            state.Enabled = false;
        }
    }
}
