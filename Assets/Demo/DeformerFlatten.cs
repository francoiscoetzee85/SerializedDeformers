using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Demo.ExampleDeformer
{
    // Serialize the and schedule the deformer
    [CreateAssetMenu(fileName = "FlattenDeformer", menuName = "FlattenDeformer")]
    public class DeformerFlatten : DeformerBase
    {
        public string notes;

        public override void Execute()
        {
            var job = new JobFactory<int, DeformerFlattenFactory>
            {
                FactoryJob = new DeformerFlattenFactory
                {
                    Envelope = Env,
                    Input = VertexIn,
                    Output = VertexOut
                }
            };
            job.Run();


            // var job = new JobParallelForFactory<int, DeformerFlattenFactory>
            // {
            //     FactoryJob = new DeformerFlattenFactory
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
    public struct DeformerFlattenFactory : IFactory<int>
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
            if (Input[srcIndex].y < Envelope[0])
                Output[srcIndex] = new Vector3(Input[srcIndex].x, 0, Input[srcIndex].z);
            else
                Output[srcIndex] = Input[srcIndex];
        }
    }
}