using System;
using System.Collections.Generic;

namespace Exam.MobileX
{
    public class VehicleRepository : IVehicleRepository
    {
        public int Count => throw new NotImplementedException();

        public void AddVehicleForSale(Vehicle vehicle, string sellerName)
        {
            throw new NotImplementedException();
        }

        public Vehicle BuyCheapestFromSeller(string sellerName)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Vehicle>> GetAllVehiclesGroupedByBrand()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetAllVehiclesOrderedByHorsepowerDescendingThenByPriceThenBySellerName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetVehicles(List<string> keywords)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetVehiclesBySeller(string sellerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetVehiclesInPriceRange(double lowerBound, double upperBound)
        {
            throw new NotImplementedException();
        }

        public void RemoveVehicle(string vehicleId)
        {
            throw new NotImplementedException();
        }
    }
}
