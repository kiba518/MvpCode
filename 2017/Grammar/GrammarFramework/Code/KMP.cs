using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    public class KMP
    {
        public KMP()
        {
             
        }

        /** 
         * 暴力破解法 
         * @param ts 主串 
         * @param ps 模式串 
         * @return 如果找到，返回在主串中第一个字符出现的下标，否则为-1 
         */

        public static int bf(String ts, String ps)
        { 
            char[] t = ts.ToCharArray(); 
            char[] p = ps.ToCharArray(); 
            int i = 0; // 主串的位置 
            int j = 0; // 模式串的位置 
            while (i < t.Length && j < p.Length)
            { 
                if (t[i] == p[j])
                { 
                    // 当两个字符相同，就比较下一个 
                    i++; 
                    j++; 
                }
                else
                { 
                    i = i - j + 1; // 一旦不匹配，i后退 
                    j = 0; // j归0 
                } 
            } 
            if (j == p.Length)
            { 
                return i - j; 
            }
            else
            { 
                return -1; 
            }

        }

       
        public static int[] getNext(String ps)
        {

            char[] p = ps.ToCharArray();

            int[] next = new int[p.Length];
            
            next[0] = -1;

            int j = 0;

            int k = -1;

            while (j < p.Length - 1)
            {

                if (k == -1 || p[j] == p[k])
                {
                   
                    next[++j] = ++k;
                     
                }
                else
                {

                    k = next[k];

                }

            }

            return next;

        }

        public static int KMP1(String ts, String ps)
        {

            char[] t = ts.ToCharArray();

            char[] p = ps.ToCharArray();

            int i = 0; // 主串的位置

            int j = 0; // 模式串的位置

            //int[] next = getNext(ps);
            int[] next = getNext2(ps);
            while (i < t.Length && j < p.Length)
            {

                if (j == -1 || t[i] == p[j])
                { // 当j为-1时，要移动的是i，当然j也要归0

                    i++;

                    j++;

                }
                else
                {

                    // i不需要回溯了

                    // i = i - j + 1;

                    j = next[j]; // j回到指定位置

                }

            }

            if (j == p.Length)
            {

                return i - j;

            }
            else
            {

                return -1;

            }

        }
        //"ABCDABCD", 
        //"ABCGABC"
        public static int[] getNext2(String ps)
        {

            char[] p = ps.ToCharArray();

            int[] next = new int[p.Length];

            next[0] = -1;

            int j = 0;

            int k = -1;

            while (j < p.Length - 1)
            {

                if (k == -1 || p[j] == p[k])
                {
                    var nextIndex = ++j;
                    var nextValue = ++k;
                    next[nextIndex] = nextValue;
                }
                else
                {

                    k = next[k];

                }

            }

            return next;

        }
    }

     
}

   
 
