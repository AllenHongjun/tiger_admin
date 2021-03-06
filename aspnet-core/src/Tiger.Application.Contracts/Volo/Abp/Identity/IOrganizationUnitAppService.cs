using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;

namespace Volo.Abp.Identity
{   
    /// <summary>
    /// 组织机构
    /// </summary>
    public interface IOrganizationUnitAppService : ICrudAppService<OrganizationUnitDto, Guid, GetOrganizationUnitInput, OrganizationUnitCreateDto, OrganizationUnitUpdateDto>
    {
        /// <summary>
        /// 获取根组织列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetRootListAsync();

        /// <summary>
        /// 获取组织明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> GetDetailsAsync(Guid id);

        Task<PagedResultDto<OrganizationUnitDto>> GetListDetailsAsync(GetOrganizationUnitInput input);

        /// <summary>
        /// 获取所有组织列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync(GetAllOrgnizationUnitInput input);

        /// <summary>
        /// 获取所有组织列表明细数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetAllListDetailsAsync(GetAllOrgnizationUnitInput input);

        /// <summary>
        /// 获取子集组织
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid parentId);

        Task<string> GetNextChildCodeAsync(Guid? parentId);

        Task MoveAsync(Guid id, Guid? parentId);

        /// <summary>
        /// 获取组织关联的用户
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="userInput"></param>
        /// <returns></returns>
        Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid? ouId,GetIdentityUsersInput userInput);
        
        /// <summary>
        /// 获取组织关联的角色
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="roleInput"></param>
        /// <returns></returns>
        Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid? ouId, PagedAndSortedResultRequestDto roleInput);
    }
}
