using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace h073_pu_iso
{
    public static class StageLoader
    {
        public static Stage LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var lines = File.ReadAllLines(path);
            var name = lines[0];
            var dimensions = lines[1].Split(":");
            var width = int.Parse(dimensions[0]);
            var height = int.Parse(dimensions[1]);
            var content = lines.SubArray(2);

            var stage = new Stage(width, height);
            var start = Point.Zero;
            var end = Point.Zero;
            var movables = new List<Point>();
            var parser = new List<Point>();
            var color = new List<Color>();

            for (var y = 0; y < height; y++)
            {
                var row = content[y];
                for (var x = 0; x < width; x++)
                {
                    var element = row[x];
                    stage.SetBlocked(x, y, element.Equals('x'));
                    if (element.Equals('s'))
                    {
                        start.X = x;
                        start.Y = y;
                    }
                    if (element.Equals('e'))
                    {
                        end.X = x;
                        end.Y = y;
                    }
                    if (element.Equals('ö'))
                    {
                        parser.Add(new Point(x, y));
                        color.Add(Color.Red);
                    }
                    if (element.Equals('c'))
                    {
                        movables.Add(new Point(x, y));
                    }
                }
            }

            stage.SetStart(start.X, start.Y);
            stage.SetEnd(end.X, end.Y);
            foreach (var movable in movables)
            {
                stage.AddMovable(movable.X, movable.Y);
            }
            foreach (var par in parser)
            {
                stage.AddParser(par.X, par.Y, Color.Red);
            }
            return stage;
        }
    }
    
    public static class Extensions
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }
        
        public static T[] SubArray<T>(this T[] array, int offset)
        {
            T[] result = new T[array.Length - offset];
            Array.Copy(array, offset, result, 0, result.Length);
            return result;
        }
    }
}