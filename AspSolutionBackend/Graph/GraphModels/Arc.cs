namespace Graph.GraphModels
{
    public class Arc<TVertex, TArc>
        // where TArc : class where TVertex : class
    {
        public Arc(string id, TArc? arcData, Vertex<TVertex, TArc> to, long? arcWeight = null)
        {
            Id = id;
            ArcData = arcData;
            Target = to;
            Weight = arcWeight ?? 0;
        }


        public string Id { get; set; } = null!;
        public TArc? ArcData { get; set; }

        public long Weight { get; set; }

        // Previous vertex 
        public Vertex<TVertex, TArc>? VPrev { get; set; }
        
        public bool Checked { get; set; } = false;

        public bool IsConnectedToGraph { get; set; } = true;    
        public Vertex<TVertex, TArc>? Target { get; set; }
        public Arc<TVertex, TArc>? Next { get; set; }
    }
}