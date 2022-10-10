using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineVehicleShowroom.Proxies.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom.TagHelpers
{
    [HtmlTargetElement("td",Attributes ="i-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _tokenString;
        private readonly RolesProxy _rolesService;

        public RoleUsersTagHelper(IHttpContextAccessor httpContextAccessor, RolesProxy rolesService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenString = _httpContextAccessor.HttpContext.Session.GetString("token");
            _rolesService = rolesService;
        }

        [HtmlAttributeName("i-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(_tokenString))
            {
                List<string> names = new List<string>();
                var role = await _rolesService.GetRoleByIdAsync(Role, _tokenString);
                if (role != null)
                {
                    foreach (var user in role.Members)
                    {
                        names.Add(user.UserName);
                    }
                }
                output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
            }
        }
    }
}
