using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeeClicker
{
    class Money
    {
        private int[] valueNumber = new int[] { 0, 0, 0, 0, 0, 0 };
        private String[] nameNumber = new String[] { "billiard", "billion", "milliard", "million", "", "" };

        public Money()
        {
            
        }

        public void setMoney(int[] money)
        {
            valueNumber = money;
        }

        public String writeMoney()
        {
            for(int i = 0 ; i < this.valueNumber.Length; i++)
            {
                if (valueNumber[i] != 0)
                {
                    if (i == this.valueNumber.Length - 1)
                    {
                        return valueNumber[i] + " " + nameNumber[i];
                    }
                    else
                    {
                        return valueNumber[i] + "." + valueNumber[i+1] + " " + nameNumber[i];
                    }
                }
            }
            return "0";
        }

    }
}
