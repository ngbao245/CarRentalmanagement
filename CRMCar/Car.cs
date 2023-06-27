namespace CRMCar
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }

        public void carInfo()
        {
            Console.WriteLine("Car info is...");
        }
    }

    public class Wheel
    {
        public void wheelInfo()
        {
            Console.WriteLine("Wheel info is...");
        }
    }

    public class Tire
    {
        public void tireInfo() => Console.WriteLine();
    }
}
