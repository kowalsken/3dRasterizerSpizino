namespace _3DRasterization
{
    public interface Light
    {
        Vector3 Calculate(VertexProcessor vert, Vertex v);
    }
}
