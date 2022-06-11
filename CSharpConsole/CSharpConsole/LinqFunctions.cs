using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpConsole
{
    public class LinqFunctions
    {
        public class Units
        {
            public string Sno { get; set; }
            public string Model { get; set; }
        }

        public LinqFunctions() { }
        public void RemoveAllFunction()
        {
            List<Units> units = new List<Units>();
            units.Add(new Units { Sno = "AA0001", Model = "WG1633000010" });
            units.Add(new Units { Sno = "AA0002", Model = "WG1633000010" });
            units.Add(new Units { Sno = "AA0003", Model = "WG1634000010" });
            units.Add(new Units { Sno = "AA0004", Model = "WG1635000010" });
            units.Add(new Units { Sno = "AA0005", Model = "WG1635000010" });

            Console.WriteLine();
            foreach (var item in units)
            {
                Console.WriteLine(string.Format("Sno:{0} Model:{1}", item.Sno, item.Model));
            }

            var mailList = new List<Units>();
            mailList.AddRange(units.Where(x => x.Model == "WG1633000010"));

            foreach (var item in mailList)
            {
                units.RemoveAll(x => x.Sno == item.Sno);
            }

            Console.WriteLine();
            foreach (var item in units)
            {
                Console.WriteLine(string.Format("Sno:{0} Model:{1}", item.Sno, item.Model));
            }

            Console.ReadLine();
        }

        public void MSDNRemoveALL()
        {
            List<string> dinosaurs = new List<string>();

            dinosaurs.Add("Compsognathus");
            dinosaurs.Add("Amargasaurus");
            dinosaurs.Add("Oviraptor");
            dinosaurs.Add("Velociraptor");
            dinosaurs.Add("Deinonychus");
            dinosaurs.Add("Dilophosaurus");
            dinosaurs.Add("Gallimimus");
            dinosaurs.Add("Triceratops");

            Console.WriteLine();
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine(dinosaur);
            }

            Console.WriteLine("\nTrueForAll(EndsWithSaurus): {0}",
                dinosaurs.TrueForAll(EndsWithSaurus));

            Console.WriteLine("\nFind(EndsWithSaurus): {0}",
                dinosaurs.Find(EndsWithSaurus));

            Console.WriteLine("\nFindLast(EndsWithSaurus): {0}",
                dinosaurs.FindLast(EndsWithSaurus));

            Console.WriteLine("\nFindAll(EndsWithSaurus):");
            List<string> sublist = dinosaurs.FindAll(EndsWithSaurus);

            foreach (string dinosaur in sublist)
            {
                Console.WriteLine(dinosaur);
            }

            Console.WriteLine(
                "\n{0} elements removed by RemoveAll(EndsWithSaurus).",
                dinosaurs.RemoveAll(EndsWithSaurus));

            Console.WriteLine("\nList now contains:");
            foreach (string dinosaur in dinosaurs)
            {
                Console.WriteLine(dinosaur);
            }

            Console.WriteLine("\nExists(EndsWithSaurus): {0}",
                dinosaurs.Exists(EndsWithSaurus));
            Console.ReadLine();
        }
        // Search predicate returns true if a string ends in "saurus".
        private static bool EndsWithSaurus(String s)
        {
            return s.ToLower().EndsWith("saurus");
        }
        
    }
}
