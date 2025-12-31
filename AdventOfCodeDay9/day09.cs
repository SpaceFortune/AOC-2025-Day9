using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2025;

class Day09
{
    private readonly List<string> input = new(File.ReadAllLines("input.txt"));

    private List<(int x, int y)> ReadPoints() =>
        input.Select(l =>
        {
            var p = l.Split(',');
            return (int.Parse(p[0]), int.Parse(p[1]));
        }).ToList();

    public void Part1()
    {
        var points = ReadPoints();

        long maxArea =
            (from a in points
             from b in points
             where a != b
             let dx = Math.Abs(a.x - b.x)
             let dy = Math.Abs(a.y - b.y)
             select (dx + 1L) * (dy + 1L))
            .Max();

        Console.WriteLine($"Day 9 Part 1: {maxArea}");
    }

    public void Part2()
    {
        var points = ReadPoints();

        var xs = points.Select(p => p.x).Distinct().Order().ToList();
        var ys = points.Select(p => p.y).Distinct().Order().ToList();

        var xIndex = xs.Select((v, i) => (v, i)).ToDictionary(t => t.v, t => t.i);
        var yIndex = ys.Select((v, i) => (v, i)).ToDictionary(t => t.v, t => t.i);

        int width = xs.Count + 2;
        int height = ys.Count + 2;

        bool[,] wall = new bool[width, height];
        bool[,] outside = new bool[width, height];
        bool[,] allowed = new bool[width, height];

        var cells = points
            .Select(p => (xIndex[p.x] + 1, yIndex[p.y] + 1))
            .ToList();

        foreach (var (x, y) in cells)
            wall[x, y] = true;

        void DrawLine((int x, int y) a, (int x, int y) b)
        {
            if (a.x == b.x)
                for (int y = Math.Min(a.y, b.y); y <= Math.Max(a.y, b.y); y++)
                    wall[a.x, y] = true;
            else
                for (int x = Math.Min(a.x, b.x); x <= Math.Max(a.x, b.x); x++)
                    wall[x, a.y] = true;
        }

        for (int i = 0; i < cells.Count; i++)
            DrawLine(cells[i], cells[(i + 1) % cells.Count]);

        var q = new Queue<(int x, int y)>();
        q.Enqueue((0, 0));
        outside[0, 0] = true;

        int[] d = { 1, 0, -1, 0, 1 };

        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                int nx = x + d[i];
                int ny = y + d[i + 1];
                if (nx < 0 || ny < 0 || nx >= width || ny >= height) continue;
                if (outside[nx, ny] || wall[nx, ny]) continue;
                outside[nx, ny] = true;
                q.Enqueue((nx, ny));
            }
        }

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                allowed[x, y] = !outside[x, y];

        bool RectangleOk((int x, int y) a, (int x, int y) b)
        {
            int x1 = Math.Min(a.x, b.x);
            int x2 = Math.Max(a.x, b.x);
            int y1 = Math.Min(a.y, b.y);
            int y2 = Math.Max(a.y, b.y);

            for (int x = x1; x <= x2; x++)
                if (!allowed[x, y1] || !allowed[x, y2]) return false;

            for (int y = y1; y <= y2; y++)
                if (!allowed[x1, y] || !allowed[x2, y]) return false;

            return true;
        }

        long maxArea = 0;

        for (int i = 0; i < cells.Count; i++)
        {
            for (int j = i + 1; j < cells.Count; j++)
            {
                long dx = Math.Abs(points[i].x - points[j].x);
                long dy = Math.Abs(points[i].y - points[j].y);
                long area = (dx + 1) * (dy + 1);

                if (area <= maxArea) continue;
                if (RectangleOk(cells[i], cells[j]))
                    maxArea = area;
            }
        }

        Console.WriteLine($"Day 9 Part 2: {maxArea}");
    }
}
