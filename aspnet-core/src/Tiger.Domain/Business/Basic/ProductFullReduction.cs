/**
 * 类    名：ProductFullReduction   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 14:07:27       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Basic
{   
    /// <summary>
    /// 产品满减价格表
    /// </summary>
    public class ProductFullReduction
    {
        public int ProductId { get; set; }


        public decimal FullPrice { get; set; }


        public decimal ReducePrice { get; set; }
    }
}
