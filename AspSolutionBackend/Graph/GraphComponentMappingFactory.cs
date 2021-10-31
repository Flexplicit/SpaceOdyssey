using System.Collections.Generic;
using System.Linq;
using Graph.GraphModels;

namespace Graph
{
    
    public  class GraphComponentMapper
    {
        public Vertex<TVertex, TArc> MapObjectToVertexWithData<TVertex, TArc>(TVertex vertexData, string id)
        {
            return new Vertex<TVertex, TArc>(id, vertexData);
        }

        public static List<TArcData?> MapDataFromArcs<TArcData, TVertex>(IEnumerable<Arc<TVertex, TArcData>> arcs)
        {
            return arcs.Select(arc => arc.ArcData).ToList();
        }
    }
}