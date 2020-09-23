namespace _3DRasterization
{
    public interface Mesh
    {
        void DrawMesh(Rasterization rasterizer, VertexProcessor processor, Light l);
        void MakeNormals();
        void Light(Light l, VertexProcessor v);
    }
}
