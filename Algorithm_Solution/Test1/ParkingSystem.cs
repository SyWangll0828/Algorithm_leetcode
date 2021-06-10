using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    //1603. 设计停车系统
    public class ParkingSystem
    {
        int[] sum;
        public ParkingSystem(int big, int medium, int small)
        {
            sum = new int[3] { big, medium, small };
        }

        public bool AddCar(int carType)
        {
            switch (carType)
            {
                case 1:
                    if (sum[0] > 0)
                    {
                        sum[0]--;
                        return true;
                    }
                    else
                        return false;
                case 2:
                    if (sum[1] > 0)
                    {
                        sum[1]--;
                        return true;
                    }
                    else
                        return false;
                default:
                    if (sum[2] > 0)
                    {
                        sum[2]--;
                        return true;
                    }
                    else
                        return false;
            }
        }
    }
}
