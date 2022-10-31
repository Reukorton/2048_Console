using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _2048
{
    public class Map
    {
        public int size { get; private set; }
        int[,] map;

        public Map(int size)
        {
            this.size = size;
            map = new int[size, size];
        }

        /// <summary>
        /// Получение значения по координатам.
        /// </summary>
        /// <param name="x">Координата Х.</param>
        /// <param name="y">Координата Y.</param>
        public int Get(int x, int y)
        {
            if (OnMap(x, y))
                return map[x, y];
            return -1;
        }

        /// <summary>
        /// Присвоение числа по координатам.
        /// </summary>
        /// <param name="x">Координата Х.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="number">Добовляемое значение.</param>
        public void Set(int x, int y, int number)
        {
            if (OnMap(x, y))
                map[x, y] = number;
        }

        public bool OnMap(int x, int y)
        {
            return x >= 0 && x < size &&
                   y >= 0 && y < size;
        }
    }
}
