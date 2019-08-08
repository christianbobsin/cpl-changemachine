using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachineWF
{
    public enum Coin { One , FiftyCents , TwentyFiveCents , TenCents, FiveCents, OneCent  };
    public class Coins
    {

        public int One { get; set; }
        public int FiftyCents { get; set; }
        public int TwentyFiveCents { get; set; }
        public int TenCents { get; set; }
        public int FiveCents { get; set; }
        public int OneCent { get; set; }

        public bool hasCoins
        {
           get { return ((One + FiftyCents + TwentyFiveCents + TenCents + FiveCents + OneCent) > 0); }
        }
        public Coins()
        {
            Clear();
        }

        public void Clear()
        {
            One = 0;
            FiftyCents = 0;
            TwentyFiveCents = 0;
            TenCents = 0;
            FiveCents = 0;
            OneCent = 0;
        }

        public void Add(Coin c, int v)
        {
            switch (c)
            {
                case Coin.One:
                    One += v;
                    break;
                case Coin.FiftyCents:
                    FiftyCents += v;
                    break;
                case Coin.TwentyFiveCents:
                    TwentyFiveCents += v;
                    break;
                case Coin.TenCents:
                    TenCents += v;
                    break;
                case Coin.FiveCents:
                    FiveCents += v;
                    break;
                case Coin.OneCent:
                    OneCent += v;
                    break;
                default:
                    break;
            }
        }

        public bool Bleed(Coin c, int v)
        {
            bool result = false;
            switch (c)
            {
                case Coin.One:
                    result = isPossible(One, v);
                    if (result)
                    {
                        One -= v;
                    }
                    break;
                case Coin.FiftyCents:
                    result = isPossible(FiftyCents, v);
                    if (result)
                    {
                        FiftyCents -= v;
                    }
                    break;
                case Coin.TwentyFiveCents:
                    result = isPossible(TwentyFiveCents, v);
                    if (result)
                    {
                        TwentyFiveCents -= v;
                    }
                    break;
                case Coin.TenCents:
                    result = isPossible(TenCents, v);
                    if (result)
                    {
                        TenCents -= v;
                    }
                    break;
                case Coin.FiveCents:
                    result = isPossible(FiveCents, v);
                    if (result)
                    {
                        FiveCents -= v;
                    }
                    break;
                case Coin.OneCent:
                    result = isPossible(OneCent, v);
                    if (result)
                    {
                        OneCent -= v;
                    }
                    break;
            }

            return result;
        }

        private bool isPossible(int a, int b)
        {
            if (a >= b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
