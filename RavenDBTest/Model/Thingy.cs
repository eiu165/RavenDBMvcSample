
using System;
namespace RavenDBTest.Model
{
    public class Thingy
    {
        public string Name { get; set; }
    }
    public class Album
    {
        public string Title { get; set; }
        public string AlbumArtUrl { get; set; }
        public float Price { get; set; }
    }
    public class RoomType
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int NumberAvaliable { get; set; }
    }
}
