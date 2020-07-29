using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Demo
{
    [CreateAssetMenu(fileName = "NoiseMultiThreadedDeformer", menuName = "NoiseMultiThreadedDeformer")]
    public class DeformerNoiseMultiThreaded : DeformerBase
    {
        public bool isMulti;
        public string notes;

        public override void Execute()
        {
            if (!isMulti)
            {
                var job = new JobFactory<int, DeformerNoiseFactoryMT>
                {
                    FactoryJob = new DeformerNoiseFactoryMT
                    {
                        Envelope = Env,
                        Input = VertexIn,
                        Output = VertexOut
                    }
                };
                job.Run();
            }
            else
            {
                var job = new JobParallelForFactory<int, DeformerNoiseFactoryMT>
                {
                    FactoryJob = new DeformerNoiseFactoryMT
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
    }

    
    // Deformer logic implementing IFactory so it can be executed 
    public struct DeformerNoiseFactoryMT : IFactory<int>
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