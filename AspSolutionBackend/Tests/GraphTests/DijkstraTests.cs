using System;
using System.Linq;
using FluentAssertions;
using Graph;
using Graph.GraphModels;
using Xunit;

namespace Tests.GraphTests
{
    // Very simple and fast tests in order to test custom dijkstra with time window constraints path finder algorithm
    public class DijkstraTests
    {
        [Fact]
        public void TestDijkstraSimpleSuccessFlow()
        {
            var (graph, vertices) = CreateSimpleGraphWithVertices();


            graph.CreateArc("a00-1", vertices[0], vertices[1], 0, 10, DateTime.Now.AddHours(1), DateTime.Now.AddDays(2));
            graph.CreateArc("a01-1", vertices[0], vertices[1], 0, 1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(10));
    

            graph.CreateArc("b10-2", vertices[1], vertices[2], 0, 1, DateTime.Now.AddDays(1).AddHours(2), DateTime.Now.AddDays(3));
            graph.CreateArc("b11-2", vertices[1], vertices[2], 0, 2, DateTime.Now, DateTime.Now.AddDays(4));
            graph.CreateArc("b12-2", vertices[1], vertices[2], 0, 6, DateTime.Now.AddDays(3), DateTime.Now.AddDays(5));
            graph.CreateArc("b13-2", vertices[1], vertices[2], 0, 7, DateTime.Now.AddDays(3), DateTime.Now.AddDays(1).AddHours(1));


            graph.CreateArc("c10-2", vertices[2], vertices[3], 0, 3, DateTime.Now.AddDays(4),
                DateTime.Now.AddDays(4).AddHours(2));

            var res = graph.CustomDijkstraPathFinder(vertices[0], vertices[3], DateTime.Now);
            var ids = res.Select(x => x.Id);
            ids.Should().ContainInOrder("a00-1", "b13-2", "c10-2"); // Arc ids
        }


        [Fact]
        public void TestDijkstraMediumSuccessFlow()
        {
            var (graph, vertices) = CreateSimpleGraphWithVertices();


            graph.CreateArc("a00-1", vertices[0], vertices[1], 0, 10, DateTime.Now.AddHours(1),
                DateTime.Now.AddDays(1));
            graph.CreateArc("a01-1", vertices[0], vertices[1], 0, 8, DateTime.Now.AddHours(2), DateTime.Now.AddDays(2));
            graph.CreateArc("a02-1", vertices[0], vertices[1], 0, 11, DateTime.Now.AddHours(10),
                DateTime.Now.AddDays(1));


            graph.CreateArc("b10-2", vertices[1], vertices[2], 0, 1, DateTime.Now, DateTime.Now.AddDays(3));
            graph.CreateArc("b11-2", vertices[1], vertices[2], 0, 2, DateTime.Now.AddDays(1).AddHours(1),
                DateTime.Now.AddDays(4));
            graph.CreateArc("b12-2", vertices[1], vertices[2], 0, 6, DateTime.Now.AddDays(3), DateTime.Now.AddDays(5));


            graph.CreateArc("c10-2", vertices[2], vertices[3], 0, 3, DateTime.Now.AddDays(6),
                DateTime.Now.AddDays(7).AddHours(2));

            var res = graph.CustomDijkstraPathFinder(vertices[0], vertices[3], DateTime.Now);
            var ids = res.Select(x => x.Id);
            ids.Should().ContainInOrder("a01-1", "b12-2", "c10-2"); // Arc ids
        }


        [Fact]
        public void TestDijkstraMustTakeHeaviestRouteInEveryArcInOrderToFindAPath()
        {
            var (graph, vertices) = CreateSimpleGraphWithVertices();


            graph.CreateArc("a00-1", vertices[0], vertices[1], 0, 15, DateTime.Now.AddHours(5),
                DateTime.Now.AddDays(6));
            graph.CreateArc("a01-1", vertices[0], vertices[1], 0, 3, DateTime.Now.AddHours(2), DateTime.Now.AddDays(8));


            graph.CreateArc("b10-2", vertices[1], vertices[2], 0, 1, DateTime.Now, DateTime.Now.AddDays(3));
            graph.CreateArc("b11-2", vertices[1], vertices[2], 0, 2, DateTime.Now.AddDays(1).AddHours(1),
                DateTime.Now.AddDays(4));
            graph.CreateArc("b12-2", vertices[1], vertices[2], 0, 10, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8));


            graph.CreateArc("c10-2", vertices[2], vertices[3], 0, 3, DateTime.Now.AddDays(8),
                DateTime.Now.AddDays(9).AddHours(2));

            var res = graph.CustomDijkstraPathFinder(vertices[0], vertices[3], DateTime.Now);
            var ids = res.Select(x => x.Id);
            ids.Should().ContainInOrder("a00-1", "b12-2", "c10-2"); // Arc ids
        }

        [Fact]
        public void TestDijkstraShouldReturnEmptyWhenContainsNoPaths()
        {
            var (graph, vertices) = CreateSimpleGraphWithVertices();


            graph.CreateArc("a00-1", vertices[0], vertices[1], 0, 15, DateTime.Now.AddHours(5),
                DateTime.Now.AddDays(6));
            graph.CreateArc("a01-1", vertices[0], vertices[1], 0, 3, DateTime.Now.AddHours(2), DateTime.Now.AddDays(8));

            graph.CreateArc("b10-2", vertices[1], vertices[2], 0, 1, DateTime.Now, DateTime.Now.AddDays(3));
            graph.CreateArc("b11-2", vertices[1], vertices[2], 0, 2, DateTime.Now.AddDays(1).AddHours(1),
                DateTime.Now.AddDays(4));

            graph.CreateArc("c10-2", vertices[2], vertices[3], 0, 3, DateTime.Now, DateTime.Now.AddDays(9).AddHours(2));

            var res = graph.CustomDijkstraPathFinder(vertices[0], vertices[3], DateTime.Now);
            res.Should().BeEmpty();
        }


        [Fact]
        public void TestDijkstraShouldGiveCorrectPathOnSingleTravel()
        {
            var (graph, vertices) = CreateSimpleGraphWithVertices(2);

            graph.CreateArc("a00-1", vertices[0], vertices[1], 0, 15, DateTime.Now.AddHours(5),
                DateTime.Now.AddDays(6));
            graph.CreateArc("a01-1", vertices[0], vertices[1], 0, 0, DateTime.Now, DateTime.Now.AddDays(8));
            graph.CreateArc("a02-1", vertices[0], vertices[1], 0, 1, DateTime.Now, DateTime.Now.AddDays(8));
            graph.CreateArc("a03-1", vertices[0], vertices[1], 0, 2, DateTime.Now.AddHours(2).AddDays(1),
                DateTime.Now.AddDays(8));
            graph.CreateArc("a04-1", vertices[0], vertices[1], 0, 15, DateTime.Now.AddHours(5),
                DateTime.Now.AddDays(6));

            var res = graph.CustomDijkstraPathFinder(vertices[0], vertices[1], DateTime.Now.AddDays(1));
            var ids = res.Select(x => x.Id);
            ids.Should().ContainInOrder("a03-1");
        }

        [Fact]
        public void TestDijkstraShouldReturnEmptyWhenNoArcs()
        {
            var (graph, vertices) = CreateSimpleGraphWithVertices(2);
            
            var res = graph.CustomDijkstraPathFinder(vertices[0], vertices[1], DateTime.Now.AddDays(1));
            
            res.Should().BeEmpty();
        }
        private ( Graph<int, int> graph, Vertex<int, int>[] vertices) CreateSimpleGraphWithVertices(int amount = 4)
        {
            var graph = new Graph<int, int>("Dijkstra Test");
            var vertices = new Vertex<int, int>[amount];
            for (var i = 0; i < amount; i++)
            {
                vertices[i] = graph.CreateVertex(i.ToString(), i);
            }

            return (graph, vertices);
        }
    }
}