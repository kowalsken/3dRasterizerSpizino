namespace _3DRasterization
{
    public class Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;
        public float[] vectorTable; //zmieni sie tylko jak zwracany jest Vector4 - w konstruktorze

        public Vector4(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
            vectorTable = new float[4] { x, y, z, w };
        }

        public Vector4(Vector3 v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
            this.W = 1.0f;
            vectorTable = new float[4] { v.X, v.Y, v.Z, 1.0f };
        }
    }
}
