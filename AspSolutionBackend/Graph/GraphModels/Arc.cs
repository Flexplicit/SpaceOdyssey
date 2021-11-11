﻿using System;

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

        public Arc(string id, TArc? arcData, Vertex<TVertex, TArc> to, DateTime arcStartConstraint,
            DateTime arcEndConstraint, long? arcWeight = null)
        {
            Id = id;
            ArcData = arcData;
            Target = to;
            Weight = arcWeight ?? 0;

            // Necessary dijkstra time window constraints
            ArcStart = arcStartConstraint;
            ArcEnd = arcEndConstraint;
        }


        public string Id { get; set; } = null!;
        public TArc? ArcData { get; set; }

        public long Weight { get; set; }

        public DateTime? ArcStart { get; set; }
        public DateTime? ArcEnd { get; set; }
        public Vertex<TVertex, TArc>? VPrev { get; set; }

        public bool Checked { get; set; } = false;

        public bool IsConnectedToGraph { get; set; } = true;
        public Vertex<TVertex, TArc>? Target { get; set; }
        public Arc<TVertex, TArc>? Next { get; set; }
    }
}