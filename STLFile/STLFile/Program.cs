using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace STLFile
{
    class Program
    {
        

        static void Main(string[] args)
        {
            STL a = new STL();
            a.readSTL("//Users//thaniya//Project//STLFile//STLFile//Test.stl");
            a.details = "blah blah blah";//เพิ่มรายละเอียด
            a.price = 250.04;//ราคา
            // Console.WriteLine(a.JsonSTL); //แสดงJSON STL แบบแยกพิกัด
            a.addJoint("a");//เพิ่มชิ้นส่วน
            a.addJoint("b");
            
            Console.WriteLine(a.joint()[0]); //แสดง joint
            

        }
    }


    

    class STL
    {





        private StreamReader STLFile;
        private static int number_vertex = 1;
        private static string[,] facetandvertex_int = new string[5, 5];
        private static List<Setfacetandvertex> ListSTL = new List<Setfacetandvertex>();
        public string JsonSTL;

        public string details = "";
        public double price;
        public List<string> jointList = new List<string>();


        public void addJoint(String jointname)
        {
            jointList.Add(jointname);
        }

        public string[] joint()
        {
            string[] a = jointList.ToArray();
            
            return a;
        }

        public  void readSTL(String path)
        {
            String Readline;
            try
            {
                STLFile = new StreamReader(path);

                while((Readline = STLFile.ReadLine())!= null)
                {
                    decodeSTLtoArray(Readline);
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("Error at Class STL : can't read file from path");
            }
            finally
            {
                if (STLFile != null)
                {
                    STLFile.Close();
                }

                try
                {
                     JsonSTL = JsonConvert.SerializeObject(ListSTL);

                     //Console.WriteLine(JsonSTL);

                    // System.IO.File.WriteAllText(@"\Users\thaniya\Project", sJSONResponse);  - >>> create JSON File 
                }
                catch
                {
                    Console.WriteLine("Error at Class STL : can't create JSON Object ");
                }

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


        static void decodeSTLtoArray(String strfromSTL)
        {


            if (strfromSTL.Contains("facet normal "))
            {

                if (number_vertex == 4)
                {
                    number_vertex = 1;

                    ListSTL.Add(new Setfacetandvertex
                    {
                        facet_x = facetandvertex_int[0, 0],
                        facet_y = facetandvertex_int[0, 1],
                        facet_z = facetandvertex_int[0, 2],
                        vertex1_x = facetandvertex_int[1, 0],
                        vertex1_y = facetandvertex_int[1, 1],
                        vertex1_z = facetandvertex_int[1, 2],
                        vertex2_x = facetandvertex_int[2, 0],
                        vertex2_y = facetandvertex_int[2, 1],
                        vertex2_z = facetandvertex_int[2, 2],
                        vertex3_x = facetandvertex_int[3, 0],
                        vertex3_y = facetandvertex_int[3, 1],
                        vertex3_z = facetandvertex_int[3, 2]
                    });
                }


                strfromSTL += '#';
                String test = getBetween(strfromSTL, "facet normal ", "#");
                String[] facet_split = test.Split(" ");



                facetandvertex_int[0, 0] = facet_split[0];
                facetandvertex_int[0, 1] = facet_split[1];
                facetandvertex_int[0, 2] = facet_split[2];





            }
            else if (strfromSTL.Contains("vertex "))
            {
                strfromSTL += '#';
                String test = getBetween(strfromSTL, "vertex ", "#");
                String[] vertex_split = test.Split(" ");

                facetandvertex_int[number_vertex, 0] = vertex_split[0];
                facetandvertex_int[number_vertex, 1] = vertex_split[1];
                facetandvertex_int[number_vertex, 2] = vertex_split[2];

                number_vertex++;
            }
        }

        public class SetListFacetandVertex
        {
            public List<Setfacetandvertex> Response { get; set; }
        }

       


        public class Setfacetandvertex
        {
            public string facet_x { get; set; }
            public string facet_y { get; set; }
            public string facet_z { get; set; }
            public string vertex1_x { get; set; }
            public string vertex1_y { get; set; }
            public string vertex1_z { get; set; }
            public string vertex2_x { get; set; }
            public string vertex2_y { get; set; }
            public string vertex2_z { get; set; }
            public string vertex3_x { get; set; }
            public string vertex3_y { get; set; }
            public string vertex3_z { get; set; }


        }

    }

    
        
    
}
