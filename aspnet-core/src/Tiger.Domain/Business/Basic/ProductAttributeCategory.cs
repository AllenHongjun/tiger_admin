﻿/**
 * 类    名：AttributeCategory   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:54:41       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Basic
{
    /// <summary>
    /// 商品属性分类表
    /// 
    /// 商品属性分规格和参数 规格用户用户购买商品是选择  参数用户标注商品属性以及搜索是筛选
    /// </summary>
    public class ProductAttributeCategory: AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        /// <summary>
        /// 属性数量
        /// </summary>
        public int AttributeCount { get; set; }

        /// <summary>
        /// 参数数量
        /// </summary>
        public int ParamCount { get; set; }
    }
}
