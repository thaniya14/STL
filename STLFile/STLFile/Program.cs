using System;
using System.IO;

namespace STLFile
{
    class Program
    {
        static string [][][]facetandvertex_int= new string[5000][][];

        
        static int number_facet = 0;

        static void Main(string[] args)
        {
            readSTLFile();
        }

        static void readSTLFile() 
        {
            StreamReader fileSTL = null;
            String line;

            try
            {
                fileSTL = new StreamReader("//Users//thaniya//Project//STLFile//STLFile//Test.stl"); //path in macbook
                while ((line = fileSTL.ReadLine()) != null)
                {
                    // Console.WriteLine(line);
                    decodeSTLtoArray(line);
                }

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error");
            }

            finally
            {
                if (fileSTL != null)
                {
                    fileSTL.Close();
                }
            }
        }

        static void decodeSTLtoArray(String strfromSTL)
        {
            if(strfromSTL.Contains("facet normal "))
            {
                number_facet++;
                strfromSTL += '#';
                String test = getBetween(strfromSTL, "facet normal ", "#");
                String[] facet_splite = test.Split(' ');

                facetandvertex_int[number_facet][0][0] = facet_splite[0];
                facetandvertex_int[number_facet][0][1] = facet_splite[1];
                facetandvertex_int[number_facet][0][2] = facet_splite[2];

                //Console.WriteLine(facet_splite[2]);

            }
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

    }
}
