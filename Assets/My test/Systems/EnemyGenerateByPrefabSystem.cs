using Lockstep.Math;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

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
            Random _r = new(500);
            var generator = SystemAPI.GetSingleton<EnemyGeneratorByPrefab>();
            var cubes = CollectionHelper.CreateNativeArray<Entity>(generator.cubeCount, Allocator.Temp);
            state.EntityManager.Instantiate(generator.cubeEntityProtoType, cubes);
            foreach (var cube in cubes)
            {
                var position = new LVector3(_r.NextInt(-20000, 20000), 0, _r.NextInt(-20000, 20000));
                state.EntityManager.AddComponentData<EnemyMoveData>(cube, new EnemyMoveData
                {
                    moveSpeed = new LFloat(true,200),
                    position = position,
                });
                var transform = SystemAPI.GetAspectRW<TransformAspect>(cube);
                transform.LocalPosition = position.ToVector3();
            }
    
            cubes.Dispose();
            // 此System只在启动时运行一次，所以在第一次更新后关闭它。
            state.Enabled = false;
        }
    }
}
