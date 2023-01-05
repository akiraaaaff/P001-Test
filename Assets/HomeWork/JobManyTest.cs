using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Jobs;
using Unity.Burst;

[BurstCompile]
struct JobA : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;
    [ReadOnly] public float deltaTime;
    public void Execute(int i)
    {
        position[i] = position[i] + velocity[i] * deltaTime;
    }
}
[BurstCompile]
struct JobB : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;
    [ReadOnly] public float deltaTime;
    public void Execute(int i)
    {
        position[i] = position[i] + velocity[i] * deltaTime;
    }
}
[BurstCompile]
struct JobC : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;
    [ReadOnly] public float deltaTime;
    public void Execute(int i)
    {
        position[i] = position[i] + velocity[i] * deltaTime;
    }
}
[BurstCompile]
struct JobD : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;
    [ReadOnly] public float deltaTime;
    public void Execute(int i)
    {
        position[i] = position[i] + velocity[i] * deltaTime;
    }
}
[BurstCompile]
struct JobE : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;
    [ReadOnly] public float deltaTime;
    public void Execute(int i)
    {
        position[i] = position[i] + velocity[i] * deltaTime;
    }
}
[BurstCompile]
struct JobF : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;
    [ReadOnly] public float deltaTime;
    public void Execute(int i)
    {
        position[i] = position[i] + velocity[i] * deltaTime;
    }
}
[BurstCompile]
struct JobG : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;
    [ReadOnly] public float deltaTime;
    public void Execute(int i)
    {
        position[i] = position[i] + velocity[i] * deltaTime;
    }
}
[BurstCompile]
struct JobH : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;
    [ReadOnly] public float deltaTime;
    public void Execute(int i)
    {
        position[i] = position[i] + velocity[i] * deltaTime;
    }
}

public class JobManyTest : MonoBehaviour
{
    public NativeArray<Vector3> velocity;
    public NativeArray<Vector3> position;

    private void Start()
    {
        position = new NativeArray<Vector3>(500, Allocator.Persistent);
        velocity = new NativeArray<Vector3>(500, Allocator.Persistent);
        for (var i = 0; i < velocity.Length; i++)
            velocity[i] = new Vector3(0, 10, 0);
    }

    private void Update()
    {
        var a = new JobA()
        {
            deltaTime = Time.time,
            velocity = velocity,
            position = position,
        };
        var aHandle = a.Schedule(position.Length, 64);
        aHandle.Complete();

        var c = new JobC()
        {
            deltaTime = Time.time,
            velocity = velocity,
            position = position,
        };
        var cHandle = c.Schedule(position.Length, 64);
        cHandle.Complete();

        var b = new JobB()
        {
            deltaTime = Time.time,
            velocity = velocity,
            position = position,
        };
        var bHandle = b.Schedule(position.Length, 64, aHandle);
        bHandle.Complete();

        var d = new JobD()
        {
            deltaTime = Time.time,
            velocity = velocity,
            position = position,
        };
        var combined = JobHandle.CombineDependencies(bHandle, cHandle);
        var dHandle = d.Schedule(position.Length, 64, combined);
        dHandle.Complete();





        Debug.Log(position[0]);
    }

    private void OnDestroy()
    {
        position.Dispose();
        velocity.Dispose();
    }
}
