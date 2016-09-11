using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    class Program
    {
        static readonly char CODE_BLANK = '◇';
        static readonly char CODE_VALID = '◆';
        static readonly char CODE_MUST_VALID = '■';
        static readonly char CODE_MUST_BLANK = '□';
        static string getN(char a, int n)
        {
            if (n == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                sb.Append(a);
            }
            return sb.ToString();
        }
        static List<string> results = new List<string>();
        static void display(List<int> have)
        {
            int mmm = 0;
            int k = 0;
            int sum = 0;
            StringBuilder sb = new StringBuilder();
            while (k < have.Count)
            {
                mmm++;
                if (mmm > 10)
                {
                    break;
                }
                sb.Append(getN(CODE_BLANK, have[k] - sum));
                sb.Append(getN(CODE_VALID, preWill[k]));
                sum = have[k] + preWill[k];
                k++;
            }
            if (sum < max + 1)
            {
                sb.Append(getN(CODE_BLANK, max + 1 - sum));
            }
            //Console.WriteLine(sb.ToString());
            results.Add(sb.ToString());
        }
        static void showSame()
        {
            int length = results.Count;
            char[][] arr = new char[length][];

            for (int i = 0; i < length; i++)
            {
                arr[i] = results[i].ToCharArray();
            }
            
            for (int i = 0; i < max + 1; i++)
            {
                //遍历第i列
                bool same = true;
                for (int j = 0; j < length; j++)
                {
                    if(arr[j][i] != CODE_VALID)
                    {
                        same = false;
                    }
                }
                if(same)
                {
                    for (int j = 0; j < length; j++)
                    {
                        arr[j][i] = CODE_MUST_VALID;
                    }
                }
            }

            for (int i = 0; i < max + 1; i++)
            {
                //遍历第i列
                bool same = true;
                for (int j = 0; j < length; j++)
                {
                    if (arr[j][i] != CODE_BLANK)
                    {
                        same = false;
                    }
                }
                if (same)
                {
                    for (int j = 0; j < length; j++)
                    {
                        arr[j][i] = CODE_MUST_BLANK;
                    }
                }
            }

            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(arr[i]);
            }


        }
                
        static void f(int from, int to, List<int> have, List<int> will)
        {
            //Console.WriteLine("from:" + from + "to:" + to + "have:" + have.Count + "will:" + will.Count);
            if (will.Count == 0)
            {
                if (have.Count == preWill.Count)
                {
                    display(have);
                }
                return;
            }
            List<int> reverseWill = new List<int>(will);
            reverseWill.Reverse();
            int length = reverseWill.Count;
            int sum = 0;
            for (int i = 0; i < length - 1; i++)
            {
                sum = sum + reverseWill[i] + 1;
            }
            int n = will[0];
            will.RemoveAt(0);
            for (int i = from; i <= to - sum - n + 1; i++)
            {
                List<int> had = new List<int>(have);
                List<int> would = new List<int>(will);
                had.Add(i);
                f(i + n + 1, to, had, would);
            }
        }
        static int allCount = 0;
        static List<int> preWill;
        static int max = 9;
        static void Main(string[] args)
        {
            List<int> have = new List<int>();
            List<int> will = new List<int>();
            if (args.Length == 0)
            {
                will.Add(2);
                will.Add(2);
                will.Add(3);
                will.Add(4);
                max = 14;
                preWill = new List<int>(will);
            }
            else if (args.Length == 2)
            {
                string[] ns = args[0].Split(',');
                int length = ns.Length;
                for (int i = 0; i < length; i++)
                {
                    will.Add(int.Parse(ns[i]));
                }
                allCount = will.Count;
                max = int.Parse(args[1]) - 1;
                preWill = new List<int>(will);
            }
            f(0, max, have, will);
            showSame();
            Console.ReadLine();
        }
    }
}
