namespace Graph.GraphModels
{
    public class Arc<TVertex, TArc>
        // where TArc : class where TVertex : class
    {
        public Arc(string id, TArc? arcData, Vertex<TVertex, TArc> to)
        {
            Id = id;
            ArcData = arcData;
            Target = to;
        }



        public string Id { get; set; } = null!;
        public TArc? ArcData { get; set; }
        public bool Checked { get; set; } = false;

        public Vertex<TVertex, TArc>? Target { get; set; }
        public Arc<TVertex, TArc>? Next { get; set; }
    }
}