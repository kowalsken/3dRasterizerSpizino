namespace _3DRasterization
{
    public interface Light
    {
        Vector3 Calculate(VertexProcessor vert, Vertex v);
        Vector3 Calculate(VertexProcessor vert, Vector3 normal);

        Vector3 CelShading(VertexProcessor vert, Vector3 normal);
        Vector3 GoochShading(VertexProcessor vert, Vector3 normal);
        Vector3 GoraudShading(VertexProcessor vert, Vertex v1, Vertex v2, Vertex v3);
    }
}
