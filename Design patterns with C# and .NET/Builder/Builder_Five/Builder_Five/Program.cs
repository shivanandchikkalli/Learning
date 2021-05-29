using System;

namespace Builder_Five
{
    public class Location
    {
        public string Area { get; set; }
        public long ZipCode { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"\n\t {nameof(Area)}: {Area},\n\t {nameof(ZipCode)}: {ZipCode},\n\t {nameof(Country)}: {Country}";
        }
    }

    public class Company
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public long Salary { get; set; }

        public override string ToString()
        {
            return $"\n\t {nameof(Name)}: {Name},\n\t {nameof(Position)}: {Position},\n\t {nameof(Salary)}: {Salary}";
        }
    }

    public class UserProfile
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Location Location { get; set; }
        public Company Company { get; set; }

        public override string ToString()
        {
            return $"User Details:\n\t {nameof(FirstName)}: {FirstName},\n\t {nameof(MiddleName)}: {MiddleName},\n\t {nameof(LastName)}: {LastName},\n {nameof(Location)}: {Location},\n {nameof(Company)}: {Company}";
        }
    }

    public class User
    {
        protected UserProfile _user = new UserProfile();

        /// <summary>
        /// Add user FirstName, LastName and MiddleName
        /// </summary>
        public UserProfileCreator With => new UserProfileCreator(_user);

        /// <summary>
        /// Add user Location details i.e., Area, ZipCode and Country
        /// </summary>
        public UserLocationCreator LivesAt => new UserLocationCreator(_user);

        /// <summary>
        /// Add user work details i.e., CompanyName, Position and Salary
        /// </summary>
        public UserJobCreator WorksAt => new UserJobCreator(_user);

        public UserProfile Build()
        {
            return _user;
        }
    }

    public class UserProfileCreator : User
    {
        public UserProfileCreator(UserProfile user)
        {
            _user = user;
        }

        public UserProfileCreator FirstName(string fName)
        {
            _user.FirstName = fName;
            return this;
        }

        public UserProfileCreator LastName(string lName)
        {
            _user.LastName = lName;
            return this;
        }

        public UserProfileCreator MiddleName(string mName)
        {
            _user.MiddleName = mName;
            return this;
        }
    }

    public class UserLocationCreator : User
    {
        public UserLocationCreator(UserProfile user)
        {
            _user = user;
            _user.Location = new Location();
        }

        public UserLocationCreator Area(string area)
        {
            _user.Location.Area = area;
            return this;
        }

        public UserLocationCreator Zip(long zip)
        {
            _user.Location.ZipCode = zip;
            return this;
        }

        public UserLocationCreator Country(string country)
        {
            _user.Location.Country = country;
            return this;
        }
    }

    public class UserJobCreator : User
    {
        public UserJobCreator(UserProfile user)
        {
            _user = user;
            _user.Company = new Company();
        }

        public UserJobCreator Company(string name)
        {
            _user.Company.Name = name;
            return this;
        }

        public UserJobCreator As(string position)
        {
            _user.Company.Position = position;
            return this;
        }

        public UserJobCreator Earns(long salary)
        {
            _user.Company.Salary = salary;
            return this;
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            var user = new User();
            var userProfile = user
                                .With
                                    .FirstName("Harry")
                                    .MiddleName("James")
                                    .LastName("Potter")
                                .LivesAt
                                    .Area("Polo Alto")
                                    .Zip(94304)
                                    .Country("USA")
                                .WorksAt
                                    .Company("Google")
                                    .As("Software Engineer")
                                    .Earns(120000)
                                .Build();


            Console.WriteLine(userProfile);

            Console.ReadLine();
        }
    }
}
