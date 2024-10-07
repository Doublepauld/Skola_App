using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola_App.Models
{
    internal class AllPodpora
    {

        public ObservableCollection<Podpora> AllPodpora1 { get; set; } = new ObservableCollection<Podpora>();

        public AllPodpora() =>
            LoadPodpora();

        public void LoadPodpora()
        {
            AllPodpora1.Clear(); // Ensure the collection is cleared first

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.ucitele.txt files.
            IEnumerable<Podpora> ucitele = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.podpora.txt")

                                        // Each file name is used to create a new Ucitele object
                                        .Select(filename =>
                                        {
                                            // Read all lines of the file
                                            string[] lines = File.ReadAllLines(filename);

                                            // Create a new Ucitele object with the first and second line
                                            return new Podpora()
                                            {
                                                Filename = filename,
                                                Jmeno = lines.Length > 0 ? lines[0] : string.Empty, // First line is the name
                                                Titul = lines.Length > 1 ? lines[1] : string.Empty  // Second line is the title
                                            };
                                        })

                                        // Order them by name
                                        .OrderBy(ucitel => ucitel.Jmeno);

            // Add each Ucitele into the ObservableCollection
            foreach (Podpora ucitel in ucitele)
            {
                AllPodpora1.Add(ucitel);
            }
        }

    }
}
