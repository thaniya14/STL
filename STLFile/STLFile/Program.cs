using System;
using System.IO;

namespace STLFile
{
    class Program
    {
        String []informationSTL = new String[500];

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
                fileSTL = new StreamReader("E:\\Project\\STL\\STLFile\\Test.stl");
                while ((line = fileSTL.ReadLine()) != null)
                {
                    Console.WriteLine(line);
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
    }
}
