using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct VoxelDistanceJob : IJobParallelFor
{
    public Vector3 point;
    public float radius;

    [ReadOnly] public NativeArray<Matrix4x4> inMatrices;

    [WriteOnly]
    public NativeList<int>.ParallelWriter hitList;

    public void Execute(int index)
    {
        Vector3 pos = inMatrices[index].GetPosition();
        if (Vector3.Distance(pos, point) <= radius)
        {
            hitList.AddNoResize(index);
        }
    }
}
