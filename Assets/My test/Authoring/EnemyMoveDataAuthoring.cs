using Lockstep.Math;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace P001.GameView
{
    struct EnemyMoveData : IComponentData
    {
        public LFloat moveSpeed;
        public LVector3 position;
    }
    public class EnemyMoveDataAuthoring : MonoBehaviour
    {
        [Range(0, 10)]public LFloat moveSpeed = new(true, 1000);
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
