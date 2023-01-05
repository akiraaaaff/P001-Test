using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace P001.GameView
{
    struct EnemyMoveData : IComponentData
    {
        public float moveSpeed;
        public float3 position;
    }
    public class EnemyMoveDataAuthoring : MonoBehaviour
    {
        [Range(0, 10)]public float moveSpeed = 1.0f;
        public class Baker : Baker<EnemyMoveDataAuthoring>
        {
            public override void Bake(EnemyMoveDataAuthoring authoring)
            {
                var data = new EnemyMoveData
                {
                    moveSpeed = authoring.moveSpeed
                };
                AddComponent(data);
            }
        }
    }
}
