using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Tiger.Basic.ProductAttributes
{
    public class ProductAttributeDto:EntityDto<Guid>
    {
        public string Name { get; set; }

        /// <summary>
        /// 属性选择类型：0->唯一；1->单选；2->多选；对应属性和参数意义不同；
        /// </summary>
        public int SelectType { get; set; }

        /// <summary>
        /// 属性录入方式：0->手工录入；1->从列表中选取
        /// </summary>
        public int InputType { get; set; }

        /// <summary>
        /// 可选值列表，以逗号隔开
        /// </summary>
        public string InputList { get; set; }

        /// <summary>
        /// 排序字段：最高的可以单独上传图片
        /// </summary>
        public int Sort { get; set; }

        public int FilterType { get; set; }

        /// <summary>
        /// 检索类型；0->不需要进行检索；1->关键字检索；2->范围检索',
        /// </summary>
        public int SearchType { get; set; }

        /// <summary>
        /// '相同属性产品是否关联；0->不关联；1->关联',
        /// </summary>
        public int RelatedStatus { get; set; }

        /// <summary>
        /// '是否支持手动新增；0->不支持；1->支持',
        /// </summary>
        public int HandAddStatus { get; set; }

        /// <summary>
        /// '属性的类型；0->规格；1->参数',
        /// </summary>
        public int Type { get; set; }

        public Guid ProductAttributeTypeId { get; set; }
    }
}
