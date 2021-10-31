using System;
using System.Collections.Generic;
using System.Linq;
using Graph.GraphModels;
using Utils;

namespace Graph
{
    public class Graph<TVertex, TArc>
    {
        private readonly string _id;
        private Vertex<TVertex, TArc>? _first;
        private int _vertexCount;
        private int _arcCount;


        public Graph(string id, Vertex<TVertex, TArc>? first = null)
        {
            _id = id;
            _first = first;
        }

        public Vertex<TVertex, TArc> CreateVertex(string vertexId, TVertex data)
        {
            _vertexCount++;
            var res = new Vertex<TVertex, TArc>(vertexId, data, _first);

            _first = res;
            return res;
        }


        public void AddVertexToFirst(Vertex<TVertex, TArc> vertex)
        {
            if (_first == null)
            {
                _first = vertex;
                return;
            }

            vertex.Next = _first;
            _first = vertex;
        }


        private void DeleteVertexNext(Vertex<TVertex, TArc> vertexStart, Vertex<TVertex, TArc> vertexToDelete)
        {
            var curr = vertexStart;
            while (curr != null)
            {
                if (curr.Next?.Id == vertexToDelete.Id)
                {
                    curr.Next = vertexToDelete.Next;
                    return;
                }

                curr = curr.Next;
            }
        }
        // private bool SwapVertexPositions(Vertex<TVertex, TArc> v1, Vertex<TVertex, TArc> v2)
        // {
        //     var v1AdjacentNodes = GetVertexAdjacentNodes(v1);
        //     foreach (var vertex in v1AdjacentNodes)
        //     {
        //         
        //     }
        // }
        //
        // private List<Vertex<TVertex, TArc>> GetVertexAdjacentNodes(Vertex<TVertex, TArc> vertex)
        // {
        //     var current = _first;
        //     var nodes = new List<Vertex<TVertex, TArc>>();
        //     while (current != null)
        //     {
        //         if (current.First?.Target?.Id == vertex.Id)
        //         {
        //             nodes.Add(current);
        //         }
        //
        //         current = current.Next;
        //     }
        //
        //     return nodes;
        // }

        public Arc<TVertex, TArc> CreateArc(string vId, Vertex<TVertex, TArc> from, Vertex<TVertex, TArc> to,
            TArc? arcData)
        {
            var res = new Arc<TVertex, TArc>(vId, arcData, to);
            _arcCount++;
            res.Next = from.First;
            res.Target = to;
            from.First = res;
            return res;
        }

        private Vertex<TVertex, TArc>? GetVertexById(string id)
        {
            var current = this._first;
            while (current != null)
            {
                if (current.Id == id) return current;
                current = current.Next;
            }

            Console.WriteLine($"Current Graph {this._id}, does not contain given Vertex with id: {id}");
            return null;
        }

        /// <summary>
        /// Finds the shortest path between 2 nodes in a graph.
        /// 
        ///  Requirements:
        ///  1. Graph arc's must have positive weight.
        ///  2. Solutions is made for a DAMG(Directed Acyclic Multigraph), might work differently on others graph variations.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void DijkstraPath(Vertex<TVertex, TArc> from, Vertex<TVertex, TArc> to)
        {
            var originVertex = GetVertexById(@from.Id);
            if (originVertex != null)
            {
                AddVertexToFirst(originVertex);
            }
            const int infinity = int.MaxValue / 4; // Should be big enough
            var start = _first;
        }


        //TODO: Dijkstra might be a better solution
        public List<List<Arc<TVertex, TArc>>> GetArcDepthFirstSearch(Vertex<TVertex, TArc> from,
            Vertex<TVertex, TArc> to)
        {
            var originVertex = GetVertexById(@from.Id);
            if (originVertex != null)
            {
                AddVertexToFirst(originVertex);
            }

            if (_first == null) return new List<List<Arc<TVertex, TArc>>>();

            var vertexPathList = new List<Vertex<TVertex, TArc>>();
            var arcResultPath = new List<List<Arc<TVertex, TArc>>>();


            vertexPathList.Add(_first);

            DepthFirstSearchUtils(_first, to, vertexPathList, arcResultPath,
                new List<Arc<TVertex, TArc>>());
            return arcResultPath;
        }

        private void DepthFirstSearchUtils(
            Vertex<TVertex, TArc> from,
            Vertex<TVertex, TArc> to,
            List<Vertex<TVertex, TArc>> vertexPathList,
            List<List<Arc<TVertex, TArc>>> arcResultPath,
            List<Arc<TVertex, TArc>> currentArcs)
        {
            if (from.Id == to.Id)
            {
                var newList = currentArcs.Select(x => x).ToList();
                arcResultPath.Add(newList);
                return;
            }

            from.Visited = true; // current node

            var currentArc = from.First;

            while (currentArc != null)
            {
                if (!currentArc.Target!.Visited)
                {
                    vertexPathList.Add(currentArc.Target);
                    currentArcs.Add(currentArc);

                    DepthFirstSearchUtils(currentArc.Target, to, vertexPathList, arcResultPath, currentArcs);

                    vertexPathList.RemoveRangeWithoutSize(vertexPathList.IndexOf(to, 1));
                    currentArcs.RemoveRangeWithoutSize(currentArcs.IndexOf(currentArc, 1));
                }

                currentArc = currentArc.Next;
            }

            from.Visited = false;
        }
    }
}