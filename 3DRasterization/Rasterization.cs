using System;
using System.Drawing;

namespace _3DRasterization
{
    public class Rasterization
    {
        Buffer buff; //Создание класса Buffer


        //Передеча в функцию
        public Rasterization(Buffer Buff)
        {
            this.buff = Buff;
        }


        //Ограничение на значение цвета
        private int maxCol(int c)
        {
            if (c > 255) return 255;
            if (c < 0) return 0;

            return c;
        }

        //Механизм триангуляции
        public void Triangle(Vector3 pos1, Vector3 pos2, Vector3 pos3, Vertex v1, Vertex v2, Vertex v3, Light l, VertexProcessor vert)
        {
            //Позиция точки относительно расширения
            float p1x = (pos1.X + 1) * buff.colorBuffer.Width * 0.5f;
            float p1y = (pos1.Y + 1) * buff.colorBuffer.Height * 0.5f;

            float p2x = (pos2.X + 1) * buff.colorBuffer.Width * 0.5f;
            float p2y = (pos2.Y + 1) * buff.colorBuffer.Height * 0.5f;

            float p3x = (pos3.X + 1) * buff.colorBuffer.Width * 0.5f;
            float p3y = (pos3.Y + 1) * buff.colorBuffer.Height * 0.5f;


            //Максимальное, минимальное значение вершин.
            int minx = (int)Math.Min(p1x, Math.Min(p2x, p3x));
            int miny = (int)Math.Min(p1y, Math.Min(p2y, p3y));
            int maxx = (int)Math.Max(p1x, Math.Max(p2x, p3x));
            int maxy = (int)Math.Max(p1y, Math.Max(p2y, p3y));

            minx = Math.Max(minx, 0);
            maxx = Math.Min(maxx, buff.colorBuffer.Width - 1);
            miny = Math.Max(miny, 0);
            maxy = Math.Min(maxy, buff.colorBuffer.Height - 1);

            //Цикл для подсчета всех пикселей
            for (int x = minx; x <= maxx; x++)
                for (int y = miny; y <= maxy; y++)
                {
                    //Принадженость пикселя к видимой области объекта
                    if ((((p1x - p2x) * (y - p1y) - (p1y - p2y) * (x - p1x) > 0) || ((p1x - p2x) * (y - p1y) - (p1y - p2y) * (x - p1x) >= 0))
                    &&
                        (((p2x - p3x) * (y - p2y) - (p2y - p3y) * (x - p2x) > 0) || ((p2x - p3x) * (y - p2y) - (p2y - p3y) * (x - p2x) >= 0))
                    &&
                        (((p3x - p1x) * (y - p3y) - (p3y - p1y) * (x - p3x) > 0) || ((p3x - p1x) * (y - p3y) - (p3y - p1y) * (x - p3x) >= 0)))
                    {
                        //Барицентрические координаты, используються что бы определить, принадлежат ли координаты текущего пикселя какому-либо из треугольников
                        float lambda1 = (((p2y - p3y) * (x - p3x)) + ((p3x - p2x) * (y - p3y))) /
                                    (((p2y - p3y) * (p1x - p3x)) + ((p3x - p2x) * (p1y - p3y)));

                        float lambda2 = (((p3y - p1y) * (x - p3x)) + ((p1x - p3x) * (y - p3y))) /
                                        (((p3y - p1y) * (p2x - p3x)) + ((p1x - p3x) * (p2y - p3y)));
                        float lambda3 = 1 - lambda1 - lambda2;

                        //Буфер глубины, расчет градиента
                        float depth = lambda1 * pos1.Z + lambda2 * pos2.Z + lambda3 * pos3.Z;

                        if (depth < buff.depthBuffer[x, y])
                        {
                            //Свечение на вершину
                            Vector3 ambient = new Vector3(50, 0, 0);
                            Vector3 color = (v1.light * lambda1) + (v2.light * lambda2) + (v3.light * lambda3);
                            Color col = Color.FromArgb(maxCol((int)(color.X + ambient.X)), maxCol((int)(color.Y + ambient.Y)), maxCol((int)(color.Z + ambient.Z)));
                            buff.depthBuffer[x, y] = depth;
                            buff.colorBuffer.SetPixel(x, y, col);
                        }
                    }
                }
        }
    }
}
