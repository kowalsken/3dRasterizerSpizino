using System;
using System.Collections.Generic;
using System.IO;

namespace _3DRasterization.Geometry
{
    public class ObjMesh : Mesh
    {
        public List<Vertex> vertexes;
        public List<int> indexes;
        public List<Vector3> norm;

        public ObjMesh()
        {
            vertexes = new List<Vertex>();
            indexes = new List<int>();
            norm = new List<Vector3>();
        }

        public void Light(Light l, VertexProcessor processor)
        {
            foreach (Vertex v in vertexes)
            {
                v.light = l.Calculate(processor, v);
            }
        }

        public void DrawMesh(Rasterization rasterizer, VertexProcessor processor, Light l)
        {
            for (int i = 0; i < indexes.Count; i += 3)
            {
                rasterizer.Triangle(processor.tr(vertexes[indexes[i]].position), processor.tr(vertexes[indexes[i + 1]].position), processor.tr(vertexes[indexes[i + 2]].position), vertexes[indexes[i]], vertexes[indexes[i + 1]], vertexes[indexes[i + 2]], l, processor);
            }
        }

        //policzenie wszystkich od razu w Scenie - nadpisywanie prawdopodobnie
        public void MakeNormals()
        {
            Vector3 v = new Vector3(0, 0, 0);
            for (int i = 0; i < vertexes.Count; i++)
            {
                vertexes[i].normal = v;
            }

            for (int i = 0; i < indexes.Count; i += 3)
            {
                Vector3 U = vertexes[indexes[i + 1]].position - vertexes[indexes[i]].position;
                Vector3 V = vertexes[indexes[i + 2]].position - vertexes[indexes[i]].position;
                Vector3 normal = U.Cross(V);
                normal = normal.Normalize();
                vertexes[indexes[i]].normal += normal;
                vertexes[indexes[i + 1]].normal += normal;
                vertexes[indexes[i + 2]].normal += normal;
            }

            for (int i = 0; i < vertexes.Count; i++)
            {
                vertexes[i].normal.Normalize();
            }
        }
    }
}
