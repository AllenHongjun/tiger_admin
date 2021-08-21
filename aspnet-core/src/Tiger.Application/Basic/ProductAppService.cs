﻿using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    public class ProductAppService
    :
        CrudAppService<
            Product, //The  entity
            ProductDto, 
            Guid, //Primary key 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateProductDto>, //Used to create/update
        IProductAppService 
    {
        public ProductAppService(IRepository<Product, Guid> repository)
            : base(repository)
        {

        }
    }
}
