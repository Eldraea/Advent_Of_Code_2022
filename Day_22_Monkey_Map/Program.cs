﻿using System.Text.RegularExpressions;

string input = File.ReadAllText("../../../input.txt");
int blockSize = 50;
const int right = 0;
const int down = 1;
const int left = 2;
const int up = 3;

Console.WriteLine(GetFinalPassword(input, 1));
Console.WriteLine(GetFinalPassword(input, 2));

int GetFinalPassword(string input, int part)
=> part == 1
? DecryptMap(input, "A->B0 C0 B0 E0\r\n\r\nB->A0 B0 A0 B0\r\n\r\nC->C0 E0 C0 A0\r\n\r\nD->E0 F0 E0 F0\r\n\r\nE->D0 A0 D0 C0\r\n\r\nF->F0 D0 F0 D0")
: DecryptMap(input, "A->B0 C0 D2 F1\r\n\r\nB->E2 C1 A0 F0\r\n\r\nC->B3 E0 D3 A0\r\n\r\nD->E0 F0 A2 C1\r\n\r\nE->B2 F1 D0 C0\r\n\r\nF->E3 B0 A3 D0");


int DecryptMap(string input, string topology)
{
    var (map, commands) = Parse(input);
    var state = new State("A", new Coordinate(0, 0), right);

    foreach (var command in commands)
    {
        switch (command)
        {
            case Left:
                state = state with { direction = (state.direction + 3) % 4 };
                break;
            case Right:
                state = state with { direction = (state.direction + 1) % 4 };
                break;
            case Forward(var n):
                for (var i = 0; i < n; i++)
                {
                    var stateNext = Step(topology, state);
                    var global = ToGlobal(stateNext);
                    if (map[global.abscissa][global.ordinate] == '.')
                    {
                        state = stateNext;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
        }
    }

    return 1000 * (ToGlobal(state).abscissa + 1) + 4 * (ToGlobal(state).ordinate + 1) + state.direction;
}

Coordinate ToGlobal(State state) =>
    state.block switch
    {
        "A" => state.coord + new Coordinate(0, blockSize),
        "B" => state.coord + new Coordinate(0, 2 * blockSize),
        "C" => state.coord + new Coordinate(blockSize, blockSize),
        "D" => state.coord + new Coordinate(2 * blockSize, 0),
        "E" => state.coord + new Coordinate(2 * blockSize, blockSize),
        "F" => state.coord + new Coordinate(3 * blockSize, 0),
        _ => throw new Exception()
    };

State Step(string topology, State state)
{

    bool wrapsAround(Coordinate coord) =>
        coord.ordinate < 0 || coord.ordinate >= blockSize || coord.abscissa < 0 || coord.abscissa >= blockSize;

    var (srcBlock, coord, direction) = state;
    var dstBlock = srcBlock;

    coord = direction switch
    {
        left => coord with { ordinate = coord.ordinate - 1 },
        down => coord with { abscissa = coord.abscissa + 1 },
        right => coord with { ordinate = coord.ordinate + 1 },
        up => coord with { abscissa = coord.abscissa - 1 },
        _ => throw new Exception()
    };

    if (wrapsAround(coord))
    {

        var line = topology.Split("\r\n").Single(x => x.StartsWith(srcBlock));
        var mapping = line.Split("->")[1].Split(" ");
        var neighbour = mapping[direction];
        dstBlock = neighbour.Substring(0, 1);
        var rotate = int.Parse(neighbour.Substring(1));

        coord = coord with
        {
            abscissa = (coord.abscissa + blockSize) % blockSize,
            ordinate = (coord.ordinate + blockSize) % blockSize,
        };

        for (var i = 0; i < rotate; i++)
        {
            coord = coord with 
            { 
                abscissa = coord.ordinate, 
                ordinate = blockSize - coord.abscissa - 1 
            };
            direction = (direction + 1) % 4;
        }
    }

    return new State(dstBlock, coord, direction);
}

(string[] map, Cmd[] path) Parse(string input)
{
    var blocks = input.Split("\r\n\r\n");

    var map = blocks[0].Split("\r\n");
    var commands = Regex
        .Matches(blocks[1], @"(\d+)|L|R")
        .Select<Match, Cmd>(m =>
            m.Value switch {
                "L" => new Left(),
                "R" => new Right(),
                string n => new Forward(int.Parse(n)),
            })
        .ToArray();

    return (map, commands);
}

record State(string block, Coordinate coord, int direction);

record Coordinate(int abscissa, int ordinate)
{
    public static Coordinate operator +(Coordinate pointA, Coordinate pointB) =>
        new Coordinate(pointA.abscissa + pointB.abscissa, pointA.ordinate + pointB.ordinate);

    public static Coordinate operator -(Coordinate pointA, Coordinate pointB) =>
        new Coordinate(pointA.abscissa - pointB.abscissa, pointA.ordinate - pointB.ordinate);
}

interface Cmd { }
record Forward(int n) : Cmd;
record Right() : Cmd;
record Left() : Cmd;