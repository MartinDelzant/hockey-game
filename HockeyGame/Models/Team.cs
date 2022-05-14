using System.ComponentModel.DataAnnotations;

namespace HockeyGame.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Coach { get; set; }
        public int Year { get; set; }
    }
}
