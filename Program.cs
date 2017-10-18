using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            IEnumerable<Artist> vernNumer = Artists.Where(art => {return art.Hometown == "Mount Vernon";});
            foreach(Artist vern in vernNumer)
            {
                System.Console.WriteLine("Name: {0}, Age: {1}", vern.ArtistName, vern.Age);
            }

            //Who is the youngest artist in our collection of artists?
            List<Artist> ByAge = Artists.OrderBy(art => art.Age).ToList();
            System.Console.WriteLine("Name: {0}, Age: {1}", ByAge[0].ArtistName, ByAge[0].Age);

            //Display all artists with 'William' somewhere in their real name
            List<Artist> Williams = Artists.Where(x => x.RealName.Contains("William")).ToList();
            foreach (Artist will in Williams)
            {
                System.Console.WriteLine("Name: {0}, Age: {1}", will.ArtistName, will.Age);
            }


            //Display the 3 oldest artist from Atlanta\
            List<Artist> Atlantans = Artists.Where(x => x.Hometown == "Atlanta").OrderByDescending(x => x.Age).ToList();
            for(int i = 0; i < 3; i++)
            {
                System.Console.WriteLine("Name: {0}, Age: {1}", Atlantans[i].ArtistName, Atlantans[i].Age);
            }

            // //(Optional) Display the Group Name of all groups that have members that are not from New York City
            List<Artist> NoYorkers = Artists.Where(x => x.Hometown != "New York City").ToList();
            List<string> query = Groups.Join(NoYorkers, 
                                        group => group.Id,
                                        artist => artist.GroupId,
                                        (group, artist) => 
                                        {
                                            return group.GroupName;
                                        }).Distinct().ToList();
            foreach(string q in query)
            {
                System.Console.WriteLine(q);
            }
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            int WuId = Groups.Where(x => x.GroupName == "Wu-Tang Clan").ToList()[0].Id;
            List<Artist> WuTangers = Artists.Where(x => x.GroupId == WuId).ToList();
            foreach (var tanger in WuTangers)
            {
                System.Console.WriteLine(tanger.ArtistName);
            }
        }
    }
}
