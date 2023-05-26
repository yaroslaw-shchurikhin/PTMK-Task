using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTK_TestWork.Details
{
    [Table("users")]
    public class User
    {
        [Column("userid"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [Column("dateofbirth")]
        public DateOnly DateOfBirth { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        public User()
        {
            Name = "";
            Gender = "";
        }

        public User(List<string> parameters)
        {
            Name = parameters[0];
            DateOfBirth = DateOnly.Parse(parameters[1]);
            Gender = parameters[2];
        }
    }
}
