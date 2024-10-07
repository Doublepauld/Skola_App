using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola_App.Models
{
    internal class AllUcitele
    {
        public ObservableCollection<Ucitele> AllUcitele1 { get; set; } = new ObservableCollection<Ucitele>();

        public AllUcitele() =>
            LoadNotes();

        public void LoadNotes()
        {
            AllUcitele1.Clear(); // Ensure the collection is cleared first

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.ucitele.txt files.
            IEnumerable<Ucitele> ucitele = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.ucitele.txt")

                                        // Each file name is used to create a new Ucitele object
                                        .Select(filename =>
                                        {
                                            // Read all lines of the file
                                            string[] lines = File.ReadAllLines(filename);

                                            // Create a new Ucitele object with the first and second line
                                            return new Ucitele()
                                            {
                                                Filename = filename,
                                                Jmeno = lines.Length > 0 ? lines[0] : string.Empty, // First line is the name
                                                Titul = lines.Length > 1 ? lines[1] : string.Empty  // Second line is the title
                                            };
                                        })

                                        // Order them by name
                                        .OrderBy(ucitel => ucitel.Jmeno);

            // Add each Ucitele into the ObservableCollection
            foreach (Ucitele ucitel in ucitele)
            {
                AllUcitele1.Add(ucitel);
            }
        }
    }
}
