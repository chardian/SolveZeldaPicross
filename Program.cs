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
                    if (arr[j][i] != CODE_VALID)
                    {
                        same = false;
                    }
                }
                if (same)
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
        static bool checkObeyKnown(List<int> have, List<int> knownOK, List<int> knownNO)
        {
            int k = 0;
            int sum = 0;
            StringBuilder sb = new StringBuilder();
            while (k < have.Count)
            {
                while (sum < have[k])
                {
                    //sum都是空的
                    if (knownOK.Contains(sum))
                    {
                        return false;
                    }
                    sum++;
                }
                while (sum < have[k] + preWill[k])
                {
                    //sum都是实的
                    if (knownNO.Contains(sum))
                    {
                        return false;
                    }
                    sum++;
                }
                k++;
            }
            while (sum <= max)
            {
                if (knownOK.Contains(sum))
                {
                    return false;
                }
                sum++;
            }
            return true;
        }
        static void f(int from, int to, List<int> have, List<int> will, List<int> knownOK, List<int> knownNO)
        {
            //Console.WriteLine("from:" + from + "to:" + to + "have:" + have.Count + "will:" + will.Count);
            if (will.Count == 0)
            {
                if (have.Count == preWill.Count)
                {
                    if (checkObeyKnown(have, knownOK, knownNO))
                    {
                        display(have);
                    }
                    else
                    {
                        return;
                    }
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
                f(i + n + 1, to, had, would, knownOK, knownNO);
            }
        }
        static int allCount = 0;
        static List<int> preWill;
        static int max = 9;
        static void Main(string[] args)
        {
            List<int> have = new List<int>();
            List<int> will = new List<int>();
            List<int> knownOK = new List<int>();
            List<int> knownNO = new List<int>();
            /*if (args.Length == 0)
            {
                will.Add(1);
                will.Add(1);
                will.Add(1);
                max = 9;
                preWill = new List<int>(will);
                knownOK.Add(8);
                knownNO.Add(2);
                knownNO.Add(3);
                knownNO.Add(5);
                knownNO.Add(7);
                knownNO.Add(9);
            }*/
            string[] ns;
            int length;
            if(args.Length == 0)
            {
                Console.WriteLine("Program.exe [queue to insert] [max length] [(optional) valid cube you know] [(optional)invalid cube you know]");
                Console.WriteLine("Program.exe 1,2,3,1 10 8,9 2");
				return;
            }

            if (args.Length >= 2)
            {
                ns = args[0].Split(',');
                length = ns.Length;
                for (int i = 0; i < length; i++)
                {
                    will.Add(int.Parse(ns[i]));
                }
                allCount = will.Count;
                max = int.Parse(args[1]) - 1;
                preWill = new List<int>(will);
            }
            if (args.Length >= 3)
            {
                ns = args[2].Split(',');
                length = ns.Length;
                for (int i = 0; i < length; i++)
                {
                    knownOK.Add(int.Parse(ns[i]) - 1);
                }
            }
            if (args.Length >= 4)
            {
                ns = args[3].Split(',');
                length = ns.Length;
                for (int i = 0; i < length; i++)
                {
                    knownNO.Add(int.Parse(ns[i]) - 1);
                }
            }

            f(0, max, have, will, knownOK, knownNO);
            showSame();
        }

    }
}
