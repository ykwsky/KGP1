using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDelegate
{
    public delegate void MonsterKillDelegate(eMonster mon);
    public delegate void GetPointDelegate(int point);
    public delegate void GetPointSampleDelegate(int coin, int point, int exp);

    public class Program
    {
        static int userCoin = 0;
        static int userPoint = 0;
        static int userExp = 0;

        static void PlusCoin(int coin, int point, int exp)
        {
            Console.WriteLine($"  User Coin 적립 : {coin}");
            userCoin = userCoin + coin;
        }
        static void PlusPoint(int coin, int point, int exp)
        {
            Console.WriteLine($"  User Point 적립 : {point}");
            userPoint = userPoint + point;
        }
        static void PlusExp(int coin, int point, int exp)
        {
            Console.WriteLine($"  User Exp 적립 : {exp}");
            userExp = userExp + exp;
        }

        static void Main(string[] args)
        {
            GetPointSampleDelegate getPointSampleDelegate = PlusCoin;
            getPointSampleDelegate += PlusPoint;
            getPointSampleDelegate += PlusExp;

            for (int i = 0; i < 10; i++)
            {
                getPointSampleDelegate(100, 30, 40);                
                Console.WriteLine($"{i}. UserCoin : {userCoin}, UserPoint : {userPoint}, UserExp : {userExp}");
                Console.WriteLine("===============================================");
            }

            Console.ReadLine();
        }
    }
}
