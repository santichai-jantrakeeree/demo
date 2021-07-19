namespace Core.Entities.Identities
{
    public class Address : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}