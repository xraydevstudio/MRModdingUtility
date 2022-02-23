using System;
using System.Xml;
using System.Collections.Generic;
using Nito.AsyncEx;

namespace MRModdingUtility
{
    public class Program
    {
        // Path to map file
        const string xmlFile = @"map.r3dmap";
        const string txtFile = @"values.txt";
        private static XmlTools _xmlTools;
        private static TagService _tagService;
        public static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }
        static async void MainAsync(string[] args)
        {
            _xmlTools = new XmlTools();
            _tagService = new TagService(txtFile);

            // Program info
            Console.WriteLine("Motor Rock Modding Utility v0.1\n");
            Console.WriteLine("Enter 'game' or 'editor' to convert map");
            Console.WriteLine("for playing in the game or editing in map editor");
            await _xmlTools.ReplaceTags(docName: xmlFile, _tagService["value1"], _tagService["value2"]);
            Console.WriteLine("\nEnter X value: ");
            var x = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter Y value: ");
            var y = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter Z value: ");
            var z = float.Parse(Console.ReadLine());
            var result = await _xmlTools.UpdateNodesPosition(xmlFile, await _xmlTools.ReadPositionNodes(docName: xmlFile), x, y, z);
            Console.WriteLine("\nUpdated map file succesfully\n");
        }
    }
}