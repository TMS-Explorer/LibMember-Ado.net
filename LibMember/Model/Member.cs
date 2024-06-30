namespace LibMember.Model
{
    public class Member
    {
        public int ID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Address { get; set; }
        public int? Age { get; set; }
    }
}
