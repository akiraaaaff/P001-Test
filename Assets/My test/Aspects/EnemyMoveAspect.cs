using Lockstep.Math;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace P001.GameView
{
    readonly partial struct EnemyMoveAspect : IAspect
    {
        readonly RefRW<LocalTransform> localTransform;
        readonly RefRW<EnemyMoveData> moveData;

        public void Move(LFloat fixedDeltaTime, LVector3 targetPos)
        {
            var direction = targetPos - moveData.ValueRO.position;
            moveData.ValueRW.position += direction * moveData.ValueRO.moveSpeed * fixedDeltaTime;
            localTransform.ValueRW.Position = moveData.ValueRO.position.ToVector3();
        }
    }
}
