using Unity.Burst;
using Unity.Jobs;

namespace Demo
{
    /*
     *  Low level generic execution templates.
     */

    // Execution Interfaces
    public interface IFactory<T>
    {
        bool IsDone(int index);
        void SetOutput(int index);
    }

    // Main thread
    [BurstCompile]
    public struct JobFactory<T, TFactory> : IJob
        where T : struct
        where TFactory : IFactory<T>
    {
        public TFactory FactoryJob;

        public void Execute()
        {
            for (var i = 0; !FactoryJob.IsDone(i); ++i) FactoryJob.SetOutput(i);
        }
    }

    // Multi thread 
    [BurstCompile]
    public struct JobParallelForFactory<T, TFactory> : IJobParallelFor
        where T : struct
        where TFactory : IFactory<T>
    {
        public TFactory FactoryJob;

        public void Execute(int index)
        {
            FactoryJob.SetOutput(index);
        }
    }
}