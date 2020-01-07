using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Izh_04
{
    public class Izh04BasicCoding
    {
        public static void Main(string[] args)
        {
            try
            {
                int firstNumber = 8;
                int secondNumber = 14;
                int startIndex = 3;
                int endIndex = 8;
                int resultNumber;
                resultNumber = InsertNumber(firstNumber, secondNumber, startIndex, endIndex);
                Console.WriteLine("Результирующее число: " + resultNumber);

                string firstString = "AsdfsgdAd";
                string secondString = "Aesssaqeeq";
                string stringConcatenation;
                stringConcatenation = StringConcatenation(firstString, secondString);
                Console.WriteLine("\nРезультат конкатенации строк: " + stringConcatenation);

                int indexOfEqualLeftAndRightSides;
                double[] arrayForFinding = new double[] { 1, 2, 3, 4, 3, 2, 1 };
                indexOfEqualLeftAndRightSides = FindingElementIndexBetweenEqualSums(arrayForFinding);
                Console.WriteLine("\nИндекс элемента, от которого сумма чисел слева равна сумме чисел справа: " + indexOfEqualLeftAndRightSides);

                int indexOfMaxElementFinding;
                int[] intArrayFindingMax = new int[] { 0, -1, 11, 21, 1, 2, 8, 65, 34, 21, 765, -12, 566, 7878, -199, 0, 34, 65, 87, 12, 34 };
                indexOfMaxElementFinding = MaxElementFinding(intArrayFindingMax);
                Console.WriteLine("\nМаксимальный элемент в неотсортированном целочисленном массиве: " + indexOfMaxElementFinding);

                int[] filteredIntArray;
                int numberForFiltering = 9;
                int[] arrayForFiltering = new int[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, -69, 70, 15, 17 };
                filteredIntArray = DigitFiltering(arrayForFiltering, numberForFiltering);
                Console.Write("\nОтфильтрованный массив:");
                foreach (int number in filteredIntArray)
                    Console.Write(" " + number);
                Console.WriteLine();

                int sourceNumber = 3456432;
                FindNextBiggerNumber(sourceNumber, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Даны два целых знаковых четырех байтовых числа и две позиции битов i и j (i меньше j).
        /// Реализовать алгоритм InsertNumber вставки битов с j-ого по i-ый бит одного числа в другое так,
        /// чтобы биты второго числа занимали позиции с бита j по бит i (биты нумеруются справа налево).
        /// С помощью (secondNumber & int.MinValue) восстанавливаем знаковый бит.
        /// </summary>
        /// <param name="firstNumber">Первое число</param>
        /// <param name="secondNumber">Второе число</param>
        /// <param name="startIndex">Начальная позиция диапазона битов для переноса (считается с 0)</param>
        /// <param name="endIndex">Конечная позиция диапазона битов для переноса (считается с 0)</param>
        /// <returns>Результирующее число</returns>
        public static int InsertNumber(int firstNumber, int secondNumber, int startIndex, int endIndex)
        {
            if ((startIndex < 0) || (endIndex < 0) || (startIndex > 31) || (endIndex > 31))
                throw new ArgumentOutOfRangeException();
            if (startIndex > endIndex)
                throw new ArgumentException();

            BitVector32 firstNumberBits = new BitVector32(firstNumber);
            BitVector32 secondNumberBits = new BitVector32(secondNumber);
            int i = 1 << startIndex;
            int j = 1;

            for (int k = startIndex; k <= endIndex; ++k)
            {
                firstNumberBits[i] = secondNumberBits[j];
                i <<= 1;
                j <<= 1;
            }

            return firstNumberBits.Data;
        }

        /// <summary>
        /// Реализовать алгоритм конкатенации двух строк,
        /// содержащих только символы латинского алфавита,
        /// исключая повторяющиеся символы.
        /// </summary>
        /// <param name="firstString">Первая строка</param>
        /// <param name="secondString">Вторая строка</param>
        /// <returns>Строка после конкатенации</returns>
        public static string StringConcatenation(string firstString, string secondString)
        {
            string concatinatedString = firstString;

            for (int i = 0; i < secondString.Length; i++)
                if (firstString.IndexOf(secondString[i]) == -1)
                    concatinatedString += secondString[i];

            return concatinatedString;
        }

        /// <summary>
        /// Реализовать алгоритм поиска в вещественном массиве индекса элемента,
        /// для которого сумма элементов слева и сумма элементов спарава равны.
        /// Если такого элемента не существует вернуть null (или -1).
        /// </summary>
        /// <param name="doubleArray">Массив вещественных чисел</param>
        /// <returns>Индекс элемента или -1, если элемент не найден</returns>
        public static int FindingElementIndexBetweenEqualSums(double[] doubleArray)
        {
            int doubleArrayLength = doubleArray.Length;
            decimal[] arrayOfSumsFromLeftToRight = new decimal[doubleArrayLength]; // decimal - чтобы исключить
            decimal[] arrayOfSumsFromRightToLeft = new decimal[doubleArrayLength]; // машинный эпсилон

            if (doubleArrayLength > 2)
            {
                arrayOfSumsFromLeftToRight[0] = (decimal)doubleArray[0];
                for (int i = 1; i < doubleArrayLength - 1; i++)
                    arrayOfSumsFromLeftToRight[i] = (decimal)doubleArray[i] + arrayOfSumsFromLeftToRight[i - 1];

                arrayOfSumsFromRightToLeft[doubleArrayLength - 1] = (decimal)doubleArray[doubleArrayLength - 1];
                for (int i = doubleArrayLength - 2; i > 1; i--)
                {
                    if (arrayOfSumsFromLeftToRight[i - 1] == arrayOfSumsFromRightToLeft[i + 1])
                        return i;

                    arrayOfSumsFromRightToLeft[i] = (decimal)doubleArray[i] + arrayOfSumsFromRightToLeft[i + 1];
                }
            }

            return -1;
        }

        /// <summary>
        /// Реализовать рекурсивный алгоритм поиска максимального элемента в неотсортированном целочисленом массиве.
        /// </summary>
        /// <param name="intArray">Входной массив целых чисел</param>
        /// <returns>Максимальный элемент в массиве</returns>
        public static int MaxElementFinding(int[] intArray)
        {
            int maxNumber = -1;

            if (intArray.Length > 0)
            {
                int i = intArray.Length - 1;
                maxNumber = Maximum(intArray, i, intArray[intArray.Length - 1]);
            }

            return maxNumber;
        }

        private static int Maximum(int[] intArray, int i, int tempMaxNumber)
        {
            if (i > 0)
                return Maximum(intArray, i - 1, Math.Max(intArray[i - 1], tempMaxNumber));

            return tempMaxNumber;
        }

        /// <summary>
        /// Реализовать метод FilterDigit, который принимает массив целых чисел и фильтрует его таким образом,
        /// чтобы на выходе остались только числа, содержащие заданную цифру (LINQ-запросы не использовать!).
        /// </summary>
        /// <param name="sourceArray">Исходный массив</param>
        /// <param name="filteringNumber">Заданная цифра для фильрации</param>
        /// <returns>Отфильтрованный массив</returns>
        public static int[] DigitFiltering(int[] sourceArray, int filteringNumber)
        {
            List<int> filteredList = new List<int>();
            string tempFilter = filteringNumber.ToString();

            if (sourceArray.Length > 0)
                foreach (int elem in sourceArray)
                    if (elem.ToString().Contains(tempFilter))
                        filteredList.Add(elem);
            
            return filteredList.ToArray();
        }

        /// <summary>
        /// Реализовать метод FindNextBiggerNumber, который принимает положительное целое число
        /// и возвращает ближайшее наибольшее целое, состоящее из цифр исходного числа, и null (или -1),
        /// если такого числа не существует.
        /// Добавить к методу FindNextBiggerNumber возможность вернуть время нахождения заданного числа,
        /// рассмотрев различные языковые возможности.
        /// </summary>
        /// <param name="sourceNumber">Исходное число</param>
        /// <returns>Ближайшее наибольшее целое число</returns>
        public static int FindNextBiggerNumber(int sourceNumber, bool showExecutionTime)
        {
            if (sourceNumber < 0)
                throw new ArgumentOutOfRangeException();

            TimeSpan startTime = DateTime.Now.TimeOfDay;
            int nextBiggerNumber = NextBiggerNumber(sourceNumber);
            TimeSpan endTime = DateTime.Now.TimeOfDay;

            if (showExecutionTime)
            {
                string message = "Поиск числа длился: ";
                TimeSpan howLong = endTime - startTime;
                ShowTimeAndMessage(howLong, message);
            }

            return nextBiggerNumber;
        }

        private static int NextBiggerNumber(int sourceNumber)
        {
            int[] buffer = new int[sourceNumber.ToString().Length];
            for (int i = buffer.Length - 1; i > -1; --i)
            {
                buffer[i] = sourceNumber % 10;
                sourceNumber /= 10;
            }
            int index = IndexSearching(buffer);

            if (index == -1)
                return -1;

            if (index < buffer.Length - 1)
            {
                int temp = buffer[index];
                buffer[index] = buffer[index + 1];
                buffer[index + 1] = temp;
                Array.Sort(buffer, index + 1, buffer.Length - index - 1);
            }

            int result = 0;
            int bufferLength = buffer.Length;
            for (int i = 0; i < bufferLength; ++i)
                result += (int)(buffer[i] * Math.Pow(10, bufferLength - i - 1));

            return result;
        }

        private static int IndexSearching(int[] temp)
        {
            for (int i = temp.Length - 1; i > 0; --i)
                if (temp[i] > temp[i - 1])
                    return i - 1;

            return -1;
        }

        private static void ShowTimeAndMessage(TimeSpan time, string message)
        {
            Console.WriteLine($"\n{message}{time}\n");
        }
    }
}
