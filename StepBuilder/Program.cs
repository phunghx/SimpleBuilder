using System;

namespace StepBuilder
{
    public enum CarType
    {
        Sedan, Crossover
    }
    public class Car
    {
        public CarType Type { set; get; }
        public int WheelSize { set ; get; }
        public override string ToString()
        {
            return $"Car type: {this.Type} with Size: {this.WheelSize}";
        }
    }
    public interface ISpecifyCarType
    {
        public ISpecifyWheelSize SelectType(CarType carType);
    }
    public interface ISpecifyWheelSize
    {
        public IBuilder SelectWheel(int size);
    }

    public interface IBuilder
    {
        public Car Build();
    }
    public class CarBuilder
    {
        public static ISpecifyCarType Create ()
        {
            return new ImlBuild();
        }
        private class ImlBuild : ISpecifyCarType,
            ISpecifyWheelSize, IBuilder
        {
            private Car car = new Car();
            public Car Build()
            {
                return car;
            }

            public ISpecifyWheelSize SelectType(CarType carType)
            {
                car.Type = carType;
                return this;
            }

            public IBuilder SelectWheel(int size)
            {
                switch(car.Type)
                {
                    case CarType.Sedan when size < 15 || size > 17:
                    case CarType.Crossover when size < 17 || size > 20:
                        throw new ArgumentOutOfRangeException($"Wrong Wheel size for {car.Type}");
                }
                car.WheelSize = size;
                return this;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var car1 = new Car {Type=CarType.Sedan,WheelSize=19 };

            var car = CarBuilder.Create()
                 .SelectType(CarType.Crossover)
                 .SelectWheel(19)
                 .Build();
            Console.WriteLine(car1);
        }
    }
}
