using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Appointments
{
    public class Service : ValueObject
    {
        public string Name { get; }

        public decimal Price { get; }

        private Service(decimal price, string name)
        {
            Price = price;
            Name = name;
        }

        public static Service Create(decimal price, string name)
        {
            return new Service(price, name);
        }
    }
}