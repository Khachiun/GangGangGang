﻿using Czaplicki.SFMLE;
using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    static class Priority
    {
        public const int TILE_BASE = 0;
        public const int UNIT_BASE = 16;
        public const int UI_BASE = 32;
        //public const int _BASE;
    }
    abstract class CollitionComponent : Entity
    {
        public abstract bool Collide(Vector2f point);
    }
    class CircleCollition : CollitionComponent
    {
        internal float radiePowTwo;

        public CircleCollition(float radie)
        {
            this.radiePowTwo = radie * radie;
        }
        public CircleCollition(float radie, Vector2f offset) : this(radie)
        {
            base.Offset = offset;
        }

        public override bool Collide(Vector2f point)
        {
            return (Position - point).Pow2().Length() < radiePowTwo;
        }

    }

    class RectangelCollition : CollitionComponent
    {
        public Square area;
        public Square testArea;

        //public RectangelShape shape;

        protected override void OffsetChanged()
        {
            ////shape.Rectangel = testArea;
            testArea = area + Position;

            ////Console.WriteLine(drawID);
            //Console.WriteLine(Parent);
            //Console.WriteLine("position : " + Position);
            //Console.WriteLine("testPosition : " + testArea.Position);
            //Console.WriteLine();

            base.OffsetChanged();
        }
        public RectangelCollition(Square area)
        {
            this.area = area;
        }

        public RectangelCollition(Square area, Vector2f offset) : this (area)
        {
            base.Offset = offset;
        }

        public override void Update()
        {

        }

        public override bool Collide(Vector2f point) // optimize for every frame use
        {
            //                                            
            return point.X > testArea.Position.X && point.X < testArea.Position.X + testArea.Size.X &&
                   point.Y > testArea.Position.Y && point.Y < testArea.Position.Y + testArea.Size.Y;

            //Console.WriteLine($"p.x > r.x = " + (point.X >= testArea.Position.X));
            //Console.WriteLine($"p.x < r.x + w = " + (point.X < testArea.Position.X + testArea.Size.X));
            //Console.WriteLine($"p.y > r.x = " + (point.X >= testArea.Position.X));
            //Console.WriteLine($"p.x > r.x = " + (point.X >= testArea.Position.X));

            //Console.WriteLine($"pos : {testArea.Position}, size : {testArea.Size} ");
            //Console.WriteLine(testArea.ToString() + ";    " + b + ";    " + point) ;
            //return b;
        }
    }
}
