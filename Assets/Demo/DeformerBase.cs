using Unity.Collections;
using UnityEngine;

namespace Demo
{
    public class DeformerBase : DeformerAbstract
    {
        protected NativeArray<float> Env;
        private float _envelope;
        protected NativeArray<Vector3> VertexIn;
        protected NativeArray<Vector3> VertexOut;

        public override void SetEnvelope(float envelope)
        {
            Env = new NativeArray<float>(1, Allocator.Persistent);
            Env[0] = _envelope;
            _envelope = envelope;
        }

        public override void SetInputVertices(Vector3[] inVertices)
        {
            VertexIn = new NativeArray<Vector3>(inVertices, Allocator.Persistent);
            VertexOut = new NativeArray<Vector3>(VertexIn.Length, Allocator.Persistent);
        }

        public override Vector3[] GetOutputVertices()
        {
            // ToDo : write unsafe method extension memcpy
            return VertexOut.ToArray();
        }

        public override void Execute()
        {
        }

        public override void Deallocate()
        {
            VertexIn.Dispose();
            VertexOut.Dispose();
            Env.Dispose();
        }
    }
}