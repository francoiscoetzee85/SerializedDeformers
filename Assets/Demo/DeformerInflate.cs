using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Demo
{
    // Serialize the and schedule the deformer
    [CreateAssetMenu(fileName = "InflateDeformer", menuName = "InflateDeformer")]
    public class DeformerInflate : DeformerBase
    {
        public override void Execute()
        {
            var job = new JobFactory<int, InflateDeformerFactory>
            {
                FactoryJob = new InflateDeformerFactory
                {
                    Envelope = Env,
                    Input = VertexIn,
                    Output = VertexOut
                }
            };
            job.Run();


            // var job = new JobParallelForFactory<int, InflateDeformerFactory>
            // {
            //     FactoryJob = new InflateDeformerFactory
            //     {
            //         Envelope = Env,
            //         Input = VertexIn,
            //         Output = VertexOut
            //     }
            // };
            // var dpt = job.Schedule(VertexIn.Length, 64);
            // dpt.Complete();
        }
    }

    
    // Deformer logic implementing IFactory so it can be executed 
    public struct InflateDeformerFactory : IFactory<int>
    {
        [ReadOnly] public NativeArray<float> Envelope;
        [ReadOnly] public NativeArray<Vector3> Input;
        [WriteOnly] public NativeArray<Vector3> Output;

        public bool IsDone(int index)
        {
            return index >= Input.Length;
        }

        public void SetOutput(int srcIndex)
        {
            Output[srcIndex] = Input[srcIndex] * Envelope[0];
        }
    }
}