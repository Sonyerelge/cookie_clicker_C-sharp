using System;

namespace FeeClicker
{
    class Character
    {
        private String name;
        private uint number;
        private uint coefficient;
        private ulong price;

        public Character(String name, uint number, uint coefficient, ulong price)
        {
            this.name = name;
            this.number = number;
            this.coefficient = coefficient;
            this.price = price;
        }
        
        public String getName()
        {
            return this.name;
        }

        public uint getNumber()
        {
            return this.number;
        }

        public uint getCoefficient()
        {
            return this.coefficient;
        }

        public UInt64 getPrice()
        {
            return this.price;
        }

        public void setNumber(uint number)
        {
            this.number = number;
        }

        public void setPrice(uint price)
        {
            this.price = price;
        }

        public UInt64 getStarsPerSecond()
        {
            return number * coefficient;
        }

        public void addOne()
        {
            number++;
            price = Convert.ToUInt64(price * 1.15);
        }
    }
}
