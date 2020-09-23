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

        #region Przeciązanie operatorów

        //suma dwoch wektorow
        public static Vector3 operator +(Vector3 v, Vector3 v2)
        {
            Vector3 vect;
            return vect = new Vector3(v.X + v2.X, v.Y + v2.Y, v.Z + v2.Z);
        }

        //roznica dwoch wektorow
        public static Vector3 operator -(Vector3 v, Vector3 v2)
        {
            Vector3 vect;
            return vect = new Vector3(v.X - v2.X, v.Y - v2.Y, v.Z - v2.Z);
        }

        //przemnozenie wartosci wektora przez skalar
        public static Vector3 operator *(Vector3 v, float scalar)
        {
            Vector3 vect;
            return vect = new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }
        #endregion

        #region Operacje na wektorach

        //iloczyn wektorowy
        public Vector3 Cross(Vector3 v)
        {
            Vector3 vect = new Vector3
                (this.Y * v.Z - this.Z * v.Y,
                this.Z * v.X - this.X * v.Z,
                this.X * v.Y - this.Y * v.X);
            return vect;
        }

        //iloczyn skalarny dwoch wektorow np. do obliczenia cosinusa 
        public float Dot(Vector3 v)
        {
            return this.X * v.X + this.Y * v.Y + this.Z * v.Z;
        }

        //Returns the reflectiton vector given an incidence vector i and a normal vector n. The resulting vector is the identical number of components as the two input vectors.
        //The normal vector n should be normalized. If n is normalized, the output vector will have the same length as the input incidence vector i.

        public Vector3 Reflect(Vector3 I, Vector3 N) //i - promień przychodzący, n - odbity
        {
            N = N.Normalize();
            return I - (N * 2.0f) * I.Dot(N);
            //I - 2.0 * dot(N, I) * N
            //return i - (n * 2.0f) * i.Dot(n);
        }

        //długość wektora
        public float GetLength()
        {
            return (float)Math.Sqrt(Dot(this));
        }

        const float eps = 0.0001f;
        //normalizacja wektora
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
