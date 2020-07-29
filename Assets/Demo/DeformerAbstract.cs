using UnityEngine;

namespace Demo
{
    public abstract class DeformerAbstract : ScriptableObject, IDeformerJob
    {
        public abstract void SetEnvelope(float envelope);

        public abstract void SetInputVertices(Vector3[] inVertices);

        public abstract Vector3[] GetOutputVertices();

        public abstract void Execute();

        public abstract void Deallocate();
    }

    public interface IDeformerJob
    {
        void SetEnvelope(float envelope);
        void SetInputVertices(Vector3[] inVertices);
        Vector3[] GetOutputVertices();
        void Execute();
        void Deallocate();
    }
}