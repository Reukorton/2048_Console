using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    public class Model
    {
        public bool isGameOver;

        private bool moved;

        public int size { get { return map.size; } }

        private static Random _random = new Random();

        Map map;

        public Model(int size)
        {
            map = new Map(size);
            Start();
        }

        /// <summary>
        /// Получение значений по координатам.
        /// </summary>
        public int GetMap(int x, int y)
        {
            return map.Get(x, y);
        }

        public void Start()
        {
            isGameOver = false;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    map.Set(x, y, 0);
                }
            }

            AddRandomNumber();
            AddRandomNumber();
        }

        private void AddRandomNumber()
        {
            if (isGameOver) return;

            var allZeroPoint = FindZeroPoint();

            if (allZeroPoint.Count == 0) return;

            int index = _random.Next(0, allZeroPoint.Count);
            int x = allZeroPoint[index][0];
            int y = allZeroPoint[index][1];

            map.Set(x, y, _random.Next(0, 10) == 0 ? 4 : 2);
        }

        private List<int[]> FindZeroPoint()
        {
            var _zeroPointList = new List<int[]>();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    int[] MapXY = new int[2];

                    if (map.Get(x, y) == 0)
                    {
                        MapXY[0] = x;
                        MapXY[1] = y;
                        _zeroPointList.Add(MapXY);
                    }
                }
            }

            return _zeroPointList;
        }

        private void Movement(int x, int y, int dx, int dy)
        {
            if (map.Get(x, y) > 0)
            {
                while(map.Get(x + dx, y + dy) == 0)
                {
                    map.Set(x + dx, y + dy, map.Get(x, y));
                    map.Set(x, y, 0);
                    x += dx;
                    y += dy;
                    moved = true;
                }
            }
        }

        private void Join(int x, int y, int dx, int dy)
        {
            if (map.Get(x, y) > 0)
            {
                if (map.Get(x + dx, y + dy) == map.Get(x, y))
                {
                    map.Set(x + dx, y + dy, map.Get(x, y) * 2);
                    while(map.Get(x - dx, y - dy) > 0)
                    {
                        map.Set(x, y, map.Get(x - dx, y - dy));
                        x -= dx;
                        y -= dy;
                    }
                    map.Set(x, y, 0);
                    moved = true;
                }
            }
        }

        public void Right()
        {
            moved = false;
            for (int y = 0; y < size; y++)
            {
                for (int x = size - 2; x >= 0; x--)
                {
                    Movement(x, y, 1, 0);
                }
                for (int x = size - 2; x >= 0; x--)
                {
                    Join(x, y, 1, 0);
                }
            }

            if (moved) AddRandomNumber();
        }

        public void Left()
        {
            moved = false;
            for (int y = 0; y < size; y++)
            {
                for (int x = 1; x < size; x++)
                {
                    Movement(x, y, -1, 0);
                }
                for (int x = 1; x < size; x++)
                {
                    Join(x, y, -1, 0);
                }
            }

            if (moved) AddRandomNumber();
        }

        public void Up()
        {
            moved = false;
            for (int x = 0; x < size; x++)
            {
                for (int y = 1; y < size; y++)
                {
                    Movement(x, y, 0, -1);
                }
                for (int y = 1; y < size; y++)
                {
                    Join(x, y, 0, -1);
                }
            }

            if (moved) AddRandomNumber();
        }

        public void Down()
        {
            moved = false;
            for (int x = 0; x < size; x++)
            {
                for (int y = size - 1; y >= 0; y--)
                {
                    Movement(x, y, 0, 1);
                }
                for (int y = size - 1; y >= 0; y--)
                {
                    Join(x, y, 0, 1);
                }
            }

            if (moved) AddRandomNumber();
        }

        public bool IsGameOver()
        {
            if (isGameOver)
            {
                return isGameOver;
            }

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (map.Get(x, y) == 0)
                    {
                        return false;
                    }
                }
            }

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (map.Get(x, y) == map.Get(x + 1, y) ||
                        map.Get(x, y) == map.Get(x, y + 1))
                    {
                        return false;
                    }
                }
            }

            isGameOver = true;
            return isGameOver;
        }
    }
}
