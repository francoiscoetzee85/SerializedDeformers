using UnityEngine;

namespace Demo
{
    /*
    *  Hooks into serialized deformer and set execution based on Interface
    */

    //TODO: this should implement an interface

    public class DeformerManager : MonoBehaviour
    {
        private Mesh _deformingMesh;
        private Vector3[] _displacedVertices;
        private Vector3[] _originalVertices;
        public DeformerAbstract deformer;
        public float envelope;

        private void Start()
        {
            _deformingMesh = gameObject.GetComponent<MeshFilter>().mesh;
            _originalVertices = _deformingMesh.vertices;
            _displacedVertices = new Vector3[_originalVertices.Length];
            for (var i = 0; i < _originalVertices.Length; i++) _displacedVertices[i] = _originalVertices[i];
        }

        private void Update()
        {
            deformer.SetInputVertices(_originalVertices);
            deformer.SetEnvelope(envelope);
            deformer.Execute();
            _deformingMesh.vertices = deformer.GetOutputVertices();
            deformer.Deallocate();
        }

        private void OnDisable()
        {
            _deformingMesh.vertices = _originalVertices;
        }
    }
}