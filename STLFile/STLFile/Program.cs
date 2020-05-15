using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace STLFile
{
    class Program
    {
        static string [,]facetandvertex_int= new string[5,5];

        static List<Setfacetandvertex> respmsg = new List<Setfacetandvertex>();

        static int number_vertex = 1;

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
                    //Console.WriteLine(line);
                    decodeSTLtoArray(line);
                    //check_facetandvertex_array()   -> เขียนฟังชั่นนี้ต่อ


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

                try
                {
                    string sJSONResponse = JsonConvert.SerializeObject(respmsg);

                   // Console.WriteLine(sJSONResponse);

                   // System.IO.File.WriteAllText(@"\Users\thaniya\Project", sJSONResponse);  - >>> create JSON File 
                }
                catch
                {
                    Console.WriteLine("can't write");
                }

            }
        }

        


        static void decodeSTLtoArray(String strfromSTL)
        {
            

            if (strfromSTL.Contains("facet normal "))
            {

                if (number_vertex == 4)
                {
                    number_vertex = 1;

                    respmsg.Add(new Setfacetandvertex
                    {
                        facet_x = facetandvertex_int[0, 0],
                        facet_y = facetandvertex_int[0,1],
                        facet_z = facetandvertex_int[0,2],
                        vertex1_x = facetandvertex_int[1,0],
                        vertex1_y = facetandvertex_int[1,1],
                        vertex1_z = facetandvertex_int[1,2],
                        vertex2_x = facetandvertex_int[2,0],
                        vertex2_y = facetandvertex_int[2,1],
                        vertex2_z = facetandvertex_int[2,2],
                        vertex3_x = facetandvertex_int[3,0],
                        vertex3_y = facetandvertex_int[3,1],
                        vertex3_z = facetandvertex_int[3,2]
                    }) ;
                }


                strfromSTL += '#';
                String test = getBetween(strfromSTL, "facet normal ", "#");
                String[] facet_split = test.Split(" ");

             
                
                facetandvertex_int[0,0] = facet_split[0];
                facetandvertex_int[0,1] = facet_split[1];
                facetandvertex_int[0,2] = facet_split[2];



               

            }
            else if(strfromSTL.Contains("vertex "))
            {
                strfromSTL += '#';
                String test = getBetween(strfromSTL, "vertex ", "#");
                String []vertex_split = test.Split(" ");
                
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
