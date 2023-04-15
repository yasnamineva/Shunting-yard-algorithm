namespace ShuntingYardAlgorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating a dictionary with the predecence of the operators
            Dictionary<char, int> operatorsByPredecence = new()
            {
                { '+', 1 },
                { '-', 1 },
                { '*', 2 },
                { '/', 2 },
                { '%', 2 },
                { '^', 3 }
            };

            // creating the operator stack and output queue
            Stack<char> operators = new();
            Queue<char> output = new();

            char[] infix = Console.ReadLine().Where(x => !Char.IsWhiteSpace(x)).ToArray();

            foreach (var symbol in infix)
            {
                if (Char.IsNumber(symbol))      //adding to output queue is the symbol is a number
                {
                    output.Enqueue(symbol);
                }
                else
                {
                    CheckPredecence(symbol, operators, operatorsByPredecence, output);
                }  
            }
            while (operators.Count != 0)
            {
                output.Enqueue(operators.Pop());
            }

            while (output.Count == 0)
            {
                Console.WriteLine(output.Dequeue());
            }
        }

        private static void CheckPredecence(char symbol, Stack<char> operators,
            Dictionary<char, int> operatorsByPredecence, Queue<char> output)
        {
            if (symbol == '(' || operators.Count == 0 || operators.Peek() == '('
                || operatorsByPredecence[symbol] > operatorsByPredecence[operators.Peek()])
            {
                operators.Push(symbol);
            }
            else if (operatorsByPredecence[symbol] <= operatorsByPredecence[operators.Peek()])
            {
                while ( operators.Count != 0 || operatorsByPredecence[symbol] <= operatorsByPredecence[operators.Peek()])
                {
                    output.Enqueue(operators.Pop());
                }
                operators.Push(symbol);
            }
            else if (symbol == ')')
            {
                while (operators.Peek() != '(')
                {
                    output.Enqueue(operators.Pop());
                }
                operators.Pop();     //removing the '('
            }

        }
    }
}