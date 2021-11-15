using System;
using System.Collections.Generic;
using System.Linq;
using Graph.GraphModels;
using Priority_Queue;
using static System.Int32;

namespace Graph
{
    public class Graph<TVertex, TArc> : AbstractGraph<TVertex, TArc>
    {
        public Graph(string id) : base(id)
        {
        }


        private void DijkstraInitialization(IEnumerable<Vertex<TVertex, TArc>> vertices,
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

        // limit should be as LOW AS POSSIBLE!
        public List<List<Arc<TVertex, TArc>>> YensKShortestPathFinder(Vertex<TVertex, TArc> from,
            Vertex<TVertex, TArc> to, DateTime start, int limit = 5)
        {
            if (First == null || limit < 1)
            {
                throw new Exception("An empty graph or negative limit occured");
            }

            var permanentShortestPaths = new List<List<Arc<TVertex, TArc>>>();
            var shortestPath1 = CustomDijkstraPathFinder(from, to, start);

            if (shortestPath1.Count == 0) return permanentShortestPaths;


            permanentShortestPaths.Add(shortestPath1);
            
            var candidatePaths = new List<List<Arc<TVertex, TArc>>>();


            for (var k = 1; k < limit; k++)
            {
                var latestPath = permanentShortestPaths[k - 1];
                // We substitute each arc with a newer one and then compare the results and pick the best one
                foreach (var arc in latestPath)
                {
                    // we temporary remove chosen arc then run dijkstra again
                    arc.IsConnectedToGraph = false;
                    var candidateRes = CustomDijkstraPathFinder(from, to, start);

                    if (candidateRes.Count < 0) continue;

                    // We restore removed arcs and add result to temporary path list.
                    arc.IsConnectedToGraph = true;
                    if (candidateRes.Count > 0) candidatePaths.Add(candidateRes);
                }

                // We compare and pick the minWeighed path
                if (candidatePaths.Count == 0) continue;
                var candidatePathMinWeightIndex = GetMinWeightSumIndex(candidatePaths);
                permanentShortestPaths.Add(candidatePaths[candidatePathMinWeightIndex]);
                candidatePaths.RemoveRange(0, candidatePaths.Count);
            }

            return permanentShortestPaths;
        }

        private static int GetMinWeightSumIndex(IEnumerable<List<Arc<TVertex, TArc>>> candidatePaths)
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
        /// Finds the shortest path between 2 nodes in a graph. Works in a Directed cyclic multigraph with time constraints
        /// </summary>
        /// <param name="from">From vertex which will be added as the first vertex.</param>
        /// <param name="to">To vertex where we will be traveling to.</param>
        /// <param name="start">Starting time from where we search for future paths</param>
        public List<Arc<TVertex, TArc>> CustomDijkstraPathFinder(Vertex<TVertex, TArc> from, Vertex<TVertex, TArc> to,
            DateTime start)
        {
            var vertices = GetAllVertices();
            DijkstraInitialization(vertices, from);

            var pq = new FastPriorityQueue<Vertex<TVertex, TArc>>(VertexCount);
            foreach (var ver in vertices)
            {
                pq.Enqueue(ver, ver.DistanceFromStartVertex);
            }

            var currentDate = start;
            while (pq.Count > 0)
            {
                var uVertex = pq.Dequeue();
                if (uVertex.Id == to.Id) continue;
                if (uVertex.AdjacentArcs.Count > 0)
                {
                    var pathExists = false;

                    using var uArcs = uVertex.AdjacentArcs.OrderBy(arc => arc.Weight).GetEnumerator();
                    {
                        while (uArcs.MoveNext())
                        {
                            var curr = uArcs.Current;
                            if (curr.VPrev != null && uVertex.Id == curr.VPrev.Id) continue;
                            if (!curr.IsConnectedToGraph || curr.ArcStart!.Value < currentDate || curr.Checked)
                                continue;

                            var vVertex = curr.Target!;
                            DijkstraArcRelaxation(uVertex, vVertex, curr);
                            pq.UpdatePriority(vVertex, vVertex.DistanceFromStartVertex);
                            pathExists = true;

                            // We save older Date so we can move back later if necessary
                            curr.DatePreviousEnd = currentDate;
                        }
                    }
                    if (pq.Count > 0 && pq.First.ChosenArc != null)
                    {
                        currentDate = pq.First.ChosenArc!.ArcEnd!.Value;
                    }

                    if (!pathExists && from.Id == uVertex.Id)
                    {
                        continue;
                    }

                    if (to.ChosenArc != null)
                    {
                        break;
                    }

                    if (!pathExists)
                    {
                        if (uVertex.ChosenArc == null)
                        {
                            break;
                        }

                        //handle cycles
                        if (uVertex.Id == uVertex.ChosenArc.VPrev!.Id) break;

                        // reverse 2 nodes back into the queue because we cannot travel further due to time constraint
                        if (from.Id == uVertex.Id) continue;
                        uVertex.DistanceFromStartVertex = int.MaxValue / 2;
                        uVertex.ChosenArc!.Checked = true;

                        pq.Enqueue(uVertex, uVertex.DistanceFromStartVertex);
                        pq.Enqueue(uVertex.VPrev!, uVertex.VPrev!.DistanceFromStartVertex);
                        currentDate = uVertex.ChosenArc.DatePreviousEnd;
                    }
                }
            }

            return ExtractPathsFromResult(from, to);
        }

        private static List<Arc<TVertex, TArc>> ExtractPathsFromResult(Vertex<TVertex, TArc> @from,
            Vertex<TVertex, TArc> to)
        {
            var arcs = new List<Arc<TVertex, TArc>>();

            var current = to;
            while (current is { VPrev: { } })
            {
                if (current.ChosenArc == null) break;
                arcs.Add(current.ChosenArc!);
                current = current.VPrev;
            }

            arcs.Reverse();
            return arcs;
        }
    }
}