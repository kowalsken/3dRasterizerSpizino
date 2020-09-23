using System.Collections.Generic;

namespace _3DRasterization
{
    public class Scene
    {
        public Scene(List<Mesh> meshList, List<Light> lights, Rasterization r, VertexProcessor v)
        {
            foreach (Mesh m in meshList)
            {
                m.MakeNormals();
                foreach (Light l in lights)
                {
                    m.Light(l, v);
                    m.DrawMesh(r, v, l);
                }
            }
        }
    }
}
