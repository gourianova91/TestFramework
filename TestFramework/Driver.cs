using System;

namespace TestFramework
{
    public class Driver
    {
        private static readonly Lazy<Driver> lazy =
        new Lazy<Driver>(() => new Driver());

        public string Name { get; private set; }

        private Driver()
        {
            Name = System.Guid.NewGuid().ToString();
        }

        public static Driver GetDriver()
        {
            return lazy.Value;
        }
    } 
}
