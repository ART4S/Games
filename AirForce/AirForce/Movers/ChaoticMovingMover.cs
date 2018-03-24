﻿using System;
using System.Collections.Generic;

namespace AirForce
{
    public class ChaoticMovingMover : IMover
    {
        private readonly FlyingObject source;
        private readonly Random random = new Random();

        public ChaoticMovingMover(FlyingObject source)
        {
            this.source = source;
        }

        public void Move(Field field, Ground ground, List<FlyingObject> objectsOnField, RewindMacroCommand rewindMacroCommand)
        {
            var shiftPositionCommand = new ChangePositionCommand(source);
            Point2D shift;

            do
            {
                shift = new Point2D(
                    x: -source.Movespeed,
                    y: +source.Movespeed * random.Next(-1, 2)); // random values: -1 0 1
            }
            while (CollisionHandler.IsIntersectGround(source.Position + shift, source.Radius, ground));

            shiftPositionCommand.ShiftPostion(shift);
            rewindMacroCommand.AddCommand(shiftPositionCommand);
        }
    }
}