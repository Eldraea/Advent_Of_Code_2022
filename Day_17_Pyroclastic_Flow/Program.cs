using Day_17_Pyroclastic_Flow;

string input = File.ReadAllText("../../../input.txt");
Console.WriteLine(Play(input, 2022));
Console.WriteLine(Play(input, 1000000000000));


string Play(string input, long rounds)
{
    int size = input.Length;
    int w = 0;
    Board board = new Board(8000);
    Cycle cycle = new Cycle(size);
    for (int round = 0; round < rounds; round++)
    {
        int position = round % 5;
        int abscissa = 2, ordinate = board.Height + 7;
        for (int i = 0; i < 4; i++)
        {
            ordinate--;
            if (input[w] == '<' && !board.CheckAbscissa(abscissa - 1, position))
                abscissa--;
            if (input[w] == '>' && !board.CheckAbscissa(abscissa + 1, position))
                abscissa++;
            w = (w + 1) % size;
        }
        while (!board.CheckCollisionsOnOrdinates(position, abscissa, ordinate))
        {
            ordinate--;
            if (input[w] == '<' && !board.CheckCollisionsOnAbsissa(position,abscissa - 1, ordinate, board.LeftTab))
                abscissa--;
            if (input[w] == '>' && !board.CheckCollisionsOnAbsissa(position, abscissa + 1, ordinate, board.RightTab))
                abscissa++;
            w = (w + 1) % size;
        }
        board.Drop(position, abscissa, ordinate);
        if (cycle.IsReady(round, rounds, position, w, board.Height)) 
            break;
    }
    return (board.Height + cycle.CycleHeight).ToString();
}
