// See https://aka.ms/new-console-template for more information

int asd = 1;
var sudoku = new int[,]
    {
        { 7,8,0,4,0,0,1,2,0 },
        { 6,0,0,0,7,5,0,0,9 },
        { 0,0,0,6,0,1,0,7,8 },
        { 0,0,7,0,4,0,2,6,0 },
        { 0,0,1,0,5,0,9,3,0 },
        { 9,0,4,0,6,0,0,0,5 },
        { 0,7,0,3,0,0,0,1,2 },
        { 1,2,0,0,0,7,4,0,0 },
        { 0,4,9,2,0,6,0,0,7 }
    };


DrawBoard(sudoku);
Solve(ref sudoku);
Console.SetCursorPosition(0, 12);

void DrawBoard (int[,] board)
{
    for (int i = 0; i < 9; i++)
    {
        if (i % 3 == 0 && i != 0) 
            Console.WriteLine("- - - - - - - - - - - - - ");

        for (int j = 0; j < 9; j++)
        {
            if (j % 3 == 0 && j != 0) 
                Console.Write("| ");

            Console.Write(board[i, j].ToString() + " ");
        }

        Console.WriteLine() ;
    }
}

bool Solve(ref int[,] board)
{
    
    Tuple<int, int> cell = Find_empty(board);
    
    int x, y;

    if (cell == null)  return true;
    else
    {
        y = cell.Item1;
        x = cell.Item2;
    }

    for (int i = 1; i < 10; i++)
    {
        boardUpdate(i, cell);
        if (validation(board, i, cell))
        {
            board[y, x] = i;
            

            if (Solve(ref board)) return true;

            board[y, x] = 0;
            boardUpdate(i, cell);
        }
    }
    boardUpdate(0, cell);
    return false;
}

void boardUpdate(int no, Tuple<int, int> cell)
{
    Thread.Sleep(50);
    int x = cell.Item2;
    int y = cell.Item1;

    if (x >= 6) x += 2;
    else if(x >= 3) x += 1;

    if (y >= 6) y += 2;
    else if (y >= 3) y += 1;

    x *= 2;

    Console.SetCursorPosition(x, y);
    Console.Write(no);
}

bool validation(int[,] board, int number, Tuple<int, int> cell)
{
    //cell is y,x
    // y is vertical
    // x is horizontal

    int y = cell.Item1;
    int x = cell.Item2;

    //Row valid
    for (int i = 0; i < 9; i++)
    {
        if (board[y, i] == number && x != i) return false;
    }

    //Column valid
    for (int i = 0; i < 9; i++)
    {
        if (board[i, x] == number && y != i) return false;
    }

    //Box valid
    int box_x = x / 3;
    int box_y = y / 3;

    for (int i = box_y*3; i < box_y*3+3; i++)
    {
        for (int j = box_x*3; j < box_x*3+3; j++)
        {
            if(board[i,j] == number && new Tuple<int,int>(i,j) != cell) return false;
        }
    }

    return true;


}

Tuple<int,int> Find_empty(int [,] board)
{
    for (int i = 0; i < 9; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            if (board[i, j] == 0) return new Tuple<int, int>(i, j);
        }
    }

    return null;
}

void Solved(int[,] board)
{
    for (int i = 0; i < board.GetLength(1); i++)
    {
        if (board[0, i] != 0) continue;

        for (int x = 1; x <= 9; x++)
        {
            if (CanPut(board, 0, x))
            {
                board[0, i] = x;
                break;
            }
        }

    }

    DrawBoard(board);
}

bool CanPut(int[,] board, int rowNo, int index )
{
    int[] row = board.GetRow(rowNo);

    if (row.Contains(index)) return false;
    return true;
}

class ConsoleSpinner
{
    int counter;

    public void Turn()
    {
        counter++;
        switch (counter % 4)
        {
            case 0: Console.Write("/"); counter = 0; break;
            case 1: Console.Write("-"); break;
            case 2: Console.Write("\\"); break;
            case 3: Console.Write("|"); break;
        }
        Thread.Sleep(100);
        Console.SetCursorPosition(Console.CursorLeft - 1, 2);
    }
}




static class MatrixExtensions
{
    /// <summary>
    /// Returns the row with number 'row' of this matrix as a 1D-Array.
    /// </summary>
    public static T[] GetRow<T>(this T[,] matrix, int row)
    {
        var rowLength = matrix.GetLength(1);
        var rowVector = new T[rowLength];

        for (var i = 0; i < rowLength; i++)
            rowVector[i] = matrix[row, i];

        return rowVector;
    }



    /// <summary>
    /// Sets the row with number 'row' of this 2D-matrix to the parameter 'rowVector'.
    /// </summary>
    public static void SetRow<T>(this T[,] matrix, int row, T[] rowVector)
    {
        var rowLength = matrix.GetLength(1);

        for (var i = 0; i < rowLength; i++)
            matrix[row, i] = rowVector[i];
    }



    /// <summary>
    /// Returns the column with number 'col' of this matrix as a 1D-Array.
    /// </summary>
    public static T[] GetCol<T>(this T[,] matrix, int col)
    {
        var colLength = matrix.GetLength(0);
        var colVector = new T[colLength];

        for (var i = 0; i < colLength; i++)
            colVector[i] = matrix[i, col];

        return colVector;
    }



    /// <summary>
    /// Sets the column with number 'col' of this 2D-matrix to the parameter 'colVector'.
    /// </summary>
    public static void SetCol<T>(this T[,] matrix, int col, T[] colVector)
    {
        var colLength = matrix.GetLength(0);

        for (var i = 0; i < colLength; i++)
            matrix[i, col] = colVector[i];
    }
}