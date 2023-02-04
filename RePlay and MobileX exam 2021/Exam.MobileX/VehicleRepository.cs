using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MobileX
{
    public class VehicleRepository : IVehicleRepository
    {
        public VehicleRepository()
        {
            this.Vehicles = new HashSet<Vehicle>();
            this.Sellers = new Dictionary<string, List<Vehicle>>();
            this.Brands = new Dictionary<string, SortedSet<Vehicle>>();
        }
        public HashSet<Vehicle> Vehicles { get; set; }
        public Dictionary<string, List<Vehicle>> Sellers { get; set; }
        public Dictionary<string, SortedSet<Vehicle>> Brands { get; set; }

        public int Count => this.Vehicles.Count;
        public bool Contains(Vehicle vehicle)
        => this.Vehicles.Contains(vehicle);

        public void AddVehicleForSale(Vehicle vehicle, string sellerName)
        {
            vehicle.Seller = sellerName;
            this.Vehicles.Add(vehicle);
            if (!Sellers.ContainsKey(sellerName))
            {
                Sellers.Add(sellerName, new List<Vehicle>());
            }
            Sellers[sellerName].Add(vehicle);
            if (!Brands.ContainsKey(vehicle.Brand))
            {
                Brands.Add(vehicle.Brand, new SortedSet<Vehicle>());
            }
            Brands[vehicle.Brand].Add(vehicle);
        }
        public void RemoveVehicle(string vehicleId)
        {
            var vehicle = this.Vehicles.FirstOrDefault(x => x.Id == vehicleId);
            if (vehicle == null)
            {
                throw new ArgumentException();
            }
            this.Vehicles.Remove(vehicle);
            this.Sellers[vehicle.Seller].Remove(vehicle);
            this.Brands[vehicle.Brand].Remove(vehicle);
        }

        public IEnumerable<Vehicle> GetVehicles(List<string> keywords)
        {
            return this.Vehicles
                .Where(v => 
                        keywords.Contains(v.Model)
                        || keywords.Contains(v.Location)
                        || keywords.Contains(v.Color)
                        || keywords.Contains(v.Brand))
                .OrderBy(x => x.IsVIP)
                .ThenBy(x => x.Price);
        }
        public IEnumerable<Vehicle> GetVehiclesBySeller(string sellerName)
        {
            if (!this.Sellers.ContainsKey(sellerName))
            {
                throw new ArgumentException();
            }
            return this.Sellers[sellerName];
        }

        public IEnumerable<Vehicle> GetVehiclesInPriceRange(double lowerBound, double upperBound)
        => this.Vehicles
            .Where(x=> lowerBound <= x.Price && x.Price <= upperBound)
            .OrderByDescending(x => x.Horsepower);

        public Vehicle BuyCheapestFromSeller(string sellerName)
        {
            if (!this.Sellers.ContainsKey(sellerName))
            {
                throw new ArgumentException();
            }
            var vehicle = this.Sellers[sellerName].OrderBy(x => x.Price).FirstOrDefault();
            if (vehicle == null)
            {
                throw new ArgumentException();
            }
            this.Vehicles.Remove(vehicle);
            this.Sellers[sellerName].Remove(vehicle);
            this.Brands[vehicle.Brand].Remove(vehicle);
            return vehicle;
        }


        public Dictionary<string, List<Vehicle>> GetAllVehiclesGroupedByBrand()
        {
            if (this.Count == 0)
            {
                throw new ArgumentException();
            }
            var result = new Dictionary<string, List<Vehicle>>();
            foreach (var brand in this.Brands)
            {
                if (!result.ContainsKey(brand.Key))
                {
                    result.Add(brand.Key, new List<Vehicle>());
                }
                result[brand.Key] = brand.Value.ToList();
            }

            return result;
        }

        public IEnumerable<Vehicle> GetAllVehiclesOrderedByHorsepowerDescendingThenByPriceThenBySellerName()
        => this.Vehicles
            .OrderByDescending(x => x.Horsepower)
            .ThenBy(x => x.Price)
            .ThenBy(x => x.Seller);
    }
}
