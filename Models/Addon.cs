using System.ComponentModel.DataAnnotations;

namespace RustedMods.Models
{
    public class Addon
    {
        [Key]
        public int Id { get; set; }

        public string ModName { get; set; } = string.Empty;
        public string ModAuthor { get; set; } = string.Empty;
        public string ModRequirements { get; set; } = string.Empty;
        public string ModVersion { get; set; } = string.Empty;
        public string ModDate { get; set; } = string.Empty;
        public string ModDesc { get; set; } = string.Empty;
        public string ModSize { get; set; } = string.Empty;
        public string ModRating { get; set; } = string.Empty;

        // SEPARATE SOMETHING SOMETHING...
        public string ModDownload { get; set; } = string.Empty;
        public string ModThumbnail { get; set; } = string.Empty;
        public string ModScreenshots { get; set; } = string.Empty;


        public string ModTrailer { get; set; } = string.Empty;
        public string ModComments { get; set; } = string.Empty;
    }
}
