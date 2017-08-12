using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace OIBot.Discord
{
    public static class GuildExtensions
    {
        public static async Task EnsureRolesAsync(this SocketGuild guild, string roleName, RoleProperties roleProperties)
        {
            var role = guild.Roles.FirstOrDefault(r => r.Name == roleName);

            if (role == null)
            {
                await guild.CreateRoleAsync(roleName, roleProperties.Permissions.Value, roleProperties.Color.Value, roleProperties.Hoist.Value).ConfigureAwait(false);
                return;
            }

            await role.ModifyAsync(x =>
            {
                x.Color = roleProperties.Color;
                x.Hoist = roleProperties.Hoist;
                x.Permissions = roleProperties.Permissions;
                x.Mentionable = roleProperties.Mentionable;
                x.Name = roleProperties.Name;
                x.Position = roleProperties.Position;
            }).ConfigureAwait(false);
        }
    }

    public static class RolePropertiesExtensions
    {
        public static void SetPermissions(this RoleProperties properties, GuildPermission permissions)
        {
            properties.Permissions = new GuildPermissions((ulong)permissions);
        }
    }
}
