using System.ComponentModel.DataAnnotations;

namespace HockeyGame.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool IsCapitain { get; set; }
    }
}
