using System;
using System.Collections.Generic;
using System.Text;

namespace RentCafeTracker
{

    public class Rootobject
    {
        public All[] all { get; set; }
        public _6_10[] _6_10 { get; set; }
        public _36_40[] _36_40 { get; set; }
        public _31_35[] _31_35 { get; set; }
        public _1_5[] _1_5 { get; set; }
        public _16_20[] _16_20 { get; set; }
        public _21_25[] _21_25 { get; set; }
        public Unit[] units { get; set; }
        public _11_15[] _11_15 { get; set; }
        public _26_30[] _26_30 { get; set; }
    }

    public class All
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public string max_sq { get; set; }
        public string min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }

    public class _6_10
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public string max_sq { get; set; }
        public string min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }

    public class _36_40
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public int max_sq { get; set; }
        public int min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }

    public class _31_35
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public int max_sq { get; set; }
        public int min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }

    public class _1_5
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public string max_sq { get; set; }
        public string min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }

    public class _16_20
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public int max_sq { get; set; }
        public int min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }

    public class _21_25
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public string max_sq { get; set; }
        public string min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }

    public class Unit
    {
        public string dateAvailable { get; set; }
        public string sq { get; set; }
        public int bathType { get; set; }
        public int ufg { get; set; }
        //public string[] specials { get; set; }
        public string unitSpace { get; set; }
        public string bedType { get; set; }
        public string un { get; set; }
        //public string apply_url { get; set; }
        public int rent { get; set; }
        public string fp_name { get; set; }
        public string fi { get; set; }
        public int uf { get; set; }
    }

    public class _11_15
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public string max_sq { get; set; }
        public string min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }

    public class _26_30
    {
        public int max_uf { get; set; }
        public int min_uf { get; set; }
        public int max_sq { get; set; }
        public int min_sq { get; set; }
        public int length { get; set; }
        public int max_rent { get; set; }
        public int min_rent { get; set; }
    }


}
