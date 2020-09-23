using System;

namespace _3DRasterization.Lights
{
    class DirectionalLight : Light
    {
        Vector3 position;
        float shininess;

        public DirectionalLight(Vector3 pos)
        {
            this.position = pos;
        }

        public Vector3 Calculate(VertexProcessor vert, Vertex v)
        {
            shininess = 1f;
            Vector3 diffuseColor = new Vector3(0, 0, 100);
            Vector3 specularColor = new Vector3(255, 0, 0);

            Vector3 N = vert.tr(v.normal).Normalize();
            Vector3 V = vert.tr(position).Normalize();
            Vector3 L = (V - position).Normalize();
            Vector3 R = L.Reflect(L, N);

            float diffuseValue = Math.Max(0.0f, L.Dot(N));
            float specularValue = (float)Math.Pow(R.Dot(V), shininess);

            diffuseColor *= diffuseValue;
            specularColor *= specularValue;

            Vector3 col = diffuseColor + specularColor;
            return col;
        }
    }
}
