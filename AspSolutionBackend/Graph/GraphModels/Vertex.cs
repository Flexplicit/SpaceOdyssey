using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace Graph.GraphModels
{
    public class Vertex<TVertex, TArc> : FastPriorityQueueNode
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
        public List<Arc<TVertex, TArc>> AdjacentArcs { get; set; } = new();

        public int DistanceFromStartVertex { get; set; } = int.MaxValue;

        public Vertex<TVertex, TArc>? VPrev { get; set; }

        public int Info { get; set; }

        public Arc<TVertex, TArc>? ChosenArc { get; set; }

        public Arc<TVertex, TArc>? GetLatestArc() => AdjacentArcs.Count == 0 ? null : AdjacentArcs[^1];

        public Arc<TVertex, TArc>? GetFirstArc() => AdjacentArcs.Count == 0 ? null : AdjacentArcs[0];

        // public void SetAdjacentArcsInfoToInfinity() => AdjacentArcs.ForEach(arc => arc.Weight = int.MaxValue / 3);
        public Arc<TVertex, TArc>? FindMinWeightArcWhoseParentIsNotVisited()
        {
            if (AdjacentArcs.Count == 0) return null;
            var res = AdjacentArcs
                .Where(x => !x.Target!.Visited)
                .ToList();
            if (!res.Any()) return null;

            var min = res.Min(x => x.Weight);
            return AdjacentArcs.First(x => x.Weight == min);
        }
    }
}