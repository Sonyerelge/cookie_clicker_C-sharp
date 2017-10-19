using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeeClicker
{
    class Character
    {
        private String name;
        private int number;
        private double coefficient;
        private int multiplier;
        Money money = new Money();

        public Character(String name, int number, double coefficient, int multiplier)
        {
            this.name = name;
            this.number = number;
            this.coefficient = coefficient;
            this.multiplier = multiplier;
        }
        
        public String getName()
        {
            return this.name;
        }

        public int getNumber()
        {
            return this.number;
        }

        public double getCoefficient()
        {
            return this.coefficient;
        }

        public int getMultiplier()
        {
            return this.multiplier;
        }

        public void setNumber(int number)
        {
            this.number = number;
        }

        public double getStarsPerSecond()
        {
            return this.number * this.coefficient * this.multiplier;
        }

        public void addOne()
        {
            this.number++;
        }

        public void setMoney(int[] money)
        {
            this.money.setMoney(money);
        }

    }
}
