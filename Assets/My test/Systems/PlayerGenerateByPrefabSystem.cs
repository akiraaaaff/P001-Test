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
    partial struct PlayerGenerateByPrefabSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerGeneratorByPrefab>();
        }
    
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
    
        }
    
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var generator = SystemAPI.GetSingleton<PlayerGeneratorByPrefab>();
            var cubes = CollectionHelper.CreateNativeArray<Entity>(generator.cubeCount, Allocator.Temp);
            state.EntityManager.Instantiate(generator.cubeEntityProtoType, cubes);
            int count = 0;
            foreach (var cube in cubes)
            {
                state.EntityManager.AddComponentData<PlayerMoveData>(cube, new PlayerMoveData
                {
                    moveSpeed = 3
                });
                var position = new float3((count - generator.cubeCount * 0.5f) * 1.2f, 0, 0);
                var transform = SystemAPI.GetAspectRW<TransformAspect>(cube);
                transform.LocalPosition = position;
                count++;
            }
    
            cubes.Dispose();
            // 此System只在启动时运行一次，所以在第一次更新后关闭它。
            state.Enabled = false;
        }
    }
}
