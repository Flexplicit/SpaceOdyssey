using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Graph.GraphModels;
using Priority_Queue;
using Utils;
using static System.Int32;

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
        //         if (current.AdjacentArcs?.Target?.Id == vertex.Id)
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
            TArc? arcData, long? arcWeight)
        {
            var res = new Arc<TVertex, TArc>(vId, arcData, to, arcWeight);
            _arcCount++;
            res.Next = from.GetLatestArc();
            res.Target = to;
            from.AdjacentArcs.Add(res);
            return res;
        }

        private int[,] CreateAdjMatrix()
        {
            if (_first == null)
            {
                throw new Exception("Cannot create AdjMatrix on an empty graph");
            }

            var info = 0;
            var v = _first;
            while (v != null)
            {
                v.Info = info++;
                v = v.Next!;
            }

            int[,] res = new int[info, info];
            v = _first;
            while (v != null)
            {
                var i = v.Info;
                using var a = v.AdjacentArcs.GetEnumerator();
                while (a.MoveNext())
                {
                    var j = a.Current.Target!.Info;
                    res[i, j]++;
                }

                v = v.Next;
            }

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

        private static void DijkstraInitialization(IEnumerable<Vertex<TVertex, TArc>> vertices,
            Vertex<TVertex, TArc> start)
        {
            foreach (var v in vertices)
            {
                v.DistanceFromStartVertex = MaxValue / 2;
                v.ChosenArc = null;
                v.VPrev = null;
            }

            start.DistanceFromStartVertex = 0;
        }

        private static void DijkstraArcRelaxation(Vertex<TVertex, TArc> u, Vertex<TVertex, TArc> v,
            Arc<TVertex, TArc> arc)
        {
            // u->v 
            if (v.DistanceFromStartVertex > u.DistanceFromStartVertex + arc.Weight)
            {
                v.DistanceFromStartVertex = u.DistanceFromStartVertex + (int)arc.Weight;
                v.VPrev = u;
                v.ChosenArc = arc;
                arc.VPrev = u;
            }
        }

        //O(kn^3)
        // limit should be as LOW AS POSSIBLE!
        public List<List<Arc<TVertex, TArc>>> YensKShortestPathFinder(Vertex<TVertex, TArc> from,
            Vertex<TVertex, TArc> to, int limit = 5)
        {
            if (_first == null || limit < 1)
            {
                throw new Exception("An empty graph or negative limit occured");
            }

            var permanentShortestPaths = new List<List<Arc<TVertex, TArc>>>(); // A list
            var shortestPath1 = DijkstraPath(from, to);
            permanentShortestPaths.Add(shortestPath1);
            var candidatePaths = new List<List<Arc<TVertex, TArc>>>(); // B list


            for (var k = 1; k < limit; k++)
            {
                var latestPath = permanentShortestPaths[k - 1];
                // We substitute each arc with a newer one and then compare the results and pick the best one
                foreach (var arc in latestPath)
                {
                    // we temporary remove chosen arc then run dijkstra again
                    arc.IsConnectedToGraph = false;
                    var candidateRes = DijkstraPath(from, to);

                    if (candidateRes.Count < 0) continue;

                    // We restore removed arcs and add result to temporary path list.
                    arc.IsConnectedToGraph = true;
                    candidatePaths.Add(candidateRes);
                }

                // We compare and pick the minWeighed path
                var candidatePathMinWeightIndex = GetMinIndexValue(candidatePaths);
                permanentShortestPaths.Add(candidatePaths[candidatePathMinWeightIndex]);
                candidatePaths.RemoveRange(0, candidatePaths.Count);
            }

            return permanentShortestPaths;
        }

        private static int GetMinIndexValue(IEnumerable<List<Arc<TVertex, TArc>>> candidatePaths)
        {
            var minIndex = 0;
            long minWeight = 0;
            foreach (var minCurrentPath in candidatePaths.Select(path => path.Min(x => x.Weight)))
            {
                if (minIndex == 0)
                {
                    minWeight = minCurrentPath;
                    minIndex++;
                    continue;
                }

                minWeight = minCurrentPath < minWeight ? minCurrentPath : minWeight;
                minIndex++;
            }

            return minIndex - 1;
        }


        /// <summary>
        /// Finds the shortest path between 2 nodes in a graph.
        /// 
        ///  Requirements:
        ///  1. Graph arc's must have positive weight.
        ///  2. Solutions is made for a DAMG(Directed Acyclic Multigraph), might work differently on others graph variations.
        ///
        ///  * Complexity O(n^2), where n = |V|
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        ///
        public List<Arc<TVertex, TArc>> DijkstraPath(Vertex<TVertex, TArc> from, Vertex<TVertex, TArc> to)
        {
            var vertices = GetAllVertices();
            DijkstraInitialization(vertices, from);

            // DijkstraInitialization(vertices, _first);
            var result = new List<Vertex<TVertex, TArc>>();

            var pq = new FastPriorityQueue<Vertex<TVertex, TArc>>(_vertexCount);
            foreach (var ver in vertices)
            {
                pq.Enqueue(ver, ver.DistanceFromStartVertex);
            }

            while (pq.Count > 0)
            {
                var uVertex = pq.Dequeue();
                result.Add(uVertex);
                if (uVertex.AdjacentArcs.Count > 0)
                {
                    using var uArcs = uVertex.AdjacentArcs.OrderBy(arc => arc.Weight).GetEnumerator();
                    while (uArcs.MoveNext())
                    {
                        var curr = uArcs.Current;
                        if (!curr.IsConnectedToGraph) continue;

                        var vVertex = curr.Target!;
                        DijkstraArcRelaxation(uVertex, vVertex, curr);
                        pq.UpdatePriority(vVertex, vVertex.DistanceFromStartVertex);
                    }
                }
            }

            return extractPathsFromResult(from, to);
        }

        private List<Arc<TVertex, TArc>> extractPathsFromResult(Vertex<TVertex, TArc> @from, Vertex<TVertex, TArc> to)
        {
            var arcs = new List<Arc<TVertex, TArc>>();

            var current = to;
            while (current != null)
            {
                if (current.VPrev?.ChosenArc == null) break;
                arcs.Add(current.VPrev!.ChosenArc!);
                current = current.VPrev;
            }

            arcs.Reverse();
            return arcs;
        }


// public void DijkstraPath(Vertex<TVertex, TArc> from, Vertex<TVertex, TArc> to)
// {
//     var res = CreateAdjMatrix();
//
//
//     var originVertex = GetVertexById(@from.Id);
//     if (originVertex != null)
//     {
//         AddVertexToFirst(originVertex);
//     }
//
//     if (_first == null) return;
//
//     var vertices = GetAllVertices();
//     var visited = new Vertex<TVertex, TArc>[vertices.Count];
//     var unVisited = vertices.ToArray();
//
//     DijkstraPreparation(vertices);
//     var startNode = _first;
//
//     var curr = 0;
//
//
//     for (var i = 0; i < unVisited.Length; i++)
//     {
//         var current = unVisited[i];
//         using var adjacentArcs = current.AdjacentArcs.GetEnumerator();
//
//         var fastestArc = adjacentArcs.Current;
//         while (adjacentArcs.MoveNext())
//         {
//             var currentArc = adjacentArcs.Current;
//             if (currentArc.Weight < fastestArc.Weight)
//             {
//                 fastestArc = currentArc;
//                 fastestArc.Target!.VPrev = current;
//             }
//         }
//     }
//
//
//     var pq = new FastPriorityQueue<Vertex<TVertex, TArc>>(_vertexCount);
//
//     // Add initial node to queue
//     pq.Enqueue(startNode, 0);
// }


        private void DijkstraPreparation(List<Vertex<TVertex, TArc>> vertices)
        {
            InjectVerticesWithInfiniteData(vertices);
            _first!.DistanceFromStartVertex = 0;
        }


        private static void InjectVerticesWithInfiniteData(List<Vertex<TVertex, TArc>> vertices)
            => vertices.ForEach(vertex => vertex.DistanceFromStartVertex = MaxValue / 2);


        private List<Vertex<TVertex, TArc>> GetAllVertices()
        {
            var vertices = new List<Vertex<TVertex, TArc>>(_vertexCount);
            var curr = _first;
            while (curr != null)
            {
                vertices.Add(curr);
                curr = curr.Next;
            }

            return vertices;
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

            var currentArc = from.GetLatestArc();

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