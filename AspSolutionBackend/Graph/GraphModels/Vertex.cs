using System;

namespace Graph.GraphModels
{
    public class Vertex<TVertex, TArc>
        // where TVertex : class where TArc : class
    {
        public Vertex(string id, TVertex vertexData, Vertex<TVertex, TArc>? nextVertex = null)
        {
            Id = id;
            VertexData = vertexData;
            this.Next = nextVertex;
        }

        public string Id { get; set; } = default!;
        public TVertex VertexData { get; set; } = default!;
        public bool Visited { get; set; }
        public Vertex<TVertex, TArc>? Next { get; set; }
        public Arc<TVertex, TArc>? First { get; set; }
    }
}