using ObjLoader.Loader.Loaders;
using _3DRasterization.Geometry;
using _3DRasterization.Lights;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace _3DRasterization
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Create collections for rasterize
            Buffer buff = new Buffer(1500, 1500, Color.BlanchedAlmond);
            List<Mesh> meshList = new List<Mesh>();
            List<Light> lightList = new List<Light>();
            Rasterization render = new Rasterization(buff);
            VertexProcessor vertex = new VertexProcessor();
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            Vector3 p0 = new Vector3(1f, 1f, 1f);
            DirectionalLight light = new DirectionalLight(p0);
            #endregion


            #region Dodanie obiektów do sceny
            ObjMesh obj = new ObjMesh();

            Console.WriteLine("OBJ File Name:");
            string nameObj = Console.ReadLine();
            var fileStream = new FileStream(nameObj, FileMode.Open);
            var result = objLoader.Load(fileStream);

            //Загрузка позиции вершин
            foreach (ObjLoader.Loader.Data.VertexData.Vertex l in result.Vertices)
            {
                Vector3 pos = new Vector3(l.X, l.Y, l.Z);
                Vertex newVert = new Vertex(pos);
                obj.vertexes.Add(newVert);
            }

            List<int> indexes = new List<int>();

            foreach (ObjLoader.Loader.Data.Elements.Group n in result.Groups)
            {
                foreach (ObjLoader.Loader.Data.Elements.Face f in n.Faces)
                {
                    for (int i = 0; i < f._vertices.Count; i++)
                    {
                        indexes.Add(f._vertices[i].VertexIndex - 1);
                    }
                }
            }

            lightList.Add(light);
            obj.indexes = indexes;
            meshList.Add(obj);
            Scene scene = new Scene(meshList, lightList, render, vertex);

            #endregion
            buff.SaveImage();

            Console.WriteLine("Rasterization Complete.");
            Console.ReadKey();
        }
    }
}
