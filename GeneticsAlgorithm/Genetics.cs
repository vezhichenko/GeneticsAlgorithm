using System.Collections;
using System.Collections.Generic;

public class Genetics
{
    //constants
    public const int POPULATION_SIZE  = 50;
    public const int CHROMOSOME_SIZE  = 100;
    public const int TOURNAMENT_SIZE  = 3;
    public const double CROSS_PROB    = 0.2;
    public const double MUTATION_PROB = 0.05;
    public const int CROSS_POINTS     = 5;
    public const int GEN_MINIMUM      = int.MinValue;
    public const int GEN_MAXIMUM      = int.MaxValue;
    //variables
    public static int[][]? population;
    //генератор популяции, генерит полностью случайных
    //микрочелов, ограничен по максимуму и минимуму
    public static void GeneratePopulation()
    {
        var Rand = new Random();
        population = new int[POPULATION_SIZE][];
        for (int i = 0; i < POPULATION_SIZE; i++)
        {
            population[i] = new int[CHROMOSOME_SIZE];
            for (int j = 0; j < CHROMOSOME_SIZE; j++)
            {
                population[i][j] = Rand.Next(GEN_MINIMUM, GEN_MAXIMUM);
            }
        }
    }
    //вывод популяции в консоль
    public static void ShowPopu()
    {
        for (int i = 0; i < POPULATION_SIZE; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < CHROMOSOME_SIZE; j++)
            {
                Console.Write(population[i][j] + " ");
            }
        }
    }
    //целевая функция
    public static int F(int x)
    {
        return (int)Math.Abs(Math.Sin(x));
    }
    //поиск отклонений
    public static int Difference(int[] x)
    {
        int err = 0;
        for (int i = 0; i < CHROMOSOME_SIZE; i++)
        {
            err += (int) Math.Pow(F(i) - x[i], 2);
        }
        return err;
    }
    //общее отклонение
    public static int Difference(int[][] x)
    {
        int err = 0;
        for (int i = 0; i < POPULATION_SIZE; i++)
        {
            err += Difference(x[i]);
        }
        return err;
    }
    //кроссинговер
    public static int[][] Cross(int[] a, int[] b)
    {
        int[][] childs = new int[2][];
        Stack<int> cross_borders = new Stack<int>();
        var Rand = new Random();
        for (int i = 0; i < CROSS_POINTS; i++)
        {
            cross_borders.Push(Rand.Next(1, CHROMOSOME_SIZE - 1));
        }
        childs[0] = childs[1] = new int[CHROMOSOME_SIZE];
        int border_1 = cross_borders.Pop();
        int border_2 = cross_borders.Pop();
        for (int i = 0; i < CHROMOSOME_SIZE; i++)
        {
            if (i >= border_2)
            {
                border_1 = border_2;
                border_2 = cross_borders.Pop();            }
            if (i < border_1)
            {
                childs[0][i] = a[i];
                childs[1][i] = b[i];
            } else
            {
                childs[0][i] = b[i];
                childs[1][i] = a[i];
            }
        }
        return childs;
    }
    //мутация
    public static int[] Mut(int[] a)
    {
        var Rand = new Random();
        int i = Rand.Next(CHROMOSOME_SIZE);
        a[i] += Rand.Next(GEN_MINIMUM + Math.Abs(a[i]), GEN_MAXIMUM - Math.Abs(a[i]));
        return a;
    }
    public static void Main()
    {
        GeneratePopulation();
        ShowPopu();
    }
}

