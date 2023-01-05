using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace P001.GameView
{
    readonly partial struct PlayerMoveAspect : IAspect
    {
        readonly RefRW<LocalTransform> localTransform;
        readonly RefRW<PlayerMoveData> moveData;

        public void Move(float fixedDeltaTime, float2 variableJoystick)
        {
            var direction = new float3(0, 0, 1) * variableJoystick.y + new float3(1, 0, 0) * variableJoystick.x;
            moveData.ValueRW.position += direction * moveData.ValueRO.moveSpeed * fixedDeltaTime;
            localTransform.ValueRW.Position = moveData.ValueRO.position;
        }
    }
}
