using Business.Concrete;
using DataAccess.Concrete.InMemory;
using DataAccess.Concrete.EntityFramework;
using System;

namespace Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            carTest();
            

        }

        private static void carTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();

            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    System.Console.WriteLine(car.ColorName);
                }
            }

            else
                System.Console.WriteLine(result.Message);
        }
    }
}
