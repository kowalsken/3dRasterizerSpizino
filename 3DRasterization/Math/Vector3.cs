using System;

namespace _3DRasterization
{
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #region Математичиские операции

        //Сумма двух векторов
        public static Vector3 operator +(Vector3 v, Vector3 v2)
        {
            Vector3 vect;
            return vect = new Vector3(v.X + v2.X, v.Y + v2.Y, v.Z + v2.Z);
        }

        //Разница двух векторов
        public static Vector3 operator -(Vector3 v, Vector3 v2)
        {
            Vector3 vect;
            return vect = new Vector3(v.X - v2.X, v.Y - v2.Y, v.Z - v2.Z);
        }

        //Умножение вектора на скаляр
        public static Vector3 operator *(Vector3 v, float scalar)
        {
            Vector3 vect;
            return vect = new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }
        #endregion

        #region Операции на векторах

        //Добуток векторов
        public Vector3 Cross(Vector3 v)
        {
            Vector3 vect = new Vector3
                (this.Y * v.Z - this.Z * v.Y,
                this.Z * v.X - this.X * v.Z,
                this.X * v.Y - this.Y * v.X);
            return vect;
        }

        //Добуток скалярный двух векторов
        public float Dot(Vector3 v)
        {
            return this.X * v.X + this.Y * v.Y + this.Z * v.Z;
        }


        public Vector3 Reflect(Vector3 I, Vector3 N) //i - промень проходной, n - отражение
        {
            N = N.Normalize();
            return I - (N * 2.0f) * I.Dot(N);
            //I - 2.0 * dot(N, I) * N
            //return i - (n * 2.0f) * i.Dot(n);
        }

        //длина вектора
        public float GetLength()
        {
            return (float)Math.Sqrt(Dot(this));
        }

        const float eps = 0.0001f;
        //нормализация вектора
        public Vector3 Normalize()
        {
            Vector3 v;
            float len = this.GetLength();
            if (len > eps)
            {
                return (this) * (1 / len);
            }
            else
            {
                return v = new Vector3(.0f, .0f, .0f);
            }
        }
        #endregion
    }
}
