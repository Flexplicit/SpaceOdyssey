using System.Collections.Generic;

namespace Graph.GraphModels
{
    public class PathData<TVertex, TArc>
    {
        public PathData()
        {
        }

        public int TotalWeight { get; set; }

        public List<Arc<TVertex, TArc>> Arcs { get; set; } = new List<Arc<TVertex, TArc>>();

        
    }
}