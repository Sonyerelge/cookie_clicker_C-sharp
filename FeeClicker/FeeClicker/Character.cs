using System;

namespace FeeClicker
{
    class Character
    {
        private String name;
        private uint number;
        private uint coefficient;
        private uint multiplier;
        private ulong defaultPrice;
        private ulong price;

        public Character(String name, uint number, uint coefficient, uint multiplier, ulong price)
        {
            this.name = name;
            this.number = number;
            this.coefficient = coefficient;
            this.multiplier = multiplier;
            this.price = price;
            this.defaultPrice = price;
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

        public uint getMultiplier()
        {
            return this.multiplier;
        }

        public UInt64 getPrice()
        {
            return this.price;
        }

        public void setNumber(uint number)
        {
            this.number = number;
        }

        public UInt64 getStarsPerSecond()
        {
            return this.number * this.coefficient;
        }

        public void addOne()
        {
            this.number++;
            this.price += 1;
        }
    }
}
