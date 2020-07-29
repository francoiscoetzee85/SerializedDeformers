using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Demo
{
    // Serialize the and schedule the deformer
    [CreateAssetMenu(fileName = "NoiseDeformer", menuName = "NoiseDeformer")]
    public class DeformerNoise : DeformerBase
    {
        public string notes;

        public override void Execute()
        {
            // var job = new JobFactory<int, DeformerNoiseFactory>
            // {
            //     FactoryJob = new DeformerNoiseFactory
            //     {
            //         Envelope = Env,
            //         Input = VertexIn,
            //         Output = VertexOut
            //     }
            // };
            // job.Run();


            var job = new JobParallelForFactory<int, DeformerNoiseFactory>
            {
                FactoryJob = new DeformerNoiseFactory
                {
                    Envelope = Env,
                    Input = VertexIn,
                    Output = VertexOut
                }
            };
            JobHandle dpt = job.Schedule(VertexOut.Length, 64);
            dpt.Complete();
        }
    }
    
    // Deformer logic implementing IFactory so it can be executed 
    public struct DeformerNoiseFactory : IFactory<int>
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
            Output[srcIndex] = Input[srcIndex] * math.sin(Envelope[0] + srcIndex);
        }
    }
}