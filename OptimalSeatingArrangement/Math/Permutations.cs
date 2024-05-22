using OptimalSeatingArrangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalSeatingArrangement.Math
{
    public static class Permutations
    {

        /// <summary>
        /// Gets permutation according to Lehmer code.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();

            // Enumerable.Range generates enumerable values from 0 to length of the guest list.
            // calling Factorial in select statement results in each number in the enumerable range will return a factorial of that number.
            // this is then arrayed so computing with sequence is according to factorial values.
            var factorials = Enumerable.Range(0, array.Length)
                .Select(Factorial)
                .ToArray();

            // a for loop to generate x new permutations of the guest list where x is factorial of number of guests minus 1.
            // Its minus one since simply shuffling all guests one step at the time around the table does not equal a new seating order.
            for (var i = 0L; i < factorials[array.Length -1]; i++)
            {
                // sequence is a list of int where the swapped guests for this permutation is marked.
                // for numbers i up to n!-1. According to Lehmer Code this guarantees we will hit all permutations and every permutation will only be hit once.
                var sequence = GenerateSequence(i, array.Length - 1, factorials);

                // yield returns enumerable and will remember where we left of. this will fill whatever object we choose with all permutations in order.
                yield return GeneratePermutation(array, sequence);
            }
        }

        /// <summary>
        /// Sequence with factoradic numbers that has a close connection to permutations.
        /// number is the current permutation, and will be the only thing that changes between permutations when calling this method.
        /// First loop sequence is an array of zeroes, nothing changes.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        /// <param name="factorials"></param>
        /// <returns></returns>
        private static int[] GenerateSequence(long number, int size, IReadOnlyList<long> factorials)
        {
            var sequence = new int[size];

            for (var j = 0; j < sequence.Length; j++)
            {
                var facto = factorials[sequence.Length - j];

                sequence[j] = (int)(number / facto); // number / facto = 0 for number < facto. if on 4th permutation(number = 4) sequence[j] =2. the swap that happens later will then swap third element with fifth element.
                number = (int)(number % facto); // number % facto will return 0 when number == facto. Uneven numbers between factorial values that are larger than facto but not evenly dividable will enable sequences > 0 with smaller factorial divisions. more for loops = smaller factorial numbers.
            }

            return sequence;
        }

        private static IEnumerable<T> GeneratePermutation<T>(T[] array, IReadOnlyList<int> sequence)
        {
            var clone = (T[])array.Clone();

            for (int i = 0; i < clone.Length - 1; i++)
            {
                Swap(ref clone[i], ref clone[i + sequence[i]]); // Takes two values. the original and the original with sequence values to shift.
            }

            return clone;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        //Standard method to calculate factorial of number n. if n = 7 then n! = 7*6*5*4*3*2*1 = 5040
        private static long Factorial(int n)
        {
            if (n == 0)
                return n;

            long nFactorial = n;

            for(int i = 1; i < n; i++)
            {
                nFactorial = nFactorial * i;
            }

            return nFactorial;
        }
    }
}
