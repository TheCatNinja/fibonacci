// Fubonacci number

Console.WriteLine("\nFibonacci number.\n" +
    "Enter [1] if you'd like to enter the number from the console.\n" +
    "Enter [2] if you'd like to read the number from a file.\n");

Console.WriteLine("Enter an option");
var option = Console.ReadLine();

while (option != "1" && option != "2")
{
    Console.WriteLine($"Option {option} is not recognized.\n" +
        $"Please, try again!");
    option = Console.ReadLine();
}

if(option == "1")
{
    FibNumConsole();
}
if(option == "2")
{
    FibNumFile();
}


// calculate Fibonacci num for a num input in the console by the user
// also checks if the num is valid
static void FibNumConsole()
{
    bool cont = false;
    string number = "";
    int numberInt = 0;
    while (cont == false)
    {
        Console.WriteLine("Enter an integer:");
        number = Console.ReadLine();
        if (number == "")
        {
            Console.WriteLine("\nThe entered value is null!\n" +
                "Please, try again.\n");
        }
        else if (!int.TryParse(number, out numberInt))
        {
            Console.WriteLine($"\nThe entered value ({number}) couldn't be converted to int.\n" +
                "You must enter a valid integer. " +
                "Please, try again.\n");
        }
        else
        {
            cont = true;
        }
    }

    Console.WriteLine($"\nThe Fibonacci number for {number} is {Fibonacci(numberInt)}.");
    PerformAnother();
}


// calculate Fibonacci num for a num from a file
static void FibNumFile()
{
    var filePath = GetAndCheckFile();
    int number = CheckNumberFromFile(filePath);
    Console.WriteLine($"\nThe Fibonacci number for {number} is {Fibonacci(number)}.");
    PerformAnother();
}


// used in FibNum: Console & File
// calculates the Fibonacci number
static int Fibonacci(int number)
{
    if (number == 0)
    {
        return 0;
    }
    else if (number == 1 || number == 2)
    {
        return 1;
    }
    else
    {
        var help = 0;
        var last = 1; //F1
        var result = 1; //F2
        for (int i = 0; i < number - 2; i++)
        {
            help = last;
            last = result;
            result += help;
        }
        return result;
    }
}


// used to boot the program again
static void PerformAnother()
{
    Console.WriteLine("Would you like to try again? " +
        "(Yes - enter [1] No - enter anything else)");
    string option = Console.ReadLine();
    if (option == "1")
    {
        System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\Fibonacci.exe");
        Environment.Exit(0);
    }
    else
    {
        Environment.Exit(0);
    }
}


// used in FibNumFile
// returns a valid number
static int CheckNumberFromFile(string filePath)
{
    bool cont = false;
    string number = File.ReadAllText(filePath);
    int numberInt = 0;
    while (cont == false)
    {
        if (!int.TryParse(number, out numberInt))
        {
            Console.WriteLine($"\nThe value ({number}) couldn't be converted to int.\n" +
                "Please enter another file path.\n");
            GetAndCheckFile();
        }
        else
        {
            cont = true;
        }
    }
    return numberInt;
}


// used in CheckNumberFromFile
// gets file path from user
// returns a file path to a file which:
// 1) exists 2) isn't empty 3) has a single line
static string GetAndCheckFile()
{
    Console.WriteLine("Please, enter the path for the file:");
    var filePath = Console.ReadLine();

    filePath = CheckIfFileExists(filePath);

    string[] array = System.IO.File.ReadAllLines(filePath);
    int cont = -1;
    while(cont == -1)
    {
        if(array.Length == 0)
        {
            Console.WriteLine("\nThe file is empty.\n" +
                "Please enter another file path.\n");
        }
        if(array.Length > 1)
        {
            Console.WriteLine("\nThe file contains more than 1 line.\n" +
                "This application can only take one number.\n" +
                "Please enter another file path.\n");
        }
        filePath = Console.ReadLine();
        CheckIfFileExists(filePath);
        array = File.ReadAllLines(filePath);
    }
    return filePath;
}


// used in GetAndCheckFile
// returns a file path to an existing file
static string CheckIfFileExists(string filePath)
{
    while (!File.Exists(filePath))
    {
        Console.WriteLine("File not found!\n" +
            "Please, try to enter the path again:");
        filePath = Console.ReadLine();
    }
    return filePath;
}

Console.ReadLine();