﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola_App.Models
{
    internal class all_udrzba
    {
        public ObservableCollection<Udrzbari> all_udrzbari { get; set; } = new ObservableCollection<Udrzbari>();

        public all_udrzba() =>
            LoadUdrzbari();

        public void LoadUdrzbari()
        {
            all_udrzbari.Clear(); // Ensure the collection is cleared first

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.ucitele.txt files.
            IEnumerable<Udrzbari> ucitele = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.udrzba.txt")

                                        // Each file name is used to create a new Ucitele object
                                        .Select(filename =>
                                        {
                                            // Read all lines of the file
                                            string[] lines = File.ReadAllLines(filename);

                                            // Create a new Ucitele object with the first and second line
                                            return new Udrzbari()
                                            {
                                                Filename = filename,
                                                Jmeno = lines.Length > 0 ? lines[0] : string.Empty, // First line is the name
                                                Specializace = lines.Length > 1 ? lines[1] : string.Empty  // Second line is the title
                                            };
                                        })

                                        // Order them by name
                                        .OrderBy(ucitel => ucitel.Jmeno);

            // Add each Ucitele into the ObservableCollection
            foreach (Udrzbari ucitel in ucitele)
            {
                all_udrzbari.Add(ucitel);
            }
        }
    }
}
