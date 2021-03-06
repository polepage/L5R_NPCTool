﻿namespace NPC.Data.GameObjects
{
    public interface IDemeanor: IGameObject
    {
        int Air { get; set; }
        int Earth { get; set; }
        int Fire { get; set; }
        int Water { get; set; }
        int Void { get; set; }
        string Unmasking { get; set; }
        string Description { get; set; }
    }
}
