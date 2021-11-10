using System;
using System.Collections.Generic;
using Graph.GraphModels;

namespace Graph
{
    public abstract class AbstractGraph<TVertex, TArc>
    {
        protected readonly string Id;
        protected Vertex<TVertex, TArc>? First;
        protected int VertexCount;
        protected int ArcCount;

        protected AbstractGraph(string id)
        {
            Id = id;
        }

        public Vertex<TVertex, TArc> CreateVertex(string vertexId, TVertex data)
        {
            VertexCount++;
            var res = new Vertex<TVertex, TArc>(vertexId, data, First);

            First = res;
            return res;
        }

        public Arc<TVertex, TArc> CreateArc(string vId, Vertex<TVertex, TArc> from, Vertex<TVertex, TArc> to,
            TArc? arcData, long? arcWeight)
        {
            var res = new Arc<TVertex, TArc>(vId, arcData, to, arcWeight);
            ArcCount++;
            res.Next = from.GetLatestArc();
            res.Target = to;
            from.AdjacentArcs.Add(res);
            return res;
        }

        protected Vertex<TVertex, TArc>? GetVertexById(string id)
        {
            var current = First;
            while (current != null)
            {
                if (current.Id == id) return current;
                current = current.Next;
            }

            Console.WriteLine($"Current Graph {this.Id}, does not contain given Vertex with id: {id}");
            return null;
        }

        protected List<Vertex<TVertex, TArc>> GetAllVertices()
        {
            var vertices = new List<Vertex<TVertex, TArc>>(VertexCount);
            var curr = First;
            while (curr != null)
            {
                vertices.Add(curr);
                curr = curr.Next;
            }

            return vertices;
        }

        protected void AddVertexToFirst(Vertex<TVertex, TArc> vertexToAdd)
        {
            if (First == null)
            {
                First = vertexToAdd;
                return;
            }

            var current = First;
            while (current != null)
            {
                if (current.Next == vertexToAdd)
                {
                    var tempFirstNextNodes = First.Next;
                    var tempVertexToAddNext = vertexToAdd.Next;
                    current.Next = First;
                    vertexToAdd.Next = tempFirstNextNodes;
                    First.Next = tempVertexToAddNext;
                    First = vertexToAdd;
                    return;
                }

                current = current.Next;
            }
        }
    }
}