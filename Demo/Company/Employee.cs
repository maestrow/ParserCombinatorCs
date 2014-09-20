namespace Demo.Company
{
    public class Employee
    {
        public PersonName Name { get; set; }

        public PhoneNum Phone { get; set; }

        public string Position { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
