/**
 * 名    称：IAuthorRepository   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:08:23       
 * 说    明: 
 * 
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Demo
{
    /*
     IAuthorRepository extends the standard IRepository<Author, Guid> interface, so all the standard repository methods will also be available for the IAuthorRepository.
        FindByNameAsync was used in the AuthorManager to query an author by name.
        GetListAsync will be used in the application layer to get a listed, sorted and filtered list of authors to show on the UI.
        We will implement this repository in the next part.
     
     */
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        Task<Author> FindByNameAsync(string name);

        Task<List<Author>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
